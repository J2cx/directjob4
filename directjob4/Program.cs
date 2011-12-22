using System;
using System.Windows.Forms;
//using System.Collections.Generic;
//using System.Linq;
//using System.IO;
//using System.Data.SqlClient;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading;

using DirectJobsLibrary;

namespace directjob4
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [STAThread]
        static void Main()
        {
            ConfigManager configManager1 = new ConfigManager();
            configManager1.LoadConfig();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DirectJobForm());
        }


        public static int taleoclose;

        //temp dir
        //public const string temp = @"C:\TEMP";
        public static string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        /*No More used */
        //user & group
        public static int grp_id = 0;
        public static int usr_id = 0;

        public const bool on = true;
        public const bool off = false;

        //verbose mode
        public static bool verbose = on;

    }
}
