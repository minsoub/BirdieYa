using System;
using System.Collections.Generic;
using System.Text;

using System.Data.SqlClient;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Collections;
using System.Configuration;

namespace SSFramework.Data
{
    public class DbBase
    {
        public SqlConnection mConn = new SqlConnection();
        public OracleConnection mOraConn = new OracleConnection();

        public DbBase()
        {

        }

        public SqlConnection Conn()
        {
            string sConnStr = ConfigurationManager.ConnectionStrings["DbConn"].ToString();
            Console.WriteLine(sConnStr);


            if (sConnStr == string.Empty)
            {
                return new SqlConnection();
            }
            else
            {
                if (mConn.State == ConnectionState.Closed)
                {
                    mConn = new SqlConnection(sConnStr);
                    mConn.Open();
                }
                else
                {
                    string msg = mConn.ConnectionString;
                }
                return mConn;
            }
        }

        public bool DBClose()
        {
            try
            {
                if (mConn.State != ConnectionState.Closed)
                {
                    mConn.Close();
                }

                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("SqlException {0} occured", e.Message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception {0} occured", e.Message);
                return false;
            }
        }

        public OracleConnection OraConn()
        {
            string sConnStr = ConfigurationManager.ConnectionStrings["DbConn"].ToString();
            Console.WriteLine(sConnStr);
            if (sConnStr == string.Empty)
            {
                return new OracleConnection();
            }
            else
            {
                if (mOraConn.State == ConnectionState.Closed)
                {
                    mOraConn = new OracleConnection(sConnStr);
                    mOraConn.Open();
                }
                else
                {
                    string msg = mOraConn.ConnectionString;
                }
                return mOraConn;
            }
        }

        public bool OraDBClose()
        {
            try
            {
                mConn.Close();
                return true;
            }
            catch (OracleException e)
            {
                Console.WriteLine("SqlException {0} occured", e.Message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception {0} occured", e.Message);
                return false;
            }
        }
    }
}
