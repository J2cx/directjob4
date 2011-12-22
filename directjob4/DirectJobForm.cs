using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using mshtml;
using System.IO;
using System.Runtime.InteropServices;
using System.Net;
//using SHDocVw;
using DirectJobsLibrary;

namespace directjob4
{
    public partial class DirectJobForm : Form
    {
        public static Hashtable Parametres = new Hashtable();
        private string TypeAnnonce="", TypeSecteur="";
        public static bool BTest;
        public static DataTable DtAnnForm = new DataTable();
        public static bool IfExtract = false;

        //private MyListBox aMyListBox;

        public DirectJobForm()
        {
            InitializeComponent();
        }

        #region Methods
        
        //
        //Get the parametres
        private void GetParametres()
        {
            if (comboBoxTypeAnnonce.SelectedItem != null)
            {
                TypeAnnonce = comboBoxTypeAnnonce.SelectedItem.ToString();
            }
      
            if (comboBoxTypeSecteur.SelectedItem != null)
            {
                TypeSecteur = comboBoxTypeSecteur.SelectedItem.ToString();
            }

            if (Parametres.Count == 0)
            {
                Parametres.Add("URL_ACCUEIL", richTextBoxPageAccueil.Text);
                Parametres.Add("ADRESSE", richTextBoxAdresse.Text);
                Parametres.Add("ENTREPRISE", textBoxEntreprise.Text);
                Parametres.Add("TANNONCEUR", TypeAnnonce);
                Parametres.Add("TSECTEUR", TypeSecteur);
                
                if (textBoxMaxpages.Text != "0")
                    Parametres.Add("MAXPAGES", textBoxMaxpages.Text);
                else
                    Parametres.Add("MAXPAGES", "99");
                Parametres.Add("PAGE_DELAY", textBoxPageDelay.Text);

                Parametres.Add("Acces_Direct", checkBoxAccesDirect.Checked);
                string s_infirstpage = "";
                for (int i = 0; i < listBoxInFirstPage.Items.Count; i++)
                {
                    s_infirstpage += (listBoxInFirstPage.Items[i] + " ;;;");
                }
                Parametres.Add("InFirstPage", s_infirstpage);
      
                string s_exfirstpage = "";
                for (int i = 0; i < listBoxExFirstPage.Items.Count; i++)
                {
                    s_exfirstpage += (listBoxExFirstPage.Items[i] + " ;;;");
                }
                Parametres.Add("ExFirstPage", s_exfirstpage);

                Parametres.Add("Lien_Constant", checkBoxLienConstant.Checked);
                string s_inpagesuivant = "";
                for (int i = 0; i < listBoxInPageSuivant.Items.Count; i++)
                {
                    s_inpagesuivant += (listBoxInPageSuivant.Items[i] + " ;;;");
                }
                Parametres.Add("InPageSuivant", s_inpagesuivant);
                string s_expagesuivant = "";
                for (int i = 0; i < listBoxExPageSuivant.Items.Count; i++)
                {
                    s_expagesuivant += (listBoxExPageSuivant.Items[i] + " ;;;");
                }
                Parametres.Add("ExPageSuivant", s_expagesuivant);
                string s_inannonce = "";
                for (int i = 0; i < listBoxInAnnonce.Items.Count; i++)
                {
                    s_inannonce += (listBoxInAnnonce.Items[i] + " ;;;");
                }
                Parametres.Add("InAnnonce", s_inannonce);
                string s_exannonce = "";
                for (int i = 0; i < listBoxExAnnonce.Items.Count; i++)
                {
                    s_exannonce += (listBoxExAnnonce.Items[i] + " ;;;");
                }
                Parametres.Add("ExAnnonce", s_exannonce);
                Parametres.Add("FullPage", checkBoxFullPage.Checked);
                string s_ingoback = "";
                for (int i = 0; i < listBoxInGoBack.Items.Count; i++)
                {
                    s_ingoback += (listBoxInGoBack.Items[i] + " ;;;");
                }
                Parametres.Add("InGoBack", s_ingoback);
                string s_exgoback = "";
                for (int i = 0; i < listBoxExGoBack.Items.Count; i++)
                {
                    s_exgoback += (listBoxExGoBack.Items[i] + " ;;;");
                }
                Parametres.Add("ExGoBack", s_exgoback);
                Parametres.Add("GoBack", checkBoxBack.Checked);
                Parametres.Add("OnePage", checkBoxOnePage.Checked);
                Parametres.Add("AnnExist", checkBoxAnnExist.Checked);
                Parametres.Add("taleo", checkBoxTaleo.Checked);
                Parametres.Add("NextAnn", textBoxNextAnn.Text);
                Parametres.Add("PopUp", false);
                if (checkBoxTri.Checked)
                {
                    Parametres.Add("tri", textBoxClicknb.Text);
                    string s_intri = "";
                    for (int i = 0; i < listBoxIntri.Items.Count; i++)
                    {
                        s_intri += (listBoxIntri.Items[i] + " ;;;");
                    }
                    Parametres.Add("Intri", s_intri);
                    string s_extri = "";
                    for (int i = 0; i < listBoxExtri.Items.Count; i++)
                    {
                        s_extri += (listBoxExtri.Items[i] + " ;;;");
                    }
                    Parametres.Add("Extri", s_extri);
                }
                else
                {
                    Parametres.Add("tri", "0");
                }
            }
            else
            {
                Parametres["URL_ACCUEIL"] = richTextBoxPageAccueil.Text;
                //Parametres["TSITE"] = textBoxTsite.Text;
                Parametres["ADRESSE"] = richTextBoxAdresse.Text;
                Parametres["ENTREPRISE"] = textBoxEntreprise.Text;
                Parametres["TANNONCEUR"] = TypeAnnonce;
                Parametres["TSECTEUR"] = TypeSecteur;
                //Parametres["PARAM"] = textBoxAnnonce.Text;
                //Parametres["PAGINATION"] = textBoxPageSuivant.Text;
                if (textBoxMaxpages.Text != "0")
                    Parametres["MAXPAGES"] = textBoxMaxpages.Text;
                else
                    Parametres["MAXPAGES"] = "99";
                Parametres["PAGE_DELAY"] = textBoxPageDelay.Text;

                Parametres["Acces_Direct"] = checkBoxAccesDirect.Checked;
                string s_infirstpage = "";
                for (int i = 0; i < listBoxInFirstPage.Items.Count; i++)
                {
                    s_infirstpage += (listBoxInFirstPage.Items[i] + " ;;;");
                }
                Parametres["InFirstPage"] = s_infirstpage;
                //MessageBox.Show(Parametres["FirstPage"].ToString());
                string s_exfirstpage = "";
                for (int i = 0; i < listBoxExFirstPage.Items.Count; i++)
                {
                    s_exfirstpage += (listBoxExFirstPage.Items[i] + " ;;;");
                }
                Parametres["ExFirstPage"] = s_exfirstpage;
                Parametres["Lien_Constant"] = checkBoxLienConstant.Checked;
                string s_inpagesuivant = "";
                for (int i = 0; i < listBoxInPageSuivant.Items.Count; i++)
                {
                    s_inpagesuivant += (listBoxInPageSuivant.Items[i] + " ;;;");
                }
                Parametres["InPageSuivant"] = s_inpagesuivant;
                string s_expagesuivant = "";
                for (int i = 0; i < listBoxExPageSuivant.Items.Count; i++)
                {
                    s_expagesuivant += (listBoxExPageSuivant.Items[i] + " ;;;");
                }
                Parametres["ExPageSuivant"] = s_expagesuivant;
                string s_inannonce = "";
                for (int i = 0; i < listBoxInAnnonce.Items.Count; i++)
                {
                    s_inannonce += (listBoxInAnnonce.Items[i] + " ;;;");
                }
                Parametres["InAnnonce"] = s_inannonce;
                //MessageBox.Show(Parametres["FirstPage"].ToString());
                string s_exannonce = "";
                for (int i = 0; i < listBoxExAnnonce.Items.Count; i++)
                {
                    s_exannonce += (listBoxExAnnonce.Items[i] + " ;;;");
                }
                Parametres["ExAnnonce"] = s_exannonce;
                Parametres["FullPage"] = checkBoxFullPage.Checked;
                string s_ingoback = "";
                for (int i = 0; i < listBoxInGoBack.Items.Count; i++)
                {
                    s_ingoback += (listBoxInGoBack.Items[i] + " ;;;");
                }
                Parametres["InGoBack"] = s_ingoback;
                //MessageBox.Show(Parametres["FirstPage"].ToString());
                string s_exgoback = "";
                for (int i = 0; i < listBoxExGoBack.Items.Count; i++)
                {
                    s_exgoback += (listBoxExGoBack.Items[i] + " ;;;");
                }
                Parametres["ExGoBack"] = s_exgoback;
                Parametres["GoBack"] = checkBoxBack.Checked;
                Parametres["OnePage"] = checkBoxOnePage.Checked;
                Parametres["AnnExist"] = checkBoxAnnExist.Checked;
                Parametres["taleo"] = checkBoxTaleo.Checked;
                Parametres["NextAnn"] = textBoxNextAnn.Text;
                Parametres["PopUp"] = false;
                if (checkBoxTri.Checked)
                {
                    Parametres["tri"] = textBoxClicknb.Text;
                    string s_intri = "";
                    for (int i = 0; i < listBoxIntri.Items.Count; i++)
                    {
                        s_intri += (listBoxIntri.Items[i] + " ;;;");
                    }
                    Parametres["Intri"] = s_intri;
                    string s_extri = "";
                    for (int i = 0; i < listBoxExtri.Items.Count; i++)
                    {
                        s_extri += (listBoxExtri.Items[i] + " ;;;");
                    }
                    Parametres["Extri"] = s_extri;
                }
                else
                {
                    Parametres["tri"] = "0";
                }
            }
        }

        //
        //methods for css
        //BtnTest0 : clear the css  directjob_test
        //BtnTest1 : Add the class directjob_test
        //BtnTest2 : the css for the class directjob_test
        #region css
        
        private void BtnTest0(HtmlElement link)
        {
            string orig_class = (string)link.GetAttribute("className");
            if (orig_class.Contains("directjob_test"))
            {
                string ss = orig_class.Replace(" directjob_test", "");
                link.SetAttribute("className", ss);
                //MessageBox.Show(link.GetAttribute("className"));
            }

        }

        private void BtnTest1(HtmlElement link)
        {
            string orig_class = (string)link.GetAttribute("className");

            link.SetAttribute("className", orig_class + " " + "directjob_test");
            //link.Focus();
            //MessageBox.Show(link.GetAttribute("classname").ToString());
            IHTMLElement myele = (IHTMLElement)link.DomElement;
            IHTMLDOMNode n1 = (IHTMLDOMNode)myele;

            //MessageBox.Show(n1.nodeName);
            IHTMLAttributeCollection attrcol = (IHTMLAttributeCollection)n1.attributes;

            treeView1.Nodes.Add(myele.tagName);
            treeView1.Nodes[treeView1.Nodes.Count - 1].Nodes.Add("Attributes");
            if (attrcol != null)
                foreach (IHTMLDOMAttribute iatt1 in attrcol)
                {
                    if (iatt1.nodeValue != null && (iatt1.nodeValue.ToString() != "") && myele.outerHTML.Contains(iatt1.nodeName))
                    //MessageBox.Show("att1 name : "+iatt1.nodeName +"\natt1 value : "+ iatt1.nodeValue.ToString());
                    {
                        //MessageBox.Show("att1 name : " + iatt1.nodeName + "\natt1 value : " + iatt1.nodeValue.ToString());
                        treeView1.Nodes[treeView1.Nodes.Count - 1].Nodes[0].Nodes.Add(iatt1.nodeName, iatt1.nodeName + "=" + iatt1.nodeValue.ToString());
                        treeView1.Nodes[treeView1.Nodes.Count - 1].Nodes[0].LastNode.ToolTipText = iatt1.nodeValue.ToString();
                    }
                }
            if (myele.style.cssText != null && myele.style.cssText.Length > 0)
            {
                treeView1.Nodes[treeView1.Nodes.Count - 1].Nodes.Add("style", "sytle=" + myele.style.cssText);
                treeView1.Nodes[treeView1.Nodes.Count - 1].LastNode.ToolTipText = myele.style.cssText;
            }
            if (myele.outerText != null && myele.outerText.Length > 0)
            {
                treeView1.Nodes[treeView1.Nodes.Count - 1].Nodes.Add("outerText", "outerText=" + myele.outerText);
                treeView1.Nodes[treeView1.Nodes.Count - 1].LastNode.ToolTipText = myele.outerText;
            }
            if (myele.outerHTML != null && myele.outerHTML.ToString().Length > 0)
            {
                treeView1.Nodes[treeView1.Nodes.Count - 1].Nodes.Add("outerHTML", "outerHTML=" + myele.outerHTML.ToString());
                treeView1.Nodes[treeView1.Nodes.Count - 1].LastNode.ToolTipText = myele.outerHTML.ToString();
            }
        }

        private void BtnTest2()
        {
            IHTMLDocument2 currentDocument = (IHTMLDocument2)webBrowser1.Document.DomDocument;

            int length = currentDocument.styleSheets.length;
            IHTMLStyleSheet styleSheet = currentDocument.createStyleSheet(@"", length + 1);
            styleSheet.cssText = @"a.directjob_test {colore:blue;border-style:solid;border-width:1px;border-color:blue;} 
                                    input.directjob_test {colore:blue;border-style:solid;border-width:1px;border-color:blue;}
                                    .directjob_test {colore:blue;border-style:solid;border-width:1px;border-color:blue;} ";
        }
        
        #endregion  

        #region TreeViwColors

        private void TreeViewColor(object sender)
        {

            try
            {
                if (((TreeView)sender).Nodes.Count > 0)
                    foreach (TreeNode tn in ((TreeView)sender).Nodes)
                    {
                        if (tn.Text != null)
                        {
                            int nbnum = 0;
                            foreach (char ch in tn.Text)
                            {
                                try
                                {
                                    int tmp = Convert.ToInt32(ch);
                                    if (tmp >= 0 && tmp <= 9)
                                    {
                                        nbnum++;
                                        // MessageBox.Show(tmp.ToString());
                                    }
                                    else
                                    {
                                        nbnum = 0;
                                    }
                                    if (nbnum >= 3)
                                        tn.ForeColor = Color.Red;
                                }
                                catch
                                { }
                            }
                            if (nbnum >= 3)
                                tn.ForeColor = Color.Red;
                        }
                        TreeNodeColor(tn);
                    }
            }
            catch (Exception exc)
            {
                Console.WriteLine("treeViewColor : " + exc.Message);
            }
        }

        private void TreeNodeColor(object sender)
        {
            try
            {
                if (((TreeNode)sender).Nodes.Count > 0)
                    foreach (TreeNode tn in ((TreeNode)sender).Nodes)
                    {
                        if (tn.Text != null)
                        {
                            int nbnum = 0;
                            foreach (char ch in tn.Text)
                            {
                                try
                                {
                                    int tmp = Convert.ToInt32(ch);
                                    if (tmp >= 48 && tmp <= 57)
                                    {
                                        nbnum++;
                                        //MessageBox.Show(tmp.ToString());
                                    }
                                    else
                                    {
                                        nbnum = 0;
                                    }
                                    if (nbnum >= 3)
                                        tn.ForeColor = Color.Red;
                                }
                                catch
                                { }
                            }

                        }
                        TreeNodeColor(tn);
                    }
            }
            catch (Exception exc)
            {
                Console.WriteLine("treeNodeColor : " + exc.Message);
            }
        }

        #endregion

        public void ChercheByTwoListBox(ListBox inListBox, ListBox exListBox, Panel panelRight, Panel panelWrong, TextBox textBoxTrouve, GroupBox trueShowGroupBox, GroupBox falseShowGroupBox)
        {
            if (inListBox == null || exListBox == null)
                return;

            string[] str_separators = new string[] { ":::", " ;;;" };

            string[] strs_in;
            string s_infirstpage = "";
            for (int i = 0; i < inListBox.Items.Count; i++)
            {
                s_infirstpage += (inListBox.Items[i] + " ;;;");
            }
            strs_in = s_infirstpage.Split(str_separators, StringSplitOptions.RemoveEmptyEntries);
            string[] strs_ex;
            string s_exfirstpage = "";
            for (int i = 0; i < exListBox.Items.Count; i++)
            {
                s_exfirstpage += (exListBox.Items[i] + " ;;;");
            }
            strs_ex = s_exfirstpage.Split(str_separators, StringSplitOptions.RemoveEmptyEntries);

            int nbfind = 0;

            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();

            if (webBrowser1.Document != null)
                foreach (HtmlElement link in webBrowser1.Document.All)
                {
                    BtnTest0(link);
                    bool findannonce = true;
                    IHTMLElement4 ilink4 = (IHTMLElement4)link.DomElement;
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
                            if (ilink4.getAttributeNode(strs_ex[tmp2 * 2]).nodeValue.ToString().Contains(strs_ex[tmp2 * 2 + 1]))
                            {
                                findannonce = false;
                                break;
                            }
                        }

                        else if (link.GetAttribute(strs_ex[tmp2 * 2]).Contains(strs_ex[tmp2 * 2 + 1]))
                        {
                            findannonce = false;
                            break;
                        }

                    }
                    if (findannonce)
                    {
                        BtnTest1(link);
                        try
                        {
                            if (webBrowser1.Document != null)
                            {
                                BtnTest1(link);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("btn cherche " + ex.Message);
                        }
                    }
                }
            treeView1.ExpandAll();
            treeView1.EndUpdate();

            if (panelRight != null && panelWrong != null)
            {
                if (nbfind == 1)
                {
                    panelRight.Visible = true;
                    panelWrong.Visible = false;
                }
                else
                {
                    panelRight.Visible = false;
                    panelWrong.Visible = true;
                }
            }
            if (trueShowGroupBox != null && nbfind == 1)
            {
                trueShowGroupBox.Visible = true;
            }
            if (falseShowGroupBox != null && nbfind != 1)
            {
                falseShowGroupBox.Visible = true;
            }
            textBoxTrouve.Text = nbfind.ToString();
        }

        #endregion

        #region Events

        //-----------------------------------------------
        //Events General

        //
        //Form1 Load
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxTypeAnnonce.SelectedIndex = 0;
            comboBoxTypeSecteur.SelectedIndex = 0;
            this.MinimumSize = new Size(buttonGo.Location.X + buttonGo.Width + buttonGetElement.Width + 20, tabControl1.Height + textBoxurl.Height + 60);
            Form1_SizeChanged(sender, null);
        }

        //
        //The Events for size changed
        //Form1 Size Changed and the splitContainer Splitter Moved
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            splitContainer2.FixedPanel = FixedPanel.Panel1;
            splitContainer2.SetBounds(splitContainer2.Location.X, splitContainer2.Location.Y, this.Width - 25, this.Height - 70);
            treeView1.SetBounds(treeView1.Location.X, treeView1.Location.Y, this.Width - tabControl1.Width - splitContainer2.Panel1.Width - 45, this.Height - 70);
            splitContainer2.FixedPanel = FixedPanel.None;
        }

        private void SplitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            treeView1.SetBounds(treeView1.Location.X, treeView1.Location.Y, this.Width - tabControl1.Width - splitContainer2.Panel1.Width - 45, this.Height - 70);
        }

        //
        //buttons for webbrowser, Button Go, Back, Forward, Refresh 
        //
        //when webBrowser completed, add the css for the class 'directjob_test' 
        //And the Event for press 'Enter' to Navigate
        #region Events_WebBrowser
        
        private void ButtonForward_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
                webBrowser1.GoForward();
        }

        private void ButtonGo_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(textBoxurl.Text);
            if (textBoxurl.Text.Contains("taleo"))
            {
                checkBoxTaleo.Checked = true;
            }
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
                webBrowser1.GoBack();
        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            BtnTest2();
        }

        private void textBox_url_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ButtonGo_Click(null, null);
        }

        #endregion
        //
        //Button Get Element Click
        private void buttonGetElement_Click(object sender, EventArgs e)
        {
            if (webBrowser1.Document != null)
            {
                treeView1.BeginUpdate();
                treeView1.Nodes.Clear();

                //HtmlElement myele1 = webBrowser1.Document.ActiveElement;
                IHTMLElement myele = (IHTMLElement)webBrowser1.Document.ActiveElement.DomElement;
                IHTMLDOMNode n1 = (IHTMLDOMNode)myele;

                //MessageBox.Show(n1.nodeName);
                IHTMLAttributeCollection attrcol = (IHTMLAttributeCollection)n1.attributes;

                treeView1.Nodes.Add(myele.tagName);
                treeView1.Nodes[0].Nodes.Add("Attributes");
                foreach (IHTMLDOMAttribute iatt1 in attrcol)
                {
                    if (iatt1.nodeValue != null && (iatt1.nodeValue.ToString() != "") && myele.outerHTML.Contains(iatt1.nodeName))
                    //MessageBox.Show("att1 name : "+iatt1.nodeName +"\natt1 value : "+ iatt1.nodeValue.ToString());
                    {
                        //MessageBox.Show("att1 name : " + iatt1.nodeName + "\natt1 value : " + iatt1.nodeValue.ToString());
                        treeView1.Nodes[0].Nodes[0].Nodes.Add(iatt1.nodeName, iatt1.nodeName + "=" + iatt1.nodeValue.ToString());
                        treeView1.Nodes[0].Nodes[0].LastNode.ToolTipText = iatt1.nodeValue.ToString();
                    }
                }
                if (myele.style.cssText != null && myele.style.cssText.Length > 0)
                {
                    treeView1.Nodes[0].Nodes.Add("style", "sytle=" + myele.style.cssText);
                    treeView1.Nodes[0].LastNode.ToolTipText = myele.style.cssText;
                }
                if (myele.outerText != null && myele.outerText.Length > 0)
                {
                    treeView1.Nodes[0].Nodes.Add("outerText", "outerText=" + myele.outerText);
                    treeView1.Nodes[0].LastNode.ToolTipText = myele.outerText;
                }
                if (myele.outerHTML != null && myele.outerHTML.ToString().Length > 0)
                {
                    treeView1.Nodes[0].Nodes.Add("outerHTML", "outerHTML=" + myele.outerHTML.ToString());
                    treeView1.Nodes[0].LastNode.ToolTipText = myele.outerHTML.ToString();
                }

                treeView1.ExpandAll();
                treeView1.EndUpdate();
            }
        }

        //
        //Button Checher click
        #region Events_Button_Cherhche
        
        private void ButtonCherche1_Click(object sender, EventArgs e)
        {
            ChercheByTwoListBox(listBoxInFirstPage, listBoxExFirstPage, panel1, panel2, textBoxnbTrouve1, null, groupBox4);
        }

        private void ButtonCherche2_Click(object sender, EventArgs e)
        {
            ChercheByTwoListBox(listBoxInPageSuivant, listBoxExPageSuivant, panel3, panel4, textBoxnbTrouve2, null, groupBox5);
        }

        private void ButtonCherche3_Click(object sender, EventArgs e)
        {
            ChercheByTwoListBox(listBoxInAnnonce, listBoxExAnnonce, null, null, textBoxnbTrouve3, groupBox6, groupBox6);
        }

        private void ButtonCherche4_Click(object sender, EventArgs e)
        {
            ChercheByTwoListBox(listBoxInGoBack, listBoxExGoBack, null, null, textBoxnbTrouve4, groupBox5, groupBox6);
        }

        private void ButtonCherche5_Click(object sender, EventArgs e)
        {
            ChercheByTwoListBox(listBoxIntri, listBoxExtri, panel9, panel10, textBoxnbTrouve5, null, null);
        }

        private void ButtonChercheTaleo_Click(object sender, EventArgs e)
        {
            string strs_in;
            strs_in = textBoxNextAnn.Text;

            //Form1.Parametres["InFirstPage"].ToString().
            int nbfind = 0;
            //richTextBox_Cherche3.Text = "";
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();

            if (webBrowser1.Document != null)
                foreach (HtmlElement link in webBrowser1.Document.All)
                {
                    BtnTest0(link);
                    bool findannonce = true;
                    IHTMLElement4 ilink4 = (IHTMLElement4)link.DomElement;
                    if (!link.GetAttribute("id").Contains(strs_in))
                    {
                        findannonce = false;
                    }

                    if (findannonce)
                    {

                        nbfind++;
                        try
                        {
                            if (webBrowser1.Document != null)
                            {
                                BtnTest1(link);

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("btn cherche 5 " + ex.Message);
                        }
                    }

                }

            treeView1.ExpandAll();
            treeView1.EndUpdate();

            textBoxnbTrouve5.Text = nbfind.ToString();
            //groupBox6.Visible = true;
            if (nbfind == 1)
            {
                panel7.Visible = true;
                panel8.Visible = false;

            }
            else
            {
                panel7.Visible = false;
                panel8.Visible = true;
            }
        }
        
        #endregion

        //
        //ListBox Events 
        //Mouse Down for contextMenuStrip
        //Mouse Move for toolTip
        #region Events_ListBox

        private void listBox_MouseDown(object sender, MouseEventArgs e)
        {
            string activeControlType = "";
            object oActiveControl;
            if (sender is SplitContainer)
            {
                activeControlType = ((SplitContainer)sender).ActiveControl.GetType().ToString();
                oActiveControl = ((SplitContainer)sender).ActiveControl;
            }
            else
            {
                activeControlType = sender.GetType().ToString();
                oActiveControl = sender;
            }
            if (activeControlType == "System.Windows.Forms.ListBox")
            {
                ListBox listBox = (ListBox)oActiveControl;
                listBox.Focus();
                //listBox_ExAnnonce.Focus();
                if (e.Button != MouseButtons.Right) return;
                if (webBrowser1.Document != null)
                {
                    int indexover = listBox.IndexFromPoint(e.X, e.Y);
                    if (indexover >= 0 && indexover < listBox.Items.Count)
                    {
                        listBox.SelectedIndex = indexover;
                    }
                    if (listBox.SelectedItem != null)
                    {
                        contextMenuStrip3.Items[1].Enabled = true;
                        contextMenuStrip3.Items[2].Enabled = true;
                    }
                    else
                    {
                        contextMenuStrip3.Items[1].Enabled = false;
                        contextMenuStrip3.Items[2].Enabled = false;
                    }
                    contextMenuStrip3.Show(listBox, e.X, e.Y);

                }
            }
        }

        private void listBox_Move(object sender, MouseEventArgs e)
        {

            string activeControlType = "";
            object oActiveControl;
            if (sender is SplitContainer)
            {
                activeControlType = ((SplitContainer)sender).ActiveControl.GetType().ToString();
                oActiveControl = ((SplitContainer)sender).ActiveControl;
            }
            else
            {
                activeControlType = sender.GetType().ToString();
                oActiveControl = sender;
            }
            if (activeControlType == "System.Windows.Forms.ListBox")
            {
                ListBox listBox = (ListBox)oActiveControl;
                Point point = new Point(e.X, e.Y);

                int hoverIndex = listBox.IndexFromPoint(point);
                if (hoverIndex >= 0 && hoverIndex < listBox.Items.Count)
                {
                    toolTip1.Active = true;
                    if (toolTip1.GetToolTip(listBox) != listBox.Items[hoverIndex].ToString())
                        //toolTip1.SetToolTip(listBox,"haha");
                        toolTip1.SetToolTip(listBox, listBox.Items[hoverIndex].ToString());
                    //toolTip1.Show(listBox.Items[hoverIndex].ToString(), listBox);
                }
                else
                {
                    toolTip1.Hide(listBox);
                }
            }

        }
        
        #endregion

        //
        //contextMenuStrip Items Click
        //contextMenuStrip1, Get Element Click for webbrowser1
        //
        //contextMenuStrip2, for treeView1
        //Inclure, Exclure, EditInclure, EditExculure, Go
        //
        //contextMenuStrip3, for listBox
        //Add, Modify, delete
        #region Events_Menu

        private void menuItem_GetElement_Click(object sender, EventArgs e)
        {
            if (webBrowser1.Document != null)
            {
                treeView1.BeginUpdate();
                treeView1.Nodes.Clear();


                IHTMLElement myele = (IHTMLElement)webBrowser1.Document.ActiveElement.DomElement;
                IHTMLDOMNode n1 = (IHTMLDOMNode)myele;

                //MessageBox.Show(n1.nodeName);
                IHTMLAttributeCollection attrcol = (IHTMLAttributeCollection)n1.attributes;

                treeView1.Nodes.Add(myele.tagName);
                treeView1.Nodes[0].Nodes.Add("Attributes");
                foreach (IHTMLDOMAttribute iatt1 in attrcol)
                {
                    if (iatt1.nodeValue != null && (iatt1.nodeValue.ToString() != "") && myele.outerHTML.Contains(iatt1.nodeName))
                    //MessageBox.Show("att1 name : "+iatt1.nodeName +"\natt1 value : "+ iatt1.nodeValue.ToString());
                    {
                        //MessageBox.Show("att1 name : " + iatt1.nodeName + "\natt1 value : " + iatt1.nodeValue.ToString());
                        treeView1.Nodes[0].Nodes[0].Nodes.Add(iatt1.nodeName, iatt1.nodeName + "=" + iatt1.nodeValue.ToString());
                        treeView1.Nodes[0].Nodes[0].LastNode.ToolTipText = iatt1.nodeValue.ToString();
                    }
                }
                if (myele.style.cssText != null && myele.style.cssText.Length > 0)
                {
                    treeView1.Nodes[0].Nodes.Add("style", "sytle=" + myele.style.cssText);
                    treeView1.Nodes[0].LastNode.ToolTipText = myele.style.cssText;
                }
                if (myele.outerText != null && myele.outerText.Length > 0)
                {
                    treeView1.Nodes[0].Nodes.Add("outerText", "outerText=" + myele.outerText);
                    treeView1.Nodes[0].LastNode.ToolTipText = myele.outerText;
                }
                if (myele.outerHTML != null && myele.outerHTML.ToString().Length > 0)
                {
                    treeView1.Nodes[0].Nodes.Add("outerHTML", "outerHTML=" + myele.outerHTML.ToString());
                    treeView1.Nodes[0].LastNode.ToolTipText = myele.outerHTML.ToString();
                }

                treeView1.ExpandAll();
                treeView1.EndUpdate();
            }
            //treeNodeColor(treeView1);
        }

        private void menuItem_Inclure_Click(object sender, EventArgs e)
        {
            string s_store = "";
            if (!checkBoxTaleo.Checked)
            {
                s_store = treeView1.SelectedNode.Name + ":::" + treeView1.SelectedNode.ToolTipText;//textBox_AttFirstPage.Text + ":::" + textBox_FirstPage.Text;

                if (tabControl1.SelectedTab == tabControl1.TabPages[1])
                    if (tabControl2.SelectedTab == tabControl2.TabPages[0])
                    {
                        if (treeView1.SelectedNode.Name != "" && treeView1.SelectedNode.Name != "" && !listBoxInFirstPage.Items.Contains(s_store) && !checkBoxTri.Checked)
                            listBoxInFirstPage.Items.Add(s_store);
                        else if (treeView1.SelectedNode.Name != "" && treeView1.SelectedNode.Name != "" && !listBoxInFirstPage.Items.Contains(s_store) && checkBoxTri.Checked)
                        {
                            listBoxIntri.Items.Add(s_store);
                        }
                    }
                    else if (tabControl2.SelectedTab == tabControl2.TabPages[1])
                    {
                        if (treeView1.SelectedNode.Name != "" && treeView1.SelectedNode.Name != "" && !listBoxInPageSuivant.Items.Contains(s_store))
                            listBoxInPageSuivant.Items.Add(s_store);
                    }
                    else if (tabControl2.SelectedTab == tabControl2.TabPages[2])
                    {
                        if (treeView1.SelectedNode.Name != "" && treeView1.SelectedNode.Name != "" && !listBoxInAnnonce.Items.Contains(s_store) && (!checkBoxFullPage.Checked || checkBoxBack.Checked))
                            listBoxInAnnonce.Items.Add(s_store);
                        if (treeView1.SelectedNode.Name != "" && treeView1.SelectedNode.Name != "" && !listBoxInGoBack.Items.Contains(s_store) && (checkBoxFullPage.Checked && !checkBoxBack.Checked))
                            listBoxInGoBack.Items.Add(s_store);
                    }
            }
            else
            {
                if (tabControl1.SelectedTab == tabControl1.TabPages[1] && treeView1.SelectedNode.Name == "id")
                {
                    s_store = treeView1.SelectedNode.ToolTipText;
                    textBoxNextAnn.Text = s_store;
                }
            }
        }

        private void menuItem_Exclure_Click(object sender, EventArgs e)
        {
            string s_store = treeView1.SelectedNode.Name + ":::" + treeView1.SelectedNode.ToolTipText;//textBox_AttFirstPage.Text + ":::" + textBox_FirstPage.Text;

            if (tabControl1.SelectedTab == tabControl1.TabPages[1])
                if (tabControl2.SelectedTab == tabControl2.TabPages[0])
                {
                    if (treeView1.SelectedNode.Name != "" && treeView1.SelectedNode.Name != "" && !listBoxExFirstPage.Items.Contains(s_store) && !checkBoxTri.Checked)
                        listBoxExFirstPage.Items.Add(s_store);
                    else if (treeView1.SelectedNode.Name != "" && treeView1.SelectedNode.Name != "" && !listBoxExFirstPage.Items.Contains(s_store) && checkBoxTri.Checked)
                        listBoxExtri.Items.Add(s_store);
                }
                else if (tabControl2.SelectedTab == tabControl2.TabPages[1])
                {
                    if (treeView1.SelectedNode.Name != "" && treeView1.SelectedNode.Name != "" && !listBoxExPageSuivant.Items.Contains(s_store))
                        listBoxExPageSuivant.Items.Add(s_store);
                }
                else if (tabControl2.SelectedTab == tabControl2.TabPages[2])
                {
                    if (treeView1.SelectedNode.Name != "" && treeView1.SelectedNode.Name != "" && !listBoxExAnnonce.Items.Contains(s_store) && (!checkBoxFullPage.Checked || checkBoxBack.Checked))
                        listBoxExAnnonce.Items.Add(s_store);
                    if (treeView1.SelectedNode.Name != "" && treeView1.SelectedNode.Name != "" && !listBoxExGoBack.Items.Contains(s_store) && (checkBoxFullPage.Checked && !checkBoxBack.Checked))
                        listBoxExGoBack.Items.Add(s_store);
                }
        }

        private void menuItem_EditInclure_Click(object sender, EventArgs e)
        {
            string s_store = treeView1.SelectedNode.Name + ":::" + treeView1.SelectedNode.ToolTipText;//textBox_AttFirstPage.Text + ":::" + textBox_FirstPage.Text;

            /*
            if (this.ActiveControl is ListBox)
            {
                //Console.WriteLine(sender.GetType().ToString() + " active control : " + this.ActiveControl.ToString());
                ListBox listBox = (ListBox)this.ActiveControl;
                Modify modifyform = new Modify();
                if (listBox.SelectedItem != null)
                    modifyform.load(listBox.SelectedItem.ToString());
                modifyform.ShowDialog();
                //MessageBox.Show(modifyform.ret_str);
                if (modifyform.ret_str.Length > 3 && !listBox.Items.Contains(modifyform.ret_str))
                    listBox.Items.Add(modifyform.ret_str);
            }
             */
            Modify modifyform = new Modify();
            modifyform.load(s_store);
            modifyform.ShowDialog();

            if (tabControl1.SelectedTab == tabControl1.TabPages[1])
                if (tabControl2.SelectedTab == tabControl2.TabPages[0])
                {
                    if (modifyform.ret_str.Length > 3 && !listBoxInFirstPage.Items.Contains(modifyform.ret_str) && !checkBoxTri.Checked)
                        listBoxInFirstPage.Items.Add(modifyform.ret_str);
                    else if (modifyform.ret_str.Length > 3 && !listBoxInFirstPage.Items.Contains(modifyform.ret_str) && checkBoxTri.Checked)
                        listBoxIntri.Items.Add(modifyform.ret_str);
                }
                else if (tabControl2.SelectedTab == tabControl2.TabPages[1])
                {
                    if (modifyform.ret_str.Length > 3 && !listBoxInPageSuivant.Items.Contains(modifyform.ret_str))
                        listBoxInPageSuivant.Items.Add(modifyform.ret_str);
                }
                else if (tabControl2.SelectedTab == tabControl2.TabPages[2])
                {
                    if (modifyform.ret_str.Length > 3 && !listBoxInAnnonce.Items.Contains(modifyform.ret_str) && (!checkBoxFullPage.Checked || checkBoxBack.Checked))
                        listBoxInAnnonce.Items.Add(modifyform.ret_str);
                    if (modifyform.ret_str.Length > 3 && !listBoxInGoBack.Items.Contains(modifyform.ret_str) && (checkBoxFullPage.Checked && !checkBoxBack.Checked))
                        listBoxInGoBack.Items.Add(modifyform.ret_str);
                }
        }

        private void menuItem_EditExclure_Click(object sender, EventArgs e)
        {
            string s_store = treeView1.SelectedNode.Name + ":::" + treeView1.SelectedNode.ToolTipText;//textBox_AttFirstPage.Text + ":::" + textBox_FirstPage.Text;

            Modify modifyform = new Modify();
            modifyform.load(s_store);
            modifyform.ShowDialog();

            if (tabControl1.SelectedTab == tabControl1.TabPages[1])
                if (tabControl2.SelectedTab == tabControl2.TabPages[0])
                {
                    if (modifyform.ret_str.Length > 3 && !listBoxExFirstPage.Items.Contains(modifyform.ret_str) && !checkBoxTri.Checked)
                        listBoxExFirstPage.Items.Add(modifyform.ret_str);
                    else if (modifyform.ret_str.Length > 3 && !listBoxExFirstPage.Items.Contains(modifyform.ret_str) && !checkBoxTri.Checked)
                        listBoxExtri.Items.Add(modifyform.ret_str);
                }
                else if (tabControl2.SelectedTab == tabControl2.TabPages[1])
                {
                    if (modifyform.ret_str.Length > 3 && !listBoxExPageSuivant.Items.Contains(modifyform.ret_str))
                        listBoxExPageSuivant.Items.Add(modifyform.ret_str);
                }
                else if (tabControl2.SelectedTab == tabControl2.TabPages[2])
                {
                    if (modifyform.ret_str.Length > 3 && !listBoxExAnnonce.Items.Contains(modifyform.ret_str) && (!checkBoxFullPage.Checked || checkBoxBack.Checked))
                        listBoxExAnnonce.Items.Add(modifyform.ret_str);
                    if (modifyform.ret_str.Length > 3 && !listBoxExGoBack.Items.Contains(modifyform.ret_str) && (checkBoxFullPage.Checked && !checkBoxBack.Checked))
                        listBoxExGoBack.Items.Add(modifyform.ret_str);
                }
        }

        private void menuItem_Go_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(treeView1.SelectedNode.Text);
            if (treeView1.SelectedNode.Text.Contains("taleo"))
            {
                checkBoxTaleo.Checked = true;
            }
        }

        private void menuItem_Add_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(sender.GetType().ToString()+" active control : "+this.ActiveControl.ToString());
            string activeControlType = "";
            object oActiveControl;
            if (this.ActiveControl is SplitContainer)
            {
                activeControlType = ((SplitContainer)this.ActiveControl).ActiveControl.GetType().ToString();
                oActiveControl = ((SplitContainer)this.ActiveControl).ActiveControl;
            }
            else
            {
                activeControlType = this.ActiveControl.GetType().ToString();
                oActiveControl = this.ActiveControl;
            }
            if (activeControlType == "System.Windows.Forms.ListBox")
            {
                ListBox listBox = (ListBox)oActiveControl;
                //Console.WriteLine(sender.GetType().ToString() + " active control : " + this.ActiveControl.ToString());

                Modify modifyform = new Modify();
                if (listBox.SelectedItem != null)
                    modifyform.load(listBox.SelectedItem.ToString());
                modifyform.ShowDialog();
                //MessageBox.Show(modifyform.ret_str);
                if (modifyform.ret_str.Length > 3 && !listBox.Items.Contains(modifyform.ret_str))
                    listBox.Items.Add(modifyform.ret_str);
            }

        }

        private void menuItem_Modify_Click(object sender, EventArgs e)
        {
            string activeControlType = "";
            object oActiveControl;
            if (this.ActiveControl is SplitContainer)
            {
                activeControlType = ((SplitContainer)this.ActiveControl).ActiveControl.GetType().ToString();
                oActiveControl = ((SplitContainer)this.ActiveControl).ActiveControl;
            }
            else
            {
                activeControlType = this.ActiveControl.GetType().ToString();
                oActiveControl = this.ActiveControl;
            }
            if (activeControlType == "System.Windows.Forms.ListBox")
            {
                ListBox listBox = (ListBox)oActiveControl;
                Modify modifyform = new Modify();
                if (listBox.SelectedItem != null)
                {
                    modifyform.load(listBox.SelectedItem.ToString());
                    modifyform.ShowDialog();
                    //MessageBox.Show(modifyform.ret_str);
                    if (!listBox.Items.Contains(modifyform.ret_str))
                        listBox.Items[listBox.SelectedIndex] = modifyform.ret_str;
                }
            }

        }

        private void menuItem_Delete_Click(object sender, EventArgs e)
        {
            string activeControlType = "";
            object oActiveControl;
            if (this.ActiveControl is SplitContainer)
            {
                activeControlType = ((SplitContainer)this.ActiveControl).ActiveControl.GetType().ToString();
                oActiveControl = ((SplitContainer)this.ActiveControl).ActiveControl;
            }
            else
            {
                activeControlType = this.ActiveControl.GetType().ToString();
                oActiveControl = this.ActiveControl;
            }
            if (activeControlType == "System.Windows.Forms.ListBox")
            {
                ListBox listBox = (ListBox)oActiveControl;
                if (listBox != null)
                    listBox.Items.Remove(listBox.SelectedItem);
            }

        }

        #endregion

        //
        //treeView1
        //BeforeExpand, for treeviewcolor
        //MouseDown, for call the contextMenuStrip2

        #region Events_TreeView

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeViewColor(sender);
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            treeView1.SelectedNode = treeView1.GetNodeAt(e.X, e.Y);
            //Point p1 = new Point(e.X,e.Y);
            //treeView1.SelectedNode = treeView1.Focused;
            if (treeView1.Nodes[0].Text != "URL of frames")
            {
                if (treeView1.SelectedNode != null && treeView1.SelectedNode.Parent != null && treeView1.SelectedNode.Parent.Text == "Attributes")
                {
                    contextMenuStrip2.Items[0].Enabled = true;
                    contextMenuStrip2.Items[1].Enabled = true;
                    contextMenuStrip2.Items[2].Enabled = false;
                    contextMenuStrip2.Items[3].Enabled = false;
                    contextMenuStrip2.Items[4].Enabled = false;
                    contextMenuStrip2.Show(treeView1, e.X, e.Y);
                }
                if (treeView1.SelectedNode != null && treeView1.SelectedNode.Parent != null && treeView1.SelectedNode.Parent.Text != "Attributes")
                {
                    contextMenuStrip2.Items[0].Enabled = false;
                    contextMenuStrip2.Items[1].Enabled = false;
                    contextMenuStrip2.Items[2].Enabled = true;
                    contextMenuStrip2.Items[3].Enabled = true;
                    contextMenuStrip2.Items[4].Enabled = false;
                    contextMenuStrip2.Show(treeView1, e.X, e.Y);
                }
            }
            else if (treeView1.SelectedNode.Parent == treeView1.Nodes[0])
            {
                contextMenuStrip2.Items[0].Enabled = false;
                contextMenuStrip2.Items[1].Enabled = false;
                contextMenuStrip2.Items[2].Enabled = false;
                contextMenuStrip2.Items[3].Enabled = false;
                contextMenuStrip2.Items[4].Enabled = true;
                contextMenuStrip2.Show(treeView1, e.X, e.Y);
            }

        }

        #endregion

        //-------------------------------------------------------
        //Events in tabControls
        #region Events_TabControls
        //
        //Tab Contronls tabpage 'Général', the buttons and events
        //Button Get Click, get the url and detect the url
        //Button GetGo Click, navigate the text in the richTextBoxPageAccueil 
        //and check if taleo 
        private void ButtonGet_Click(object sender, EventArgs e)
        {
            if (webBrowser1.Url != null)
            {
                richTextBoxPageAccueil.Text = webBrowser1.Url.ToString();
                if (webBrowser1.Document.Window.Frames.Count > 0)
                {
                    treeView1.BeginUpdate();
                    treeView1.Nodes.Clear();
                    treeView1.Nodes.Add("URL of frames");
                    for (int i = 0; i < webBrowser1.Document.Window.Frames.Count; i++)
                    {
                        IHTMLWindow2 htmlWindow = (IHTMLWindow2)(((HTMLDocumentClass)(webBrowser1.Document.DomDocument)).frames.item(i));
                        treeView1.Nodes[0].Nodes.Add(CrossFrameIE.GetDocumentFromWindow(htmlWindow).url.ToString());
                    }
                    treeView1.ExpandAll();
                    treeView1.EndUpdate();
                }
            }
            else
                richTextBoxPageAccueil.Text = "";
        }

        private void ButtonGetGo_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(richTextBoxPageAccueil.Text);
            if (richTextBoxPageAccueil.Text.Contains("taleo"))
            {
                checkBoxTaleo.Checked = true;
            }
        }

        //
        //Tab Controls tabpage 'Parametres'
        //tabpage 'Premiere Page'
        private void CheckBoxTaleo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTaleo.Checked)
            {
                checkBoxAccesDirect.Checked = true;
                checkBoxAccesDirect.Enabled = false;
                groupBoxNextAnn.Visible = true;
                checkBoxOnePage.Checked = true;
                checkBoxOnePage.Enabled = false;
                checkBoxAnnExist.Checked = false;
                checkBoxAnnExist.Enabled = false;
            }
            else
            {
                checkBoxAccesDirect.Enabled = true;
                groupBoxNextAnn.Visible = false;
                checkBoxOnePage.Enabled = true;
                checkBoxAnnExist.Enabled = true;
            }

        }

        private void CheckBoxAccesDirect_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAccesDirect.Checked)
                groupBox4.Visible = false;
            else
                groupBox4.Visible = true;
        }

        private void CheckBoxTri_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTri.Checked)
            {
                groupBox9.Visible = true;
                textBoxClicknb.Text = "2";
            }
            else
            {
                groupBox9.Visible = false;
                textBoxClicknb.Text = "0";
            }
        }

        //
        //tabpage 'PageSuivante'
        private void checkBox_OnePage_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnePage.Checked)
                groupBox5.Visible = false;
            else
                groupBox5.Visible = true;
        }

        //
        //tabpage 'Annonce'
        private void checkBox_AnnExist_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAnnExist.Checked)
                groupBox6.Visible = true;
            else
                groupBox6.Visible = false;
        }

        private void checkBox_FullPage_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFullPage.Checked)
                groupBox8.Visible = true;
            else
                groupBox8.Visible = false;
        }

        private void checkBox_Back_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBack.Checked)
                groupBox7.Visible = false;
            else
                groupBox7.Visible = true;
        }

        //
        //Valide or Test Buttons, and the buttons on the tabpage 'Valide'
        private void ButtonValide_Click(object sender, EventArgs e)
        {
            GetParametres();
            BTest = false;
            Hashtable tmp_params = Parametres;
            //SQL.Connect();
            int site_id = Convert.ToInt32(SQL.Get(@"select id from sites where entreprise = '" + Parametres["ENTREPRISE"].ToString().Replace("'", "''") + "'"));

            DialogResult dr3 = DialogResult.No;
            if (site_id != 0)
            {
                dr3 = MessageBox.Show("This company is already record in the data base. Do you want to update the infos of this company?", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr3 == DialogResult.Yes)
                {
                    //SQL.Connect();
                    try
                    {
                        //SQL.Requet(@"update sites set adresse = 'jb' where id = 2");

                        SQL.Requet(@"update sites set ADRESSE='" + Parametres["ADRESSE"] + "', entreprise='" + Parametres["ENTREPRISE"].ToString().Replace("'", "''")
                            + "', tannonceur='" + Parametres["TANNONCEUR"].ToString().Replace("'", "''") + "', tsecteur='" + Parametres["TSECTEUR"].ToString().Replace("'", "''") + "', maxpages=" + Parametres["MAXPAGES"].ToString()
                            + ", page_delay=" + Parametres["PAGE_DELAY"].ToString() + ", acces_direct=" + Convert.ToByte(Parametres["Acces_Direct"]) + ", lien_constant="
                            + Convert.ToByte(Parametres["Lien_Constant"]) + ", full_page=" + Convert.ToByte(Parametres["FullPage"]) + ", goback="
                            + Convert.ToByte(Parametres["GoBack"]) + ", taleo=" + Convert.ToByte(Parametres["taleo"]) + ", PopUp="
                            + Convert.ToByte(Parametres["PopUp"]) + ", Onepage=" + Convert.ToByte(Parametres["OnePage"])
                            + ", URL_ACCUEIL='" + Parametres["URL_ACCUEIL"] + "', tri=" + Convert.ToInt32(Parametres["tri"]).ToString()
                            + " where id=" + site_id.ToString());
                        string slog = @"Update sites, entreprise : " + Parametres["ENTREPRISE"];
                        Fuctions.log(slog);

                        SQL.Requet(@"delete from params where site_id=" + site_id);
                        SQL.Requet(@"delete from extract_queue where site_id=" + site_id);

                    }
                    catch (Exception exc)
                    { MessageBox.Show("update : " + exc.Message); }
                    //SQL.Disconnect();
                }
            }
            else
            {
                //SQL.Connect();
                try
                {
                    SQL.Requet(@"insert into sites (URL_ACCUEIL, ADRESSE, ENTREPRISE, TANNONCEUR, TSECTEUR, MAXPAGES, PAGE_DELAY, ACCES_DIRECT, LIEN_CONSTANT, FULL_PAGE, GoBack, taleo, PopUp, OnePage, tri) values ('"
                    + Parametres["URL_ACCUEIL"] + "', '" + Parametres["ADRESSE"] + "', '" + Parametres["ENTREPRISE"].ToString().Replace("'", "''") + "', '" + Parametres["TANNONCEUR"].ToString().Replace("'", "''") + "', '"
                    + Parametres["TSECTEUR"].ToString().Replace("'", "''") + "', " + Parametres["MAXPAGES"] + ", " + Parametres["PAGE_DELAY"] + "," + Convert.ToByte(Parametres["Acces_Direct"]) + ","
                    + Convert.ToByte(Parametres["Lien_Constant"]) + "," + Convert.ToByte(Parametres["FullPage"]) + "," + Convert.ToByte(Parametres["GoBack"]) + ","
                    + Convert.ToByte(Parametres["taleo"]) + "," + Convert.ToByte(Parametres["PopUp"]) + "," + Convert.ToByte(Parametres["OnePage"]) + ", " + Convert.ToInt32(Parametres["tri"]) + ")");
                    string slog = @"Insert into sites, entreprise : " + Parametres["ENTREPRISE"];
                    Fuctions.log(slog);

                }
                catch
                { }
                //SQL.Disconnect();

            }
            if (site_id == 0 || dr3 == DialogResult.Yes)
            {
                //SQL.Connect();
                try
                {
                    site_id = Convert.ToInt32(SQL.Get(@"select id from sites where entreprise = '" + Parametres["ENTREPRISE"].ToString().Replace("'", "''") + "'"));
                    //int tmp_site_id = Convert.ToInt16(SQL.Get(@"select id from sites where ENTREPRISE = '" + Parametres["ENTREPRISE"].ToString().Replace("'", "''") + "'"));
                    //if (tmp_site_id != site_id)
                    {
                        // site_id = tmp_site_id;
                        //MessageBox.Show(site_id.ToString());
                        int nbparams = 0;
                        if (Convert.ToByte(Parametres["Acces_Direct"]) == 0 && Convert.ToByte(Parametres["taleo"]) == 0)
                        {
                            if (Parametres["InFirstPage"] != null && Parametres["InFirstPage"].ToString().Length > 7)
                            {
                                SQL.Requet("insert into params (SITE_ID, VALUE, TYPE) values (" + site_id + ", '" + Parametres["InFirstPage"].ToString().Replace("'", "''") + "', 'InFirstPage')");
                                nbparams++;
                            }
                            if (Parametres["ExFirstPage"] != null && Parametres["ExFirstPage"].ToString().Length > 7)
                            {
                                SQL.Requet("insert into params (SITE_ID, VALUE, TYPE) values (" + site_id + ", '" + Parametres["ExFirstPage"].ToString().Replace("'", "''") + "', 'ExFirstPage')");
                                nbparams++;
                            }
                        }
                        if (Parametres["InPageSuivant"] != null && Parametres["InPageSuivant"].ToString().Length > 7 && Convert.ToByte(Parametres["taleo"]) == 0)
                        {
                            SQL.Requet("insert into params (SITE_ID, VALUE, TYPE) values (" + site_id + ", '" + Parametres["InPageSuivant"].ToString().Replace("'", "''") + "', 'InPageSuivant')");
                            nbparams++;
                        }
                        if (Parametres["ExPageSuivant"] != null && Parametres["ExPageSuivant"].ToString().Length > 7 && Convert.ToByte(Parametres["taleo"]) == 0)
                        {
                            SQL.Requet("insert into params (SITE_ID, VALUE, TYPE) values (" + site_id + ", '" + Parametres["ExPageSuivant"].ToString().Replace("'", "''") + "', 'ExPageSuivant')");
                            nbparams++;
                        }
                        if (Parametres["InAnnonce"] != null && Parametres["InAnnonce"].ToString().Length > 7 && Convert.ToByte(Parametres["taleo"]) == 0)
                        {
                            SQL.Requet("insert into params (SITE_ID, VALUE, TYPE) values (" + site_id + ", '" + Parametres["InAnnonce"].ToString().Replace("'", "''") + "', 'InAnnonce')");
                            nbparams++;
                        }
                        if (Parametres["ExAnnonce"] != null && Parametres["ExAnnonce"].ToString().Length > 7 && Convert.ToByte(Parametres["taleo"]) == 0)
                        {
                            SQL.Requet("insert into params (SITE_ID, VALUE, TYPE) values (" + site_id + ", '" + Parametres["ExAnnonce"].ToString().Replace("'", "''") + "', 'ExAnnonce')");
                            nbparams++;
                        }
                        if (Convert.ToByte(Parametres["FullPage"]) == 1 && Convert.ToByte(Parametres["GoBack"]) == 0 && Convert.ToByte(Parametres["taleo"]) == 0)
                        {
                            if (Parametres["InGoBack"] != null && Parametres["InGoBack"].ToString().Length > 7)
                            {
                                SQL.Requet("insert into params (SITE_ID, VALUE, TYPE) values (" + site_id + ", '" + Parametres["InGoBack"].ToString().Replace("'", "''") + "', 'InGoBack')");
                                nbparams++;
                            }
                            if (Parametres["ExGoBack"] != null && Parametres["ExGoBack"].ToString().Length > 7)
                            {
                                SQL.Requet("insert into params (SITE_ID, VALUE, TYPE) values (" + site_id + ", '" + Parametres["ExGoBack"].ToString().Replace("'", "''") + "', 'ExGoBack')");
                                nbparams++;
                            }
                        }
                        if (Convert.ToInt32(Parametres["tri"]) > 0)
                        {
                            if (Parametres["Intri"] != null && Parametres["Intri"].ToString().Length > 7)
                            {
                                SQL.Requet("insert into params (SITE_ID, VALUE, TYPE) values (" + site_id + ", '" + Parametres["Intri"].ToString().Replace("'", "''") + "', 'Intri')");
                                nbparams++;
                            }
                            if (Parametres["Extri"] != null && Parametres["Extri"].ToString().Length > 7)
                            {
                                SQL.Requet("insert into params (SITE_ID, VALUE, TYPE) values (" + site_id + ", '" + Parametres["Extri"].ToString().Replace("'", "''") + "', 'Extri')");
                                nbparams++;
                            }
                        }

                        if (Parametres["NextAnn"] != null && Convert.ToByte(Parametres["taleo"]) != 0)
                        {
                            SQL.Requet("insert into params (SITE_ID, VALUE, TYPE) values (" + site_id + ", '" + Parametres["NextAnn"].ToString().Replace("'", "''") + "', 'NextAnn')");
                            nbparams++;
                        }

                        Fuctions.log("Insert into params, siteid : " + site_id.ToString() + ", entreprise : " + Parametres["ENTREPRISE"] + ", nbparams : " + nbparams.ToString());
                        int sernb = 0;
                        if (Convert.ToByte(Parametres["taleo"]) != 0 || Convert.ToByte(Parametres["PopUp"]) != 0)
                            sernb = 2;
                        else
                            sernb = 1;
                        SQL.Requet(@"insert into extract_queue (SITE_ID, SERVER_NUMBER, DONE) values (" + site_id + ", " + sernb.ToString() + ", 0 )");
                        Fuctions.log("Insert into extract_queue, site_id : " + site_id.ToString() + ", entreprise : " + Parametres["ENTREPRISE"]);
                    }

                }
                catch (Exception exce)
                {
                    MessageBox.Show("from1 insert into sites etc " + exce.Message);
                }
                //SQL.Disconnect();
            }

            //}

        }

        private void ButtonTest_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("2B TEST");
            GetParametres();
            BTest = false;
            //GetParametres();
            IfExtract = false;
            Hashtable tmp_params = Parametres;
            DialogResult dr4 = MessageBox.Show("Do you want to do a test just the pages(no annonces)?", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr4 == DialogResult.Yes)
            {

                tmp_params["InAnnonce"] = @"Annonce:::NoExist ;;;";
            }

            WebForm webform1 = new WebForm(IfExtract, tmp_params);
            //webform1.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(webform1.webBrowser1_DocumentCompleted);
            webform1.ShowDialog();
            //return BTest;
        }

        private void ButtonTestPage_Click(object sender, EventArgs e)
        {
            GetParametres();
            BTest = false;
            //GetParametres();
            IfExtract = false;
            Hashtable tmp_params = Parametres;

            tmp_params["InAnnonce"] = @"Annonce:::NoExist ;;;";

            WebForm webform1 = new WebForm(IfExtract, tmp_params);
            //webform1.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(webform1.webBrowser1_DocumentCompleted);
            webform1.ShowDialog();
        }

        private void ButtonExtract_Click(object sender, EventArgs e)
        {
            GetParametres();
            IfExtract = true;
            WebForm wb = new WebForm(IfExtract, Parametres);
            wb.ShowDialog();

        }

        private void button_Verifier_Click(object sender, EventArgs e)
        {
            GetParametres();
            //button2_Click(sender, e);
            Verifier v1 = new Verifier();
            v1.ShowDialog();
        }

        private void button_Vide_Click(object sender, EventArgs e)
        {
            textBoxEntreprise.Text = "Entreprise";
            richTextBoxPageAccueil.Text = "url page accueil";
            textBoxMaxpages.Text = "10";
            textBoxPageDelay.Text = "0";
            //Console.WriteLine(comboBoxTypeAnnonce.SelectedItem.ToString());
            //Console.WriteLine(comboBoxTypeAnnonce.Text);
            comboBoxTypeAnnonce.Text = "Type d'annonceur";
            //Console.WriteLine(comboBoxTypeAnnonce.SelectedItem.ToString());
            //Console.WriteLine(comboBoxTypeAnnonce.Text);
            comboBoxTypeSecteur.Text = "Type de secteur";
            richTextBoxAdresse.Text = "Adresse postale";
            checkBoxAccesDirect.Checked = true;
            listBoxInFirstPage.Items.Clear();
            listBoxExFirstPage.Items.Clear();
            panel1.Visible = false;
            panel2.Visible = false;
            textBoxnbTrouve1.Text = "";
            checkBoxLienConstant.Checked = true;
            listBoxInPageSuivant.Items.Clear();
            listBoxExPageSuivant.Items.Clear();
            panel3.Visible = false;
            panel4.Visible = false;
            textBoxnbTrouve2.Text = "";
            listBoxInAnnonce.Items.Clear();
            listBoxExAnnonce.Items.Clear();
            textBoxnbTrouve3.Text = "";
            checkBoxFullPage.Checked = false;
            checkBoxBack.Checked = true;
            listBoxInGoBack.Items.Clear();
            listBoxExGoBack.Items.Clear();
            textBoxnbTrouve4.Text = "";
            checkBoxOnePage.Checked = true;
            checkBoxAnnExist.Checked = false;
            textBoxNextAnn.Text = "";
            textBoxnbTrouve5.Text = "";
            checkBoxTaleo.Checked = false;
            checkBoxTri.Checked = false;
            listBoxIntri.Items.Clear();
            listBoxExtri.Items.Clear();
            textBoxnbTrouve6.Text = "";
        }

        private void button_Charge_Click(object sender, EventArgs e)
        {
            string value = "Name of the company";
            if (PopupWindow.InputBox_SelectEntreprise("Name of the company", "Name:", ref value) == DialogResult.OK && value != null)
            {
                button_Vide_Click(null, null);
                //SQL.Connect();
                try
                {
                    DataRow dr_site;
                    DataTable dt_params;
                    dr_site = SQL.GetRow(@"select * from sites where entreprise = '" + value.Replace("'", "''") + "'");
                    dt_params = SQL.GetTable(@"select * from params where site_id=" + dr_site["id"]);
                    //MessageBox.Show(dt_params.Rows[0]["type"].ToString());
                    foreach (DataRow drparam in dt_params.Rows)
                    {
                        string[] str_separators = new string[] { " ;;;" };
                        string[] strs = { "" };
                        if (drparam["value"].ToString().Length > 7)
                            strs = drparam["value"].ToString().Split(str_separators, StringSplitOptions.RemoveEmptyEntries);
                        switch (drparam["type"].ToString())
                        {
                            case "InFirstPage":
                                foreach (string str in strs)
                                {
                                    listBoxInFirstPage.Items.Add(str);
                                }
                                break;
                            case "ExFirstPage":
                                foreach (string str in strs)
                                {

                                    listBoxExFirstPage.Items.Add(str);
                                }
                                break;
                            case "InPageSuivant":
                                foreach (string str in strs)
                                {

                                    listBoxInPageSuivant.Items.Add(str);
                                }
                                break;
                            case "ExPageSuivant":
                                foreach (string str in strs)
                                {

                                    listBoxExPageSuivant.Items.Add(str);
                                }
                                break;
                            case "InAnnonce":
                                foreach (string str in strs)
                                {

                                    listBoxInAnnonce.Items.Add(str);
                                }
                                break;
                            case "ExAnnonce":
                                foreach (string str in strs)
                                {

                                    listBoxExAnnonce.Items.Add(str);
                                }
                                break;
                            case "InGoBack":
                                foreach (string str in strs)
                                {

                                    listBoxInGoBack.Items.Add(str);
                                }
                                break;
                            case "ExGoBack":
                                foreach (string str in strs)
                                {

                                    listBoxExGoBack.Items.Add(str);
                                }
                                break;
                            case "Intri":
                                foreach (string str in strs)
                                {

                                    listBoxIntri.Items.Add(str);
                                }
                                break;
                            case "Extri":
                                foreach (string str in strs)
                                {

                                    listBoxExtri.Items.Add(str);
                                }
                                break;
                            case "NextAnn":
                                textBoxNextAnn.Text = drparam["value"].ToString();
                                break;
                            default: MessageBox.Show("There is not this type of parametre : " + drparam["type"].ToString()); break;
                        }
                    }

                    textBoxEntreprise.Text = dr_site["entreprise"].ToString();
                    richTextBoxPageAccueil.Text = dr_site["url_accueil"].ToString();
                    textBoxMaxpages.Text = dr_site["maxpages"].ToString();
                    textBoxPageDelay.Text = dr_site["page_delay"].ToString();
                    comboBoxTypeSecteur.Text = dr_site["tsecteur"].ToString();
                    comboBoxTypeAnnonce.Text = dr_site["tannonceur"].ToString();
                    richTextBoxAdresse.Text = dr_site["adresse"].ToString();
                    checkBoxAccesDirect.Checked = (bool)dr_site["acces_direct"];
                    checkBoxLienConstant.Checked = (bool)dr_site["lien_constant"];
                    checkBoxOnePage.Checked = (bool)dr_site["OnePage"];
                    checkBoxBack.Checked = (bool)dr_site["GoBack"];
                    checkBoxFullPage.Checked = (bool)dr_site["Full_Page"];
                    checkBoxAnnExist.Checked = true;
                    checkBoxTaleo.Checked = (bool)dr_site["taleo"];
                    if (Convert.ToInt32(dr_site["tri"]) == 0)
                    {
                        checkBoxTri.Checked = false;
                        textBoxClicknb.Text = "0";
                    }
                    else
                    {
                        checkBoxTri.Checked = true;
                        textBoxClicknb.Text = (Convert.ToInt32(dr_site["tri"])).ToString();
                    }

                }
                catch
                { }

                //SQL.Disconnect();
            }


        }
        
        #endregion

        #endregion

    }

    
}