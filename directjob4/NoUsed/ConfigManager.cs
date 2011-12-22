using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;
using System.Data.SqlClient;
using System.Web;


namespace directjob4
{/*
    class ConfigManager
    {
        private Hashtable Elemements = new Hashtable();

        public ConfigManager()
        {
            Elemements = new Hashtable();
        }

        public void LoadConfig()
        {
            XmlTextReader r = new XmlTextReader("XMLFile2.xml");
            r.MoveToContent();
            string s, sname;
            while (r.Read())
            {
                if (r.NodeType == XmlNodeType.Element)
                {
                    sname = r.Name;
                    //Console.Write("element name :'{0}'", sname);                 
                    s = r.ReadElementString();
                    //Console.WriteLine(" Value of Element is '{0}'", s);
                    Elemements.Add(sname, s);
                    Console.WriteLine("Key = {0} value = {1}", sname, Elemements[sname]);
                }
            }
            r.Close();
            Program.ConnectionString = @"Data Source=" + Elemements["nomduserveur"] + ";Initial Catalog=" + Elemements["database"] 
                                        + ";User Id=" + Elemements["login"] + ";Password=" + Elemements["pass"] + ";";
            Program.oCon.ConnectionString = Program.ConnectionString;


            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine("Press Enter to continue");
            //Console.ReadLine();
        }

    }*/
}
