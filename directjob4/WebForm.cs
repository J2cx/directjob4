using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using mshtml;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Data;
using System.Drawing;
using System.Diagnostics;
//using System.Net;
//using System.Web;
//using System.Diagnostics;
//using System.Security;
using DirectJobsLibrary;


namespace directjob4
{
    public partial class WebForm : Form
    {
        public int npage = 0;
        private HashSet<string> pages = new HashSet<string>();
        public bool nomorepage = false;
        private int nbannonces = 0;
        public bool needfirstpage = false;
        private bool memepagesuivant = false;
        public bool extract = false;
        private HashSet<string> linksTrouve = new HashSet<string>();
        private bool fullpage = false;
        private bool infindannonce = false;
        public Hashtable one_site;
        public HashSet<string> allanonnces = new HashSet<string>();
        public bool bilanlog = false;
        //private bool showAnnonce = true;
        //public DataTable dtAnnIn = new DataTable();
        public static int insertfail = 0;
        private bool tri = true;
        private int nbtri = 0;
        /*
        private int nbNavigating = 0;
        private int nbactive = 0;
        private bool refresh = false;
        private bool deactivated = false;
        */
        public WebForm(bool extractAnnonces, Hashtable one_site)
        {
            InitializeComponent();
            if (!(bool)one_site["taleo"])
                this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            else
                this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted_taleo);
            extract = extractAnnonces;
            this.one_site = one_site;
            needfirstpage = !(bool)one_site["Acces_Direct"];
            memepagesuivant = (bool)one_site["Lien_Constant"];
            fullpage = (bool)one_site["FullPage"];
            insertfail = 0;
            nbtri = Convert.ToInt32(one_site["tri"]);
            //setDt();
            try
            {
                webBrowser1.Navigate(one_site["URL_ACCUEIL"].ToString());
            }
            catch (Exception exc)
            {
                MessageBox.Show("Constructor : " + exc.Message);
            }
        }

        private void WebForm_Load(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //if (((WebBrowser)sender).ReadyState != WebBrowserReadyState.Complete)
            //  return; 
            //Console.WriteLine("webBrowser1_DocumentCompleted " + e.Url.AbsolutePath + " : " + this.webBrowser1.Url.AbsolutePath);
            if (e.Url.AbsolutePath != this.webBrowser1.Url.AbsolutePath)
                return;
            //Console.WriteLine("DocComplete MessageBox Show");
            this.Text = one_site["ENTREPRISE"].ToString() + " " + (npage + Convert.ToInt16(!needfirstpage)).ToString() + " / " + Convert.ToInt32(one_site["MAXPAGES"]).ToString() + " page";
            Thread.Sleep(1000 * Convert.ToInt32(one_site["PAGE_DELAY"].ToString()));
            if (!pages.Contains(webBrowser1.Document.Url.ToString()))
                pages.Add(webBrowser1.Document.Url.ToString());

            if (((WebBrowser)sender).Document != null)
            {
                //Console.WriteLine("DocComplete not null");
                int i_maxpages = Convert.ToInt16(one_site["MAXPAGES"]) + Convert.ToInt16(needfirstpage);
                int i_firstpage = Convert.ToInt16(needfirstpage);
                if (npage < i_firstpage)
                {
                    //Console.WriteLine("npage < i_firstpage");
                    FindFirstPage((WebBrowser)sender);
                    return;
                }
                else if (npage < i_maxpages)
                {
                    //Console.WriteLine("npage < i maxpage, go find annonce");
                    // if ((bool)one_site["AnnExist"])
                    //{
                    if (Convert.ToInt32(one_site["tri"]) > 0 && tri)
                    {
                        triParDate();
                        tri = false;
                        return;
                    }
                    FindAnnonces((WebBrowser)sender);
                    //}
                    if (infindannonce)
                        return;
                    if (!(bool)one_site["OnePage"] && !(infindannonce))
                        if (FindNextPage((WebBrowser)sender))
                        {
                            npage++;
                            Console.WriteLine(npage.ToString());
                        }
                }

                if (npage >= i_maxpages || nomorepage || ((bool)one_site["OnePage"]&&npage>=Convert.ToInt16(needfirstpage)))
                {/*
                    Form1.BTest = true;
                    Form1.dtAnnForm = dtAnnIn;
                    //MessageBox.Show(dtAnnIn.Rows.Count.ToString());
                    if (!bilanlog)
                    {
                        bilanlog = true;
                        string sforlog;
                        int nbpagereal;
                        if ((bool)one_site["OnePage"])
                            nbpagereal = 1;
                        else
                            nbpagereal = npage - i_firstpage + Convert.ToInt16(nomorepage);
                        Console.WriteLine(nbpagereal.ToString() + " pages of " + one_site["ENTREPRISE"] + " " + allanonnces.Count.ToString() + " annonces of " + one_site["ENTREPRISE"]);

                        sforlog = one_site["ENTREPRISE"] + " : " + nbpagereal.ToString() + " pages, " + allanonnces.Count.ToString() + " annonces.";
                        Program.log(sforlog);
                    }
                  */
                    webClose();
                }
            }

        }

        private void FindFirstPage(WebBrowser webbrowser3)
        {
            //Console.WriteLine("start first page");
           // MessageBox.Show(webBrowser1.Document.Url.ToString());
            string[] str_separators = new string[] { ":::", " ;;;" };
            string[] strs_in = { "" };
            if (one_site["InFirstPage"] != null && one_site["InFirstPage"].ToString().Length > 7)
                strs_in = one_site["InFirstPage"].ToString().Split(str_separators, StringSplitOptions.RemoveEmptyEntries);
            string[] strs_ex = { "" };
            if (one_site["ExFirstPage"] != null && one_site["ExFirstPage"].ToString().Length > 7)
                strs_ex = one_site["ExFirstPage"].ToString().Split(str_separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (HtmlElement link in webbrowser3.Document.All)
            {
                bool findfirstpage = true;
                IHTMLElement4 ilink4 = (IHTMLElement4)link.DomElement;
                for (int tmp1 = 0; tmp1 < (strs_in.Length / 2); tmp1++)
                {
                    if (ilink4.getAttributeNode(strs_in[tmp1 * 2]) != null && ilink4.getAttributeNode(strs_in[tmp1 * 2]).nodeValue != null)
                    {
                        if (!ilink4.getAttributeNode(strs_in[tmp1 * 2]).nodeValue.ToString().Contains(strs_in[tmp1 * 2 + 1]))
                        {
                            findfirstpage = false;
                            break;
                        }
                    }
                    else if (!link.GetAttribute(strs_in[tmp1 * 2]).Contains(strs_in[tmp1 * 2 + 1]))
                    {
                        findfirstpage = false;
                        break;
                    }
                }
                for (int tmp2 = 0; tmp2 < (strs_ex.Length / 2); tmp2++)
                {
                    if (ilink4.getAttributeNode(strs_ex[tmp2 * 2]) != null && ilink4.getAttributeNode(strs_ex[tmp2 * 2]).nodeValue != null)
                    {
                        if (ilink4.getAttributeNode(strs_ex[tmp2 * 2]).nodeValue.ToString().Contains(strs_ex[tmp2 * 2 + 1]) || !findfirstpage)
                        {
                            findfirstpage = false;
                            break;
                        }
                    }
                    else if (link.GetAttribute(strs_ex[tmp2 * 2]).Contains(strs_ex[tmp2 * 2 + 1]) || !findfirstpage)
                    {
                        findfirstpage = false;
                        break;
                    }
                }
                if (findfirstpage)
                {
                    //Console.WriteLine("fin first page");
                    bool bonclik = false;
                    for (int tmp1 = 0; tmp1 < (strs_in.Length / 2); tmp1++)
                    {
                        if (strs_in[tmp1 * 2].IndexOf("onclick", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            bonclik = true;
                            link.RaiseEvent("onclick");
                            break;
                        }
                    }
                    if(!bonclik)
                    link.InvokeMember("click");

                    npage = 1;
                    break;
                }
            }
            

        }

        private bool FindAnnonces(WebBrowser webbrowser3)
        {
            HashSet<string> anonnces = new HashSet<string>();
            string[] str_separators = new string[] { ":::", " ;;;" };
            string[] strs_in = { "" };
            if (one_site["InAnnonce"] != null && one_site["InAnnonce"].ToString().Length > 7)
                strs_in = one_site["InAnnonce"].ToString().Split(str_separators, StringSplitOptions.RemoveEmptyEntries);
            string[] strs_ex = { "" };
            if (one_site["ExAnnonce"] != null && one_site["ExAnnonce"].ToString().Length > 7)
                strs_ex = one_site["ExAnnonce"].ToString().Split(str_separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (HtmlElement link in webbrowser3.Document.All)
            {
                bool findannonce = true;
                IHTMLElement4 ilink4 = (IHTMLElement4)link.DomElement;
                if (linksTrouve.Contains(link.OuterHtml))
                    continue;
                for (int tmp1 = 0; tmp1 < (strs_in.Length / 2); tmp1++)
                {
                    if (ilink4.getAttributeNode(strs_in[tmp1 * 2]) != null && ilink4.getAttributeNode(strs_in[tmp1 * 2]).nodeValue != null)
                    {
                        if (!ilink4.getAttributeNode(strs_in[tmp1 * 2]).nodeValue.ToString().Contains(strs_in[tmp1 * 2 + 1]))
                        {
                            findannonce = false;
                            break;
                        }
                    }
                    else if (!link.GetAttribute(strs_in[tmp1 * 2]).Contains(strs_in[tmp1 * 2 + 1]))
                    {
                        findannonce = false;
                        break;
                    }
                }
                for (int tmp2 = 0; tmp2 < (strs_ex.Length / 2); tmp2++)
                {
                    if (ilink4.getAttributeNode(strs_ex[tmp2 * 2]) != null && ilink4.getAttributeNode(strs_ex[tmp2 * 2]).nodeValue != null)
                    {
                        if (ilink4.getAttributeNode(strs_ex[tmp2 * 2]).nodeValue.ToString().Contains(strs_ex[tmp2 * 2 + 1]) || !findannonce)
                        {
                            findannonce = false;
                            break;
                        }
                    }
                    else if (link.GetAttribute(strs_ex[tmp2 * 2]).Contains(strs_ex[tmp2 * 2 + 1]) || !findannonce)
                    {
                        findannonce = false;
                        break;
                    }
                }
                if (findannonce)
                {
                    anonnces.Add(link.OuterHtml);
                    linksTrouve.Add(link.OuterHtml);
                    //Console.WriteLine("npage : " + npage.ToString() + " nb linksTrouve :" + linksTrouve.Count.ToString() + " link.OuterHtml :" + link.OuterHtml);

                    if (fullpage)
                    {
                        try
                        {
                            infindannonce = true;
                            this.webBrowser1.DocumentCompleted -= new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
                            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted_2);
                            try
                            {
                                //link.Focus();
                                //SendKeys.Send("ENTER");
                                bool bonclik = false;
                                for (int tmp1 = 0; tmp1 < (strs_in.Length / 2); tmp1++)
                                {
                                    if (strs_in[tmp1 * 2].IndexOf("onclick", StringComparison.OrdinalIgnoreCase) >= 0)
                                    {
                                        bonclik = true;
                                        link.RaiseEvent("onclick");
                                        break;
                                    }
                                }
                                if (!bonclik)
                                link.InvokeMember("click");
                                
                            }
                            catch (Exception exc)
                            {
                                Console.WriteLine("findAnnonce click " + exc.Message);
                            }
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show("findAnnonce fullpage " + exc.Message);
                        }
                        break;
                    }
                    else
                    {
                        //SQL.Connect();
                        int site_id = Convert.ToInt16(SQL.Get(@"select id from sites where ENTREPRISE = '" + one_site["ENTREPRISE"].ToString().Replace("'", "''") + "'"));
                        /*
                         DataRow row = dtAnnIn.NewRow();
                        row["id"] = site_id;
                        row["Entreprise"] = one_site["ENTREPRISE"].ToString();
                        row["link"] = link.GetAttribute("href");
                        row["link_hash"] = CreateMD5Hash(link.GetAttribute("href"));
                        dtAnnIn.Rows.Add(row);
                        */
                        //dtAnnIn.Rows.Add(site_id, one_site["ENTREPRISE"].ToString(),one_site["ENTREPRISE"].ToString(), link.GetAttribute("href"), CreateMD5Hash(link.GetAttribute("href")));
                        if (extract && site_id != 0)
                        {
                            if (SQL.Requet(@"insert into liens (id_site, nom, link, link_hash) values (" + site_id + ", '" + one_site["ENTREPRISE"].ToString().Replace("'", "''") + "', '"
                                        + link.GetAttribute("href") + "','" + CreateMD5Hash(link.GetAttribute("href")) + "')"))
                            {
                                anonnces.Add(link.GetAttribute("href"));
                                allanonnces.Add(anonnces.Last());
                                //dtAnnIn.Rows.Add(dr);
                            }
                            else
                            {
                                insertfail++;
                                if (insertfail > 5 && one_site["MAXPAGES"].ToString() == "99")
                                    webClose();
                            }
                        }
                        //SQL.Disconnect();
                    }
                }
            }
            //nbannonces += anonnces.Count();
            if (anonnces.Count() > 0)
                return true;
            else
                return false;
        }

        public void webBrowser1_DocumentCompleted_2(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsolutePath != this.webBrowser1.Url.AbsolutePath)
                return;

            string data = webBrowser1.Document.Body.InnerHtml;
            //WebExtract we = new WebExtract(data);
            //we.ShowDialog();

            //SQL.Connect();
            int site_id = 0;
            if (SQL.Get(@"select id from sites where ENTREPRISE = '" + one_site["ENTREPRISE"].ToString().Replace("'", "''") + "'") != null)
                site_id = Convert.ToInt16(SQL.Get(@"select id from sites where ENTREPRISE = '" + one_site["ENTREPRISE"].ToString().Replace("'", "''") + "'"));
            /*DataRow row = dtAnnIn.NewRow();
            row["id"] = site_id;
            row["Entreprise"] = one_site["ENTREPRISE"].ToString();
            row["source"] = data;
            row["source_hash"] = CreateMD5Hash(webBrowser1.Document.Title);
            dtAnnIn.Rows.Add(row);
            */
            //dtAnnIn.Rows.Add(site_id, one_site["ENTREPRISE"].ToString(), data, CreateMD5Hash(data));
            //Console.WriteLine(dtAnnIn.Rows.Count.ToString());
            if (extract && site_id != 0)
            {
                string sinnertext = webBrowser1.Document.Body.InnerText;
                string smd5 = CreateMD5Hash(sinnertext);
                if (SQL.Requet(@"insert into pages (id_site, source, source_hash) values (" + site_id.ToString() + ", '" + data.Replace("'", "''") + "', '" +smd5 + "')"))
                {
                    allanonnces.Add(data);
                    //dtAnnIn.Rows.Add(dr);
                }
                else
                {
                    insertfail++;
                    if (insertfail > 5 && one_site["MAXPAGES"].ToString() == "99")
                        webClose();
                }
            }
            //SQL.Disconnect();
            this.webBrowser1.DocumentCompleted -= new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted_2);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted_3);
            if ((bool)one_site["GoBack"])
            {
                if (this.webBrowser1.CanGoBack)
                    this.webBrowser1.GoBack();
                else
                {
                    /*
                    Form1.BTest = true;
                    Form1.dtAnnForm = dtAnnIn;
                    //MessageBox.Show(dtAnnIn.Rows.Count.ToString());
                    if (!bilanlog)
                    {
                        bilanlog = true;
                        Console.WriteLine("The site break down, it cant go back, so close!");
                        Program.log("The site break down, it cant go back, so close!");
                        //int i_firstpage = Convert.ToInt16(needfirstpage);
                        string sforlog;
                        Console.WriteLine((npage).ToString() + " pages of " + one_site["ENTREPRISE"] + " " + allanonnces.Count.ToString() + " annonces of " + one_site["ENTREPRISE"]);
                        sforlog = one_site["ENTREPRISE"] + " : " + (npage).ToString() + " pages, " + allanonnces.Count.ToString() + " annonces.";
                        Program.log(sforlog);
                    }
                    */
                    webClose();
                    this.Dispose();
                }
            }
            else
            {
                if (!GoBack(this.webBrowser1))
                {/*
                    Form1.BTest = true;
                    Form1.dtAnnForm = dtAnnIn;
                    //MessageBox.Show(dtAnnIn.Rows.Count.ToString());
                    if (!bilanlog)
                    {
                        bilanlog = true;
                        Console.WriteLine("The site break down, it cant go back, so close!");
                        Program.log("The site break down, it cant go back, so close!");
                        string sforlog;
                        //int i_firstpage = Convert.ToInt16(needfirstpage);
                        Console.WriteLine(npage.ToString() + " pages of " + one_site["ENTREPRISE"] + " " + allanonnces.Count.ToString() + " annonces of " + one_site["ENTREPRISE"]);
                        sforlog = one_site["ENTREPRISE"] + " : " + (npage).ToString() + " pages, " + allanonnces.Count.ToString() + " annonces.";
                        Program.log(sforlog);
                    }*/
                    this.webClose();
                    this.Dispose();
                    //return;
                }
            }
        }

        private bool GoBack(WebBrowser webbrowser3)
        {
            string[] str_separators = new string[] { ":::", " ;;;" };
            string[] strs_in = { "" };
            if (one_site["InGoBack"] != null && one_site["InGoBack"].ToString().Length > 7)
                strs_in = one_site["InGoBack"].ToString().Split(str_separators, StringSplitOptions.RemoveEmptyEntries);
            string[] strs_ex = { "" };
            if (one_site["ExGoBack"] != null && one_site["ExGoBack"].ToString().Length > 7)
                strs_ex = one_site["ExGoBack"].ToString().Split(str_separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (HtmlElement link in webbrowser3.Document.All)
            {
                bool findpagesuivant = true;
                IHTMLElement4 ilink4 = (IHTMLElement4)link.DomElement;
                for (int tmp1 = 0; tmp1 < (strs_in.Length / 2); tmp1++)
                {
                    if (ilink4.getAttributeNode(strs_in[tmp1 * 2]) != null && ilink4.getAttributeNode(strs_in[tmp1 * 2]).nodeValue != null)
                    {
                        if (!ilink4.getAttributeNode(strs_in[tmp1 * 2]).nodeValue.ToString().Contains(strs_in[tmp1 * 2 + 1]))
                        {
                            findpagesuivant = false;
                            break;
                        }
                    }
                    else if (!link.GetAttribute(strs_in[tmp1 * 2]).Contains(strs_in[tmp1 * 2 + 1]))
                    {
                        findpagesuivant = false;
                        break;
                    }
                }
                for (int tmp2 = 0; tmp2 < (strs_ex.Length / 2); tmp2++)
                {
                    if (ilink4.getAttributeNode(strs_ex[tmp2 * 2]) != null && ilink4.getAttributeNode(strs_ex[tmp2 * 2]).nodeValue != null)
                    {
                        if (ilink4.getAttributeNode(strs_ex[tmp2 * 2]).nodeValue.ToString().Contains(strs_ex[tmp2 * 2 + 1]) || !findpagesuivant)
                        {
                            findpagesuivant = false;
                            break;
                        }
                    }
                    else if (link.GetAttribute(strs_ex[tmp2 * 2]).Contains(strs_ex[tmp2 * 2 + 1]) || !findpagesuivant)
                    {
                        findpagesuivant = false;
                        break;
                    }
                }

                if (findpagesuivant)
                {
                    bool bonclik = false;
                    for (int tmp1 = 0; tmp1 < (strs_in.Length / 2); tmp1++)
                    {
                        if (strs_in[tmp1 * 2].IndexOf("onclick", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            bonclik = true;
                            link.RaiseEvent("onclick");
                            break;
                        }
                    }
                    if (!bonclik)
                    
                    link.InvokeMember("click");
                    return true;
                }
            }
            return false;
        }

        public string CreateMD5Hash(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
                // To force the hex string to lower-case letters instead of
                // upper-case, use he following line instead:
                // sb.Append(hashBytes[i].ToString("x2")); 
            }
            return sb.ToString();
        }

        public void webBrowser1_DocumentCompleted_3(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsolutePath != this.webBrowser1.Url.AbsolutePath)
                return;

            this.webBrowser1.DocumentCompleted -= new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted_2);
            this.webBrowser1.DocumentCompleted -= new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted_3);

            if (!FindAnnonces(webBrowser1))
            {
                infindannonce = false;

                this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
                WebBrowserDocumentCompletedEventArgs eargs = new WebBrowserDocumentCompletedEventArgs(webBrowser1.Url);
                webBrowser1_DocumentCompleted_tmp2(webBrowser1, eargs);
            }
        }

        private void webBrowser1_DocumentCompleted_tmp2(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsolutePath != this.webBrowser1.Url.AbsolutePath)
                return;
            if (!pages.Contains(webBrowser1.Document.Url.ToString()))
                pages.Add(webBrowser1.Document.Url.ToString());
            Thread.Sleep(1000 * Convert.ToInt32(one_site["PAGE_DELAY"].ToString()));
            if (((WebBrowser)sender).Document != null)
            {
                int i_maxpages = Convert.ToInt16(one_site["MAXPAGES"]) + Convert.ToInt16(needfirstpage);
                int i_firstpage = Convert.ToInt16(needfirstpage);
                if (npage < i_firstpage)
                {
                    FindFirstPage((WebBrowser)sender);
                }
                else if (npage < i_maxpages)
                {
                    FindAnnonces((WebBrowser)sender);
                    if (infindannonce)
                        return;
                    if (FindNextPage((WebBrowser)sender))
                    {
                        if((bool)one_site["GoBack"])
                        npage++;
                    }
                }

                if (npage >= i_maxpages || nomorepage || ((bool)one_site["OnePage"] && npage >= Convert.ToInt16(needfirstpage)))
                {/*
                    Form1.BTest = true;
                    Form1.dtAnnForm = dtAnnIn;
                    //MessageBox.Show(dtAnnIn.Rows.Count.ToString());
                    if (!bilanlog)
                    {
                        bilanlog = true;
                        string sforlog;
                        int nbpagereal;
                        if ((bool)one_site["OnePage"])
                            nbpagereal = 1;
                        else
                            nbpagereal = npage - i_firstpage + Convert.ToInt16(nomorepage);
                        Console.WriteLine(nbpagereal.ToString() + " pages of " + one_site["ENTREPRISE"] + " " + allanonnces.Count.ToString() + " annonces of " + one_site["ENTREPRISE"]);

                        sforlog = one_site["ENTREPRISE"] + " : " + nbpagereal.ToString() + " pages, " + allanonnces.Count.ToString() + " annonces.";
                        Program.log(sforlog);
                    }*/
                    webClose();
                }

            }
        }

        private bool FindNextPage(WebBrowser webbrowser3)
        {

            string[] str_separators = new string[] { ":::", " ;;;" };
            string[] strs_in = { "" };
            if (one_site["InPageSuivant"] != null && one_site["InPageSuivant"].ToString().Length > 7)
                strs_in = one_site["InPageSuivant"].ToString().Split(str_separators, StringSplitOptions.RemoveEmptyEntries);
            string[] strs_ex = { "" };
            if (one_site["ExPageSuivant"] != null && one_site["ExPageSuivant"].ToString().Length > 7)
                strs_ex = one_site["ExPageSuivant"].ToString().Split(str_separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (HtmlElement link in webbrowser3.Document.All)
            {
                bool findpagesuivant = true;
                IHTMLElement4 ilink4 = (IHTMLElement4)link.DomElement;
                for (int tmp1 = 0; tmp1 < (strs_in.Length / 2); tmp1++)
                {
                    if (ilink4.getAttributeNode(strs_in[tmp1 * 2]) != null && ilink4.getAttributeNode(strs_in[tmp1 * 2]).nodeValue != null)
                    {
                        if (!ilink4.getAttributeNode(strs_in[tmp1 * 2]).nodeValue.ToString().Contains(strs_in[tmp1 * 2 + 1]))
                        {
                            findpagesuivant = false;
                            break;
                        }
                    }
                    else if (!link.GetAttribute(strs_in[tmp1 * 2]).Contains(strs_in[tmp1 * 2 + 1]))
                    {
                        findpagesuivant = false;
                        break;
                    }
                }
                for (int tmp2 = 0; tmp2 < (strs_ex.Length / 2); tmp2++)
                {
                    if (ilink4.getAttributeNode(strs_ex[tmp2 * 2]) != null && ilink4.getAttributeNode(strs_ex[tmp2 * 2]).nodeValue != null)
                    {
                        if (ilink4.getAttributeNode(strs_ex[tmp2 * 2]).nodeValue.ToString().Contains(strs_ex[tmp2 * 2 + 1]) || !findpagesuivant)
                        {
                            findpagesuivant = false;
                            break;
                        }
                    }
                    else if (link.GetAttribute(strs_ex[tmp2 * 2]).Contains(strs_ex[tmp2 * 2 + 1]) || !findpagesuivant)
                    {
                        findpagesuivant = false;
                        break;
                    }
                }
                if (!memepagesuivant)
                {

                    bool exist = false;
                    foreach (string onepage in pages)
                    {
                        if (link.GetAttribute("HREF").Equals(onepage) || link.GetAttribute("HREF").Contains(onepage + "#"))
                        {
                            exist = true;
                            break;
                        }
                    }
                    if (findpagesuivant && !exist)
                    {
                        bool bonclik = false;
                        for (int tmp1 = 0; tmp1 < (strs_in.Length / 2); tmp1++)
                        {
                            if (strs_in[tmp1 * 2].IndexOf("onclick", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                bonclik = true;
                                link.RaiseEvent("onclick");
                                break;
                            }
                        }
                        if (!bonclik)

                        link.InvokeMember("click");
                        return true;

                    }
                }
                else
                {
                    if (findpagesuivant)
                    {
                        bool bonclik = false;
                        for (int tmp1 = 0; tmp1 < (strs_in.Length / 2); tmp1++)
                        {
                            if (strs_in[tmp1 * 2].IndexOf("onclick", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                bonclik = true;
                                link.RaiseEvent("onclick");
                                break;
                            }
                        }
                        if (!bonclik)
                        link.InvokeMember("click");
                        return true;

                    }
                }
            }
            nomorepage = true;
            return false;
        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            one_site["PopUp"] = true;
            DirectJobForm.Parametres["PopUp"] = true;
            this.webBrowser1.DocumentCompleted -= new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted_2);
            this.Activated += new System.EventHandler(this.webForm_Activated);
            test t1 = new test(this);
            ThreadStart job = new ThreadStart(t1.testrun);
            Thread thread = new Thread(job);
            thread.Start();
            Thread.Sleep(1000);
        }

        public void webBrowser1_DocumentCompleted_tmp(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsolutePath != this.webBrowser1.Url.AbsolutePath)
                return;
            if (!pages.Contains(webBrowser1.Document.Url.ToString()))
                pages.Add(webBrowser1.Document.Url.ToString());
            if (!FindAnnonces(webBrowser1))
            {
                infindannonce = false;
                Thread.Sleep(1000 * Convert.ToInt32(one_site["PAGE_DELAY"].ToString()));
                //pages.Add(webBrowser1.Document.Url.ToString());
                if (((WebBrowser)sender).Document != null)
                {
                    int i_maxpages = Convert.ToInt16(one_site["MAXPAGES"]) + Convert.ToInt16(needfirstpage);
                    int i_firstpage = Convert.ToInt16(needfirstpage);
                    if (npage < i_firstpage)
                    {
                        FindFirstPage((WebBrowser)sender);
                    }
                    else if (npage < i_maxpages)
                    {
                        FindAnnonces((WebBrowser)sender);
                        if (infindannonce)
                            return;
                        FindNextPage((WebBrowser)sender);
                    }

                    if (npage >= i_maxpages || nomorepage)
                    {/*
                        Form1.BTest = true;
                        Form1.dtAnnForm = dtAnnIn;
                        //MessageBox.Show(dtAnnIn.Rows.Count.ToString());
                        if (!bilanlog)
                        {
                            bilanlog = true;
                            string sforlog;
                            int nbpagereal;
                            if ((bool)one_site["OnePage"])
                                nbpagereal = 1;
                            else
                                nbpagereal = npage - i_firstpage + Convert.ToInt16(nomorepage);
                            Console.WriteLine(nbpagereal.ToString() + " pages of " + one_site["ENTREPRISE"] + " " + allanonnces.Count.ToString() + " annonces of " + one_site["ENTREPRISE"]);

                            sforlog = one_site["ENTREPRISE"] + " : " + nbpagereal.ToString() + " pages, " + allanonnces.Count.ToString() + " annonces.";
                            Program.log(sforlog);
                        }*/
                        this.webClose();
                        //Dispose();
                    }
                }
            }
        }

        public void webForm_Activated(object sender, EventArgs e)
        {
            //Console.WriteLine("webForm activated!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            this.Activated -= new System.EventHandler(this.webForm_Activated);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            WebBrowserDocumentCompletedEventArgs eargs = new WebBrowserDocumentCompletedEventArgs(this.webBrowser1.Url);
            this.webBrowser1_DocumentCompleted_tmp(this.webBrowser1, eargs);
        }
        private void setDt()
        {
            /*
            DataColumn dcIndex = new DataColumn("No.");
            dcIndex.AutoIncrement = true;
            dcIndex.AutoIncrementSeed = 1;
            dcIndex.AutoIncrementStep = 1;
            dtAnnIn.Columns.Add(dcIndex);
            dtAnnIn.PrimaryKey = new DataColumn[] { dcIndex };



            //dtAnnIn.Columns[0].ColumnName = "id";
            DataColumn dc0 = new DataColumn();
            dc0.ColumnName = "id";
            dc0.ReadOnly = true;
            //dc0.Unique = true;
            //dc0.AutoIncrement = true;
            dtAnnIn.Columns.Add(dc0);

            DataColumn dc1 = new DataColumn();
            dc1.ColumnName = "Entreprise";
            dtAnnIn.Columns.Add(dc1);
            if (fullpage || (bool)one_site["taleo"])
            {
                dtAnnIn.TableName = "pages";
                DataColumn dc2 = new DataColumn();
                DataColumn dc3 = new DataColumn();
                dc2.ColumnName = "source";
                dc3.ColumnName = "source_hash";
                dc3.Unique = true;
                dtAnnIn.Columns.Add(dc2);
                dtAnnIn.Columns.Add(dc3);
            }
            else
            {
                dtAnnIn.TableName = "liens";
                DataColumn dc2 = new DataColumn();
                DataColumn dc3 = new DataColumn();
                dc2.ColumnName = "link";
                dc3.ColumnName = "link_hash";
                dc3.Unique = true;
                dtAnnIn.Columns.Add(dc2);
                dtAnnIn.Columns.Add(dc3);

            }
             */
            // dtAnnIn.Columns[3].Unique = true;
            /*
                        songsDataGridView.Columns[0].Name = "Release Date";
    songsDataGridView.Columns[1].Name = "Track";
    songsDataGridView.Columns[2].Name = "Title";
    songsDataGridView.Columns[3].Name = "Artist";
    songsDataGridView.Columns[4].Name = "Album";
             * */
            //else
            //dtAnnIn = 

            //Form1.dtAnnForm = dtAnnIn;
        }
        private void webBrowser1_DocumentCompleted_taleo(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            OpenAnnonce();
        }
        public void OpenAnnonce()
        {
            Console.WriteLine("start open an annonce");
            foreach (HtmlElement link in webBrowser1.Document.All)
            {
                if (link.OuterHtml != null)
                {
                    if (link.GetAttribute("id").Contains("reqTitleLinkAction"))
                    {
                        webBrowser1.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted_taleo);
                        webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted_taleo_2);
                        Console.WriteLine(link.InnerHtml);
                        nbannonces = 1;
                        
                        link.InvokeMember("click");
                        //link.Focus();
                        //SendKeys.Send("{ENTER}");
                        break;
                    }
                }
            }
            //return false;
        }
        // private int lala = 0;
        private void webBrowser1_DocumentCompleted_taleo_2(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //if (((WebBrowser)sender).ReadyState != WebBrowserReadyState.Complete)
            //return;
            this.Text=one_site["ENTREPRISE"]+" "+(nbannonces-1).ToString()+" / "+(10 * Convert.ToInt32(one_site["MAXPAGES"])).ToString()+" annonce";
            string data = "";

            if (nbannonces <= 10 * Convert.ToInt16(one_site["MAXPAGES"]))
            {
                if (webBrowser1.Document != null)
                {

                    data = webBrowser1.Document.Body.InnerHtml;
                    //SQL.Connect();
                    try
                    {
                        int site_id = 0;
                        if (SQL.Get(@"select id from sites where ENTREPRISE = '" + one_site["ENTREPRISE"].ToString().Replace("'", "''") + "'") != null)
                            site_id = Convert.ToInt16(SQL.Get(@"select id from sites where ENTREPRISE = '" + one_site["ENTREPRISE"].ToString().Replace("'", "''") + "'"));
                        //Console.WriteLine("innsert an annonce into dt");
                        /*
                        DataRow row = dtAnnIn.NewRow();
                        row["id"] = site_id;
                        row["Entreprise"] = one_site["ENTREPRISE"].ToString();
                        row["source"] = data;
                        row["source_hash"] = CreateMD5Hash(webBrowser1.Document.Title);
                        dtAnnIn.Rows.Add(row);
                        */
                        //dtAnnIn.Rows.Add(site_id, one_site["ENTREPRISE"].ToString(), data, CreateMD5Hash(data));
                        if (extract && site_id != 0)
                        {
                            bool insertdone = false;
                            insertdone = SQL.Requet(@"insert into pages (id_site, source, source_hash) values (" + site_id + ", '"
                                + data.Replace("'", "''") + "', '" + CreateMD5Hash(webBrowser1.Document.Title) + "')");
                            if (insertdone)
                            {
                                allanonnces.Add(data);

                                //dtAnnIn.Rows.Add(dr);

                            }
                            else
                            {
                                insertfail++;
                                if (insertfail > 5 && one_site["MAXPAGES"].ToString() == "99")
                                    webClose();
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("taleo_2 " + exc.Message);
                    }
                    //SQL.Disconnect();
                }
                nbannonces++;
                //lala++;
                //Console.WriteLine("lala : "+lala.ToString());
                NextAnnonce();

            }
            else
            {/*
                Form1.BTest = true;
                Form1.dtAnnForm = dtAnnIn;
                //Console.WriteLine(dtAnnIn.Rows.Count.ToString());
                //Console.WriteLine("log and close");
                if (!bilanlog)
                {
                    int i_firstpage = Convert.ToInt16(needfirstpage);
                    bilanlog = true;
                    string sforlog;
                    int nbpagereal;
                    if ((bool)one_site["OnePage"])
                        nbpagereal = 1;
                    else
                        nbpagereal = npage - i_firstpage + Convert.ToInt16(nomorepage);
                    Console.WriteLine(nbpagereal.ToString() + " pages of " + one_site["ENTREPRISE"] + " " + allanonnces.Count.ToString() + " annonces of " + one_site["ENTREPRISE"]);

                    sforlog = one_site["ENTREPRISE"] + " : " + nbpagereal.ToString() + " pages, " + allanonnces.Count.ToString() + " annonces.";
                    Program.log(sforlog);
                }
                */
                webClose();
                return;
            }
        }
        private Thread th;
        public bool NextAnnonce()
        {
            foreach (HtmlElement link in webBrowser1.Document.All)
            {
                IHTMLElement4 ilink4 = (IHTMLElement4)link.DomElement;

                if (link.GetAttribute("id").Contains(one_site["NextAnn"].ToString()))
                {

                    //Console.WriteLine("Trouve one");
                    //Console.WriteLine("id : " + link.GetAttribute("id"));

                    //nbpages++;
                    //Console.WriteLine(link.Name.ToString());

                    Program.taleoclose = 2;
                    backgroud bg = new backgroud(this);
                    ThreadStart ts = new ThreadStart(bg.runback);
                    th = new Thread(ts);
                    th.Name = "thname";
                    th.Start();
                    webBrowser1.Navigating += new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);

                    /*
                    if (ilink4.getAttributeNode("onclick").nodeValue != null && ilink4.getAttributeNode("onclick").nodeValue.ToString()!="")
                        Console.WriteLine(ilink4.getAttributeNode("onclick").nodeValue);
                    else
                        Console.WriteLine("get node onclick null" + ilink4.getAttributeNode("onclick").nodeValue);
                    */
                    /*
                    if (((IHTMLElement4)link.Parent.DomElement).getAttributeNode("class").nodeValue.ToString().Contains("off"))
                        Console.WriteLine(((IHTMLElement4)link.Parent.DomElement).getAttributeNode("class").nodeValue);
                    else
                        Console.WriteLine("not off " + ((IHTMLElement4)link.Parent.DomElement).getAttributeNode("class").nodeValue);
                     */
                    link.Focus();
                    //Console.WriteLine("after focus");
                    // return;    
                    //link.InvokeMember("click");
                    SendKeys.Send("{ENTER}");
                    //    link.InvokeMember("click");
                    return true;
                    //1nbpages++;
                }
            }
            /*
            Console.WriteLine("gbne!!!");
            Form1.BTest = true;
            Form1.dtAnnForm = dtAnnIn;
            if (!bilanlog)
            {
                int i_firstpage = Convert.ToInt16(needfirstpage);
                bilanlog = true;
                string sforlog;
                int nbpagereal;
                if ((bool)one_site["OnePage"])
                    nbpagereal = 1;
                else
                    nbpagereal = npage - i_firstpage + Convert.ToInt16(nomorepage);
                Console.WriteLine(nbpagereal.ToString() + " pages of " + one_site["ENTREPRISE"] + " " + allanonnces.Count.ToString() + " annonces of " + one_site["ENTREPRISE"]);

                sforlog = one_site["ENTREPRISE"] + " : " + nbpagereal.ToString() + " pages, " + allanonnces.Count.ToString() + " annonces.";
                Program.log(sforlog);
            }

            Close();
            */
            return false;
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Program.taleoclose = 5;
            webBrowser1.Navigating -= new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);
            th.Join();

        }
        public void webClose()
        {
            DirectJobForm.BTest = true;
            //Form1.dtAnnForm = dtAnnIn;
            //Console.WriteLine(dtAnnIn.Rows.Count.ToString());
            //Console.WriteLine("log and close");
            if (!bilanlog)
            {
                int i_firstpage = Convert.ToInt16(needfirstpage);
                bilanlog = true;
                string sforlog;
                int nbpagereal;
                if ((bool)one_site["OnePage"])
                    nbpagereal = 1;
                else
                    nbpagereal = npage - i_firstpage + Convert.ToInt16(nomorepage);
                Console.WriteLine(nbpagereal.ToString() + " pages of " + one_site["ENTREPRISE"] + " " + allanonnces.Count.ToString() + " annonces of " + one_site["ENTREPRISE"]);

                sforlog = one_site["ENTREPRISE"] + " : " + nbpagereal.ToString() + " pages, " + allanonnces.Count.ToString() + " annonces.";
                Fuctions.log(sforlog);
            }

            Close();
        }
        private void triParDate()
        {
            Console.WriteLine("tri par date");
            string[] str_separators = new string[] { ":::", " ;;;" };
            string[] strs_in = { "" };
            if (one_site["Intri"] != null && one_site["Intri"].ToString().Length > 7)
                strs_in = one_site["Intri"].ToString().Split(str_separators, StringSplitOptions.RemoveEmptyEntries);
            string[] strs_ex = { "" };
            if (one_site["Extri"] != null && one_site["Extri"].ToString().Length > 7)
                strs_ex = one_site["Extri"].ToString().Split(str_separators, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (HtmlElement link in webBrowser1.Document.All)
            {
                bool tripardate = true;
                IHTMLElement4 ilink4 = (IHTMLElement4)link.DomElement;
                for (int tmp1 = 0; tmp1 < (strs_in.Length / 2); tmp1++)
                {
                    if (ilink4.getAttributeNode(strs_in[tmp1 * 2]) != null && ilink4.getAttributeNode(strs_in[tmp1 * 2]).nodeValue != null)
                    {
                        if (!ilink4.getAttributeNode(strs_in[tmp1 * 2]).nodeValue.ToString().Contains(strs_in[tmp1 * 2 + 1]))
                        {
                            tripardate = false;
                            break;
                        }
                    }
                    else if (!link.GetAttribute(strs_in[tmp1 * 2]).Contains(strs_in[tmp1 * 2 + 1]))
                    {
                        tripardate = false;
                        break;
                    }
                }
                for (int tmp2 = 0; tmp2 < (strs_ex.Length / 2); tmp2++)
                {
                    if (ilink4.getAttributeNode(strs_ex[tmp2 * 2]) != null && ilink4.getAttributeNode(strs_ex[tmp2 * 2]).nodeValue != null)
                    {
                        if (ilink4.getAttributeNode(strs_ex[tmp2 * 2]).nodeValue.ToString().Contains(strs_ex[tmp2 * 2 + 1]) || !tripardate)
                        {
                            tripardate = false;
                            break;
                        }
                    }
                    else if (link.GetAttribute(strs_ex[tmp2 * 2]).Contains(strs_ex[tmp2 * 2 + 1]) || !tripardate)
                    {
                        tripardate = false;
                        break;
                    }
                }

                if (tripardate)
                {
                    Console.WriteLine("find tri par date" + link.OuterText);
                    webBrowser1.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
                    webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted_tri);
                    //Console.WriteLine(webBrowser1.DocumentCompleted);
                    Console.WriteLine("nbtri : " + nbtri.ToString());
                    nbtri--;
                    bool bonclik = false;
                    for (int tmp1 = 0; tmp1 < (strs_in.Length / 2); tmp1++)
                    {
                        if (strs_in[tmp1 * 2].IndexOf("onclick", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            bonclik = true;
                            link.RaiseEvent("onclick");
                            break;
                        }
                    }
                    if (!bonclik)
                    link.InvokeMember("click");


                    return;
                }
            }
            return;
        }
        private  bool once=false;
                
        private void webBrowser1_DocumentCompleted_tri(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            Console.WriteLine(e.Url.AbsolutePath + " : " + this.webBrowser1.Url.AbsolutePath);
            if (e.Url.AbsolutePath != this.webBrowser1.Url.AbsolutePath)
                return;
            webBrowser1.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted_tri);
            if (nbtri <= 0 )
            {
                if (once == false)
                {
                    once = true;

                    if ((bool)one_site["Acces_Direct"])
                    {
                        npage = 0;
                    }
                    else
                    {
                        npage = 1;
                    }
                    
                    webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
                    webBrowser1_DocumentCompleted_tri_tmp(sender, e);
                    return;
                }
                else
                    return;
            }
            else
            {
                
                triParDate();
            }
        }
        private void webBrowser1_DocumentCompleted_tri_tmp(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (((WebBrowser)sender).Document != null)
            {
                int i_maxpages = Convert.ToInt16(one_site["MAXPAGES"]) + Convert.ToInt16(needfirstpage);
                FindAnnonces((WebBrowser)sender);
                //}
                if (!(bool)one_site["OnePage"] && !(infindannonce))
                    if (FindNextPage((WebBrowser)sender))
                    {
                        npage++;
                        //Console.WriteLine(npage.ToString());
                    }


                if (npage >= i_maxpages || nomorepage || (bool)one_site["OnePage"])
                {

                    webClose();
                }
            }
        }


    }
       
    

    public class test
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        public static StringBuilder buff;
        int chars = 256;
        WebForm webform_tmp;

        public test(WebForm webf1)
        {
            buff = new StringBuilder(chars);
            webform_tmp = webf1;
        }

        public void testrun()
        {

            while (true)
            {
                // Obtain the handle of the active window. 
                IntPtr handle = GetForegroundWindow();
                //Console.WriteLine(buff.ToString());
                //Console.WriteLine(handle.ToString());
                // Update the controls. 
                if (GetWindowText(handle, buff, chars) > 0)
                {
                    //Console.WriteLine(buff.ToString());
                    //Console.WriteLine(handle.ToString());

                    if (buff.ToString().Contains("Internet"))
                    {
                        SHDocVw.ShellWindows shellWindows = new SHDocVw.ShellWindowsClass();
                        string filename;
                        foreach (SHDocVw.InternetExplorer ie in shellWindows)
                        {
                            filename = Path.GetFileNameWithoutExtension(ie.FullName).ToLower();
                            if (filename.Equals("iexplore") && ie.HWND == handle.ToInt32() && ie.Document != null)
                            {
                                mshtml.IHTMLDocument2 htmlDoc = ie.Document as mshtml.IHTMLDocument2;
                                //SQL.Connect();
                                try
                                {
                                    int site_id = 0;
                                    if (SQL.Get(@"select id from sites where ENTREPRISE = '" + webform_tmp.one_site["ENTREPRISE"].ToString().Replace("'", "''") + "'") != null)
                                        site_id = Convert.ToInt16(SQL.Get(@"select id from sites where ENTREPRISE = '" + webform_tmp.one_site["ENTREPRISE"].ToString().Replace("'", "''") + "'"));
                                   /*
                                    DataRow row = webform_tmp.dtAnnIn.NewRow();
                                    row["id"] = site_id;
                                    row["Entreprise"] = webform_tmp.one_site["ENTREPRISE"].ToString();
                                    row["source"] = htmlDoc.body.innerHTML;
                                    row["source_hash"] = webform_tmp.CreateMD5Hash(htmlDoc.title);
                                    webform_tmp.dtAnnIn.Rows.Add(row);
                                    */
                                    //webform_tmp.dtAnnIn.Rows.Add(site_id, webform_tmp.one_site["ENTREPRISE"].ToString(), htmlDoc.body.InnerHtml, webform_tmp.CreateMD5Hash(htmlDoc.body.InnerHtml));
                                    if (webform_tmp.extract && site_id != 0)
                                    {
                                        bool insertdone = false;
                                        insertdone = SQL.Requet(@"insert into pages (id_site, source, source_hash) values (" + site_id + ", '"
                                            + htmlDoc.body.innerHTML.Replace("'", "''") + "', '" + webform_tmp.CreateMD5Hash(htmlDoc.body.innerText.ToString()) + "')");
                                        if (insertdone)
                                        {
                                            webform_tmp.allanonnces.Add(htmlDoc.body.innerHTML.ToString());
                                        }
                                        else
                                        {
                                            WebForm.insertfail++;
                                            if (WebForm.insertfail > 5 && webform_tmp.one_site["MAXPAGES"].ToString() == "99")
                                                webform_tmp.webClose();
                                        }
                                    }

                                }
                                catch (Exception exc)
                                {
                                    Console.WriteLine("testrun : "+exc.Message);
                                }
                                //SQL.Disconnect();
                                try
                                {
                                    ie.Quit();
                                }
                                catch
                                {

                                }
                                return;
                            }
                        }
                    }

                }
                Thread.Sleep(1000);
            }
        }

    }

    class backgroud
    {
        private WebForm bform;

        
        public backgroud(WebForm bf)
        {
            bform = bf;
        }

        public void runback()
        {
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine(Program.taleoclose.ToString());
                if (Program.taleoclose == 5)
                    return;
                Thread.Sleep(500);
            }
            bform.webClose();
            return;
        }
    }
}

