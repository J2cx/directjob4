using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace directjob4
{
    public partial class Modify : Form
    {
        public string ret_str="";
        public string in_str = "";

        public Modify()
        {
            InitializeComponent();
        }

        private void Modify_Load(object sender, EventArgs e)
        {
            Modify_SizeChanged(sender, null);
            //load();
        }
        public void load(string str_item)
        { 
            string str_in = str_item;
            in_str = str_item;
            if (str_in != null && str_in.Length > 3)
            {
                string[] str_separators = new string[] { ":::" };
                string[] strs_att;
                strs_att = str_item.Split(str_separators, StringSplitOptions.RemoveEmptyEntries);
                textBox1.Text = strs_att[0];
                richTextBox1.Text = strs_att[1];
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ret_str = textBox1.Text + ":::" + richTextBox1.Text;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ret_str = in_str;
            Close();
        }

        private void Modify_SizeChanged(object sender, EventArgs e)
        {
            //SetBounds(webBrowser1.Location.X, webBrowser1.Location.Y, treeView1.Location.X + treeView1.Size.Width, webBrowser1.Size.Height);
            button1.SetBounds(button1.Location.X, this.Size.Height - button1.Size.Height - 50, button1.Size.Width, button1.Size.Height);
            button2.SetBounds(button2.Location.X, button1.Location.Y,button1.Size.Width,button1.Size.Height);
            textBox1.SetBounds(textBox1.Location.X, textBox1.Location.Y, this.Size.Width-label_Att.Size.Width-50,textBox1.Size.Height);
            richTextBox1.SetBounds(richTextBox1.Location.X,richTextBox1.Location.Y, textBox1.Size.Width, button1.Location.Y-label_Value.Location.Y-10);
            //Console.WriteLine(textBox1.Size.Width.ToString());
        }
    }
}
