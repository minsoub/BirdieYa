using System;
using System.Collections.Generic;
using System.Text;

using System.Data.SqlClient;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;
using System.Collections;
using System.Data;

namespace SSFramework.Data
{
    public class DbControl
    {
        DbBase dbBase = new DbBase();

        public DbControl()
        {
            //dbBase.Conn();
        }

        public DataTable GetDataTableSQL(string sSql, ArrayList aPrams)
        {
            DataTable dt = new DataTable();
            SqlCommand com = new SqlCommand(sSql, dbBase.Conn());

            if (aPrams != null)
            {
                com.Parameters.AddRange(aPrams.ToArray());
            }
            SqlDataAdapter da = new SqlDataAdapter(com);

            try
            {
                da.Fill(dt);
            }
            catch (Exception e)
            {
                this.DBCloseSQL();

                string sParameters = string.Empty;

                foreach (SqlParameter sp in aPrams)
                {
                    sParameters += sp.Value;
                }

                throw new Exception("Error Message : " + e.Message + sParameters);
            }
            finally
            {
                this.DBCloseSQL();
            }

            if (dt == null) dt = new DataTable();

            return dt;
        }

        public DataTable GetDataTable(string sSql, ArrayList aPrams)
        {
            DataTable dt = new DataTable();
            OracleCommand com = new OracleCommand(sSql, dbBase.OraConn());

            if (aPrams != null)
            {
                com.Parameters.AddRange(aPrams.ToArray());
            }
            OracleDataAdapter da = new OracleDataAdapter(com);

            try
            {
                da.Fill(dt);
            }
            catch (Exception e)
            {
                this.DBClose();

                string sParameters = string.Empty;

                foreach (OracleParameter sp in aPrams)
                {
                    sParameters += sp.Value;
                }

                throw new Exception("Error Message : " + e.Message + sParameters);
            }
            finally
            {
                this.DBClose();
            }

            if (dt == null) dt = new DataTable();

            return dt;
        }

        public string MultiExcuteSQL(string query, ArrayList array)
        {
            string sMessage = string.Empty;
            int result = 0;

            SqlConnection conn = dbBase.Conn();

            SqlTransaction tr = conn.BeginTransaction(IsolationLevel.ReadCommitted);

            SqlCommand com = new SqlCommand(query, conn);

            com.Transaction = tr;

            try
            {
                if (array != null)
                {
                    foreach (ArrayList arr in array)
                    {
                        com.Parameters.Clear();
                        com.Parameters.AddRange(arr.ToArray());
                        result += com.ExecuteNonQuery();
                    }
                }
                tr.Commit();
            }
            catch (SqlException se)
            {
                sMessage = "Error Message = " + se.Message.ToString();
                
                tr.Rollback();

                this.DBCloseSQL();
            }
            finally
            {
                this.DBCloseSQL();
            }
            return sMessage;
        }

        public string MultiExcute(string query, ArrayList array)
        {
            string sMessage = string.Empty;
            int result = 0;

            OracleConnection conn = dbBase.OraConn();

            OracleTransaction tr = conn.BeginTransaction(IsolationLevel.ReadCommitted);

            OracleCommand com = new OracleCommand(query, conn);

            com.Transaction = tr;

            try
            {
                if (array != null)
                {
                    foreach (ArrayList arr in array)
                    {
                        com.Parameters.Clear();
                        com.Parameters.AddRange(arr.ToArray());
                        result += com.ExecuteNonQuery();
                    }
                }
                tr.Commit();
            }
            catch (OracleException se)
            {
                sMessage = "Error Message = " + se.Message.ToString();

                tr.Rollback();

                this.DBClose();
            }
            finally
            {
                this.DBClose();
            }
            return sMessage;
        }

        public string ExecuteSQL(string query, ArrayList array)
        {
            string sMessage = string.Empty;
            int result = 0;

            SqlCommand com = new SqlCommand(query, dbBase.Conn());

            com.Parameters.AddRange(array.ToArray());

            try
            {
                result = com.ExecuteNonQuery();
            }
            catch (SqlException se)
            {
                this.DBCloseSQL();
                sMessage = "Error Message = " + se.Message.ToString();
            }
            finally
            {
                this.DBCloseSQL();
            }
            
            return sMessage;
        }

        public string Execute(string query, ArrayList array)
        {
            string sMessage = string.Empty;
            int result = 0;

            OracleCommand com = new OracleCommand(query, dbBase.OraConn());

            com.Parameters.AddRange(array.ToArray());

            try
            {
                result = com.ExecuteNonQuery();
            }
            catch (OracleException se)
            {
                this.DBClose();
                sMessage = "Error Message = " + se.Message.ToString();
            }
            finally
            {
                this.DBClose();
            }

            return sMessage;
        }

        private void DBCloseSQL()
        {
            if (dbBase != null)
            {
                dbBase.DBClose();
            }
        }

        private void DBClose()
        {
            if (dbBase != null)
            {
                dbBase.OraDBClose();
            }
        }


    }
}
