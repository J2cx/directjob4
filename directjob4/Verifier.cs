using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mshtml;
using System.Net;

using DirectJobsLibrary;

namespace directjob4
{
    public partial class Verifier : Form
    {
        private DataTable dt_annonces = new DataTable();
        public Verifier()
        {
            this.InitializeComponent();
            
        }
        
        private void Verifier_Load(System.Object sender, System.EventArgs e)
        {
            this.Verifier_SizeChanged(this, null);
            //Console.WriteLine("start load....");
            //SQL.Connect();
            try
            {
                DataTable dt_sites = new DataTable();
                DataTable dt_params = new DataTable();
                
                int id_site;
                //Form1
                
                dt_sites=SQL.GetTable(@"select * from sites where entreprise = '"+DirectJobForm.Parametres["ENTREPRISE"].ToString().Replace("'","''")+"'");
                id_site = Convert.ToInt16(dt_sites.Rows[0][0]);
                //dt_params = SQL.GetTable(@"select * from params where site_id = "+id_site);
                dt_annonces = SQL.GetTable(@"select * from liens where id_site = " + id_site);
                //MessageBox.Show(dt_annonces.Rows.Count.ToString());
                if (dt_annonces.Rows.Count <= 0)
                {
                    dt_annonces = SQL.GetTable(@"select * from pages where id_site = " + id_site);
                }
                //dt_annonces = Form1.dtAnnForm;
                
                /*
                DataColumn dc0 = new DataColumn();
                DataColumn dc1 = new DataColumn();
                DataColumn dc2 = new DataColumn();
                dc0.ColumnName = "id";
                dt_annonces.Columns.Add(dc0);
                dc1.ColumnName = "jb";
                dt_annonces.Columns.Add(dc1);
                dc2.ColumnName = "djb~~";
                dt_annonces.Columns.Add(dc2);
                for (int i = 0; i < 150; i++)
                {
                    DataRow dr1 = dt_annonces.NewRow();
                    dr1[0] = i; dr1[1] = "jb1"; dr1[2] = "djb1";
                    dt_annonces.Rows.Add(dr1);
                }
                 * */
                //dataGridView1.DataSource = dt_sites;
                //dataGridView2.DataSource = dt_params;
                dataGridView3.DataSource = dt_annonces;
                //Console.WriteLine("dtale.count : "+dt_sites.Rows.Count.ToString());
                foreach (DataColumn dc in dt_annonces.Columns)
                {
                    if (dc.ColumnName == "link")
                    {
                        contextMenuStrip1.Items[0].Enabled = true;
                        contextMenuStrip1.Items[1].Enabled = false;
                        break;
                    }
                    else if (dc.ColumnName == "source")
                    {
                        contextMenuStrip1.Items[0].Enabled = false;
                        contextMenuStrip1.Items[1].Enabled = true;
                        break;
                    }
                }
                string str_url = SQL.Get(@"select URL_ACCUEIL from sites where id =" + id_site);
                try
                {
                    webBrowser1.Navigate(str_url);
                }
                catch
                { }
            }
            catch (Exception exc)
            {
                Console.WriteLine(" JB.."+exc.Message); 
            }
            //SQL.Disconnect();
        }

        private void Verifier_SizeChanged(object sender, EventArgs e)
        {

            splitContainer1.SetBounds(splitContainer1.Location.X, splitContainer1.Location.Y, this.Size.Width - 15, this.Size.Height - 80);
            /*
            webBrowser1.SetBounds(webBrowser1.Location.X,webBrowser1.Location.Y,this.Size.Width/2-10,this.Size.Height-75);
            if (dataGridView3.Visible == true && webBrowser2.Visible == true)
            {
                dataGridView3.SetBounds(this.Size.Width / 2, webBrowser1.Location.Y, webBrowser1.Size.Width-5, webBrowser1.Size.Height / 2 - 10);
                webBrowser2.SetBounds(dataGridView3.Location.X, dataGridView3.Location.Y + dataGridView3.Height + 10, dataGridView3.Size.Width, dataGridView3.Size.Height);
            }
            else if (dataGridView3.Visible == true && webBrowser2.Visible == false)
            {
                dataGridView3.SetBounds(this.Size.Width / 2, webBrowser1.Location.Y, webBrowser1.Size.Width-5, webBrowser1.Size.Height);
                webBrowser2.SetBounds(this.Size.Width / 2, webBrowser1.Location.Y, webBrowser1.Size.Width - 5, webBrowser1.Size.Height);
            }
            else if (dataGridView3.Visible == false && webBrowser2.Visible == true)
            {
                webBrowser2.SetBounds(this.Size.Width / 2, webBrowser1.Location.Y, webBrowser1.Size.Width-5, webBrowser1.Size.Height);
                dataGridView3.SetBounds(this.Size.Width / 2, webBrowser1.Location.Y, webBrowser1.Size.Width - 5, webBrowser1.Size.Height);
            }
            
            textBox_Url.SetBounds(textBox_Url.Location.X, textBox_Url.Location.Y, this.Size.Width / 2 - textBox_Url.Location.X - button_Go.Size.Width - 2, textBox_Url.Size.Height);
            button_Go.SetBounds(textBox_Url.Location.X + textBox_Url.Size.Width + 2, button_Go.Location.Y, button_Go.Size.Width, button_Go.Size.Height);
            button_Show.SetBounds(this.Size.Width / 2, button_Show.Location.Y, button_Show.Size.Width, button_Show.Size.Height);
            button_Reset.SetBounds(button_Show.Location.X + button_Show.Size.Width + 20, button_Reset.Location.Y, button_Reset.Size.Width, button_Reset.Size.Height);
            */
              //dataGridView1.SetBounds(panel1.Location.X,panel1.Location.Y, panel1.Size.Width,dataGridView1.Size.Height);
            //dataGridView2.SetBounds(panel1.Location.X, dataGridView1.Size.Height+5, panel1.Size.Width, panel1.Size.Height/2-dataGridView1.Size.Height-10);
            //dataGridView3.SetBounds(panel1.Location.X, panel1.Size.Height/2, panel1.Size.Width, panel1.Size.Height/2-5);

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Document.Url != null)
                textBox_Url.Text = webBrowser1.Document.Url.ToString();
            /*
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webBrowser1.Document.Url.ToString());
            request.CookieContainer = new CookieContainer();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Print the properties of each cookie.
            foreach (Cookie cook in response.Cookies)
            {
                Console.WriteLine("Cookie:");
                Console.WriteLine("{0} = {1}", cook.Name, cook.Value);
                Console.WriteLine("Domain: {0}", cook.Domain);
                Console.WriteLine("Path: {0}", cook.Path);
                Console.WriteLine("Port: {0}", cook.Port);
                Console.WriteLine("Secure: {0}", cook.Secure);

                Console.WriteLine("When issued: {0}", cook.TimeStamp);
                Console.WriteLine("Expires: {0} (expired? {1})",
                    cook.Expires, cook.Expired);
                Console.WriteLine("Don't save: {0}", cook.Discard);
                Console.WriteLine("Comment: {0}", cook.Comment);
                Console.WriteLine("Uri for comments: {0}", cook.CommentUri);
                Console.WriteLine("Version: RFC {0}", cook.Version == 1 ? "2109" : "2965");

                // Show the string representation of the cookie.
                Console.WriteLine("String: {0}", cook.ToString());
            }
            */
        }


        private void dataGridView_CellMousDouble(object sender, DataGridViewCellMouseEventArgs e)
        {
            /*
            //dataGridView3.GetChildAtPoint(new Point(e.X, e.Y));
            DataTable dt =(DataTable) dataGridView3.DataSource;
            //DataColumn dc=ne("links"))

            foreach(DataColumn  dc in dt.Columns)
            {
                if (dc.ColumnName == "link")
                {
                    if (dataGridView3.SelectedCells[0].ColumnIndex == 1)
                    { MessageBox.Show(dataGridView3.SelectedCells[0].Value.ToString()); }
                }
            }
             */
            foreach (DataGridViewCell dgvc in dataGridView3.SelectedCells)
            {

                if (dgvc.OwningColumn.Name == "link")
                {
                    //MessageBox.Show(dgvc.Value.ToString());
                    try
                    {
                        webBrowser2.Navigate(dgvc.Value.ToString());
                    }
                    catch
                    { }
                    break;
                }
                else if (dgvc.OwningColumn.Name == "source")
                {
                    //MessageBox.Show(dgvc.Value.ToString());
                    try
                    { webBrowser2.DocumentText = dgvc.Value.ToString(); }
                    catch
                    { }
                    break;
                }
            }
        }
        /*
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Visible == true && webBrowser2.Visible == true)
            {
                dataGridView3.Visible = false;
                webBrowser2.SetBounds(dataGridView3.Location.X, webBrowser1.Location.Y, dataGridView3.Size.Width, webBrowser1.Size.Height);
            }
            else if (dataGridView3.Visible == false && webBrowser2.Visible == true)
            {
                dataGridView3.Visible = true;
                webBrowser2.Visible = false;
                dataGridView3.SetBounds(dataGridView3.Location.X, webBrowser1.Location.Y, dataGridView3.Size.Width, webBrowser1.Size.Height);
            }
            else if (dataGridView3.Visible == true && webBrowser2.Visible == false)
            {
                webBrowser2.Visible = true;
                dataGridView3.SetBounds(dataGridView3.Location.X, webBrowser1.Location.Y, dataGridView3.Size.Width, webBrowser1.Size.Height/2-10);
                webBrowser2.SetBounds(dataGridView3.Location.X,dataGridView3.Location.Y+dataGridView3.Size.Height+10, dataGridView3.Size.Width, webBrowser1.Size.Height/2-10);
            }
        }
        */
        private void button_Go_Click(object sender, EventArgs e)
        {
            if(textBox_Url.Text!=null)
                try
                {
                    webBrowser1.Navigate(textBox_Url.Text);
                }
                catch
                { }
        }

        private void button_Back_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
                webBrowser1.GoBack();
        }

        private void button_Forward_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
                webBrowser1.GoForward();
        }

        private void button_Refresh_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void chercherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string keywords=webBrowser1.Document.ActiveElement.GetAttribute("href");
            var dv = new DataView(dt_annonces);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < keywords.Length; i++)
            {
                char c = keywords[i];
                if (c == '*' || c == '%' || c == '[' || c == ']')
                    sb.Append("[").Append(c).Append("]");
                else if (c == '\'')
                    sb.Append("''");
                else
                    sb.Append(c);
            }

            string rule = @"link like '%keywords%'".Replace("keywords", sb.ToString());
            dv.RowFilter = rule;
            dataGridView3.DataSource = dv;
        }

        private void chercherDansLaPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IHTMLDocument2 htmlDocument = webBrowser1.Document.DomDocument as IHTMLDocument2;
            IHTMLSelectionObject currentSelection = htmlDocument.selection;
            if (currentSelection != null)
            {
                IHTMLTxtRange range = currentSelection.createRange() as IHTMLTxtRange;
                if (range != null && dataGridView3.RowCount > 0)
                {
                    string keywords = range.text;
                    //DataView dv = (DataView)dataGridView3.DataSource;
               
                    
                    var dv = new DataView(dt_annonces);
                    StringBuilder sb = new StringBuilder();
                    if(keywords!=null)
                    for (int i = 0; i < keywords.Length; i++)
                    {
                        char c = keywords[i];
                        if (c == '*' || c == '%' || c == '[' || c == ']')
                            sb.Append("[").Append(c).Append("]");
                        else if (c == '\'')
                            sb.Append("''");
                        else
                            sb.Append(c);
                    }

                    string rule= @"source like '%keywords%'".Replace("keywords",sb.ToString());
                    dv.RowFilter = rule;
                    dataGridView3.DataSource = dv;
                    /*
                    foreach(DataRow dr in dataGridView3.Rows)
                    {
                        dr["source"].ToString().Contains(keywords);
                        
                    }
                  */
                }
            } 
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = dt_annonces;
        }

      



  

        /*
        private Panel buttonPanel = new Panel();
        private System.Windows.Forms.DataGridView songsDataGridView = new System.Windows.Forms.DataGridView();
        private Button addNewRowButton = new Button();
        private Button deleteRowButton = new Button();

        public Verifier()
        {
            this.Load += new EventHandler(Verifier_Load);
        }

        private void Verifier_Load(System.Object sender, System.EventArgs e)
        {
            SetupLayout();
            SetupDataGridView();
            PopulateDataGridView();
        }

        private void songsDataGridView_CellFormatting(object sender,
            System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        {
            if (e != null)
            {
                if (this.songsDataGridView.Columns[e.ColumnIndex].Name == "Release Date")
                {
                    if (e.Value != null)
                    {
                        try
                        {
                            e.Value = DateTime.Parse(e.Value.ToString())
                                .ToLongDateString();
                            e.FormattingApplied = true;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("{0} is not a valid date.", e.Value.ToString());
                        }
                    }
                }
            }
        }

        private void addNewRowButton_Click(object sender, EventArgs e)
        {
            this.songsDataGridView.Rows.Add();
        }

        private void deleteRowButton_Click(object sender, EventArgs e)
        {
            if (this.songsDataGridView.SelectedRows.Count > 0 &&
                this.songsDataGridView.SelectedRows[0].Index !=
                this.songsDataGridView.Rows.Count - 1)
            {
                this.songsDataGridView.Rows.RemoveAt(
                    this.songsDataGridView.SelectedRows[0].Index);
            }
        }

        private void SetupLayout()
        {
            this.Size = new Size(600, 500);

            addNewRowButton.Text = "Add Row";
            addNewRowButton.Location = new Point(10, 10);
            addNewRowButton.Click += new EventHandler(addNewRowButton_Click);

            deleteRowButton.Text = "Delete Row";
            deleteRowButton.Location = new Point(100, 10);
            deleteRowButton.Click += new EventHandler(deleteRowButton_Click);

            buttonPanel.Controls.Add(addNewRowButton);
            buttonPanel.Controls.Add(deleteRowButton);
            buttonPanel.Height = 50;
            buttonPanel.Dock = DockStyle.Bottom;

            this.Controls.Add(this.buttonPanel);
        }

        private void SetupDataGridView()
        {
            this.Controls.Add(songsDataGridView);

            songsDataGridView.ColumnCount = 5;

            songsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            songsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            songsDataGridView.ColumnHeadersDefaultCellStyle.Font =
                new Font(songsDataGridView.Font, FontStyle.Bold);

            songsDataGridView.Name = "songsDataGridView";
            songsDataGridView.Location = new Point(8, 8);
            songsDataGridView.Size = new Size(500, 250);
            songsDataGridView.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            songsDataGridView.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            songsDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            songsDataGridView.GridColor = Color.Black;
            songsDataGridView.RowHeadersVisible = false;

            songsDataGridView.Columns[0].Name = "Release Date";
            songsDataGridView.Columns[1].Name = "Track";
            songsDataGridView.Columns[2].Name = "Title";
            songsDataGridView.Columns[3].Name = "Artist";
            songsDataGridView.Columns[4].Name = "Album";
            songsDataGridView.Columns[4].DefaultCellStyle.Font =
                new Font(songsDataGridView.DefaultCellStyle.Font, FontStyle.Italic);

            songsDataGridView.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            songsDataGridView.MultiSelect = false;
            songsDataGridView.Dock = DockStyle.Fill;

            songsDataGridView.CellFormatting += new
                DataGridViewCellFormattingEventHandler(
                songsDataGridView_CellFormatting);
        }

        private void PopulateDataGridView()
        {

            string[] row0 = { "11/22/1968", "29", "Revolution 9", 
            "Beatles", "The Beatles [White Album]" };
            string[] row1 = { "1960", "6", "Fools Rush In", 
            "Frank Sinatra", "Nice 'N' Easy" };
            string[] row2 = { "11/11/1971", "1", "One of These Days", 
            "Pink Floyd", "Meddle" };
            string[] row3 = { "1988", "7", "Where Is My Mind?", 
            "Pixies", "Surfer Rosa" };
            string[] row4 = { "5/1981", "9", "Can't Find My Mind", 
            "Cramps", "Psychedelic Jungle" };
            string[] row5 = { "6/10/2003", "13", 
            "Scatterbrain. (As Dead As Leaves.)", 
            "Radiohead", "Hail to the Thief" };
            string[] row6 = { "6/30/1992", "3", "Dress", "P J Harvey", "Dry" };

            songsDataGridView.Rows.Add(row0);
            songsDataGridView.Rows.Add(row1);
            songsDataGridView.Rows.Add(row2);
            songsDataGridView.Rows.Add(row3);
            songsDataGridView.Rows.Add(row4);
            songsDataGridView.Rows.Add(row5);
            songsDataGridView.Rows.Add(row6);

            songsDataGridView.Columns[0].DisplayIndex = 3;
            songsDataGridView.Columns[1].DisplayIndex = 4;
            songsDataGridView.Columns[2].DisplayIndex = 0;
            songsDataGridView.Columns[3].DisplayIndex = 1;
            songsDataGridView.Columns[4].DisplayIndex = 2;
        }*/
    }
        
}
