using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace directjob4
{
    /*
    class SQL
    {
        //DEPS: Program.debug();

        //private string ConnectionString = Program.ConnectionString;
        //private static SqlCeEngine oSql = Program.oSql;

        //private static SqlConnection oCon = Program.oCon;
        //private static SqlCommand oCmd = Program.oCmd;

        public static void Connect()
        {
            try
            {
                Program.oCon.Open();
                Program.oCmd = Program.oCon.CreateCommand();
                //oCon.Open();
                //oCmd = oCon.CreateCommand();
            }
            catch (Exception e)
            {
                //Program.debug(e.StackTrace);
                Program.debug("sql connect "+e.Message);
            }
        }

        public static void Disconnect()
        {
            try
            {
                Program.oCon.Close();
            }
            catch (Exception e)
            {
                Program.debug("sql disconnect "+e.Message);
            }
        }

        public static void Do(string ssql)
        {
            Program.oCmd.CommandText = ssql;
            try
            {
                Program.oCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Program.debug(e.StackTrace);
            }
        }

        public static string Get(string ssql)
        {
            string ret = null;

            Program.oCmd.CommandText = ssql;
            try
            {
                ret = Program.oCmd.ExecuteScalar().ToString();
            }
            catch (Exception e)
            {
                //if (!(e is System.NullReferenceException))
                //{
                if (e.GetType().ToString() != "System.NullReferenceException") 
                {
                    //Program.debug(e.StackTrace);
                    Program.debug("sql get "+e.Message);
                }

            }

            return ret;
        }

        public static bool Requet(string ssql)
        {
            bool relt;
            DataTable ret = null;
            if (Program.oCmd == null)
            { 
                Program.oCmd=Program.oCon.CreateCommand(); 
            }

            Program.oCmd.CommandText = ssql;
            try
            {
                SqlDataAdapter oAdapter = new SqlDataAdapter();
                oAdapter.SelectCommand = Program.oCmd;

                DataSet oSet = new DataSet();
                oAdapter.Fill(oSet, "oTable");

                ret = oSet.Tables["oTable"];
                relt = true;

            }
                
            catch (SqlException sqle)
            {
                relt = false;
                //MessageBox.Show(sqle.Message);
                if (sqle.ErrorCode != -2146232060)
                {
                    //Program.debug(sqle.StackTrace);
                    Program.debug("sql requet "+sqle.Message);
                }


            }
            return relt;

            //return ret;
        }
        
        public static DataTable GetTable(string ssql)
        {
            DataTable ret = null;

            Program.oCmd.CommandText = ssql;
            try
            {
                SqlDataAdapter oAdapter = new SqlDataAdapter();
                oAdapter.SelectCommand = Program.oCmd;

                DataSet oSet = new DataSet();
                oAdapter.Fill(oSet, "oTable");

                ret = oSet.Tables["oTable"];
            }
            catch (Exception e)
            {
               // Program.debug(e.StackTrace);
                Program.debug("sql gettable "+e.Message);
            }

            return ret;
        }

        public static DataRow GetRow(string ssql)
        {
            DataRow ret = null;
                        Program.oCmd.CommandText = ssql;
            try
            {
                SqlDataAdapter oAdapter = new SqlDataAdapter();
                oAdapter.SelectCommand = Program.oCmd;

                DataSet oSet = new DataSet();
                oAdapter.Fill(oSet, "oTable");

                DataTable oTable = oSet.Tables["oTable"];
                ret = oTable.Rows[0];
            }
            catch (Exception e)
            {
                Program.debug("sql getrow "+e.Message);
            }

            return ret;
        }

        public static DataColumn GetColumn(string ssql)
        {
            DataColumn ret = null;

            Program.oCmd.CommandText = ssql;
            try
            {
                SqlDataAdapter oAdapter = new SqlDataAdapter();
                oAdapter.SelectCommand = Program.oCmd;

                DataSet oSet = new DataSet();
                oAdapter.Fill(oSet, "oTable");

                DataTable oTable = oSet.Tables["oTable"];
                ret = oTable.Columns[0];
            }
            catch (Exception e)
            {
                Program.debug("sql getrow " + e.Message);
            }

            return ret;
        }
    }*/
}