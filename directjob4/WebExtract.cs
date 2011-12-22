using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mshtml;
using System.Threading;

namespace directjob4
{
    public partial class WebExtract : Form
    {
        private string s_doc;

        public WebExtract(string s_doc)
        {
            InitializeComponent();
            this.s_doc = s_doc;
            //HtmlDocument hdoc = this.webBrowser1.Document;
            //hdoc.Write(s_doc);
        }

        private void WebExtract_Load(object sender, EventArgs e)
        {
            //IHTMLDocument2
            //webform2.linkopen.InvokeMember("click")
            //((IHTMLDocument2)this.webBrowser1.Document.DomDocument).write(s_doc);
            //HtmlDocument hdoc = this.webBrowser1.Document;
            //hdoc.Write(s_doc);
            this.webBrowser1.DocumentText=s_doc;
        
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Thread.Sleep(2000);
            //Close();
        }



        
    }
}
