using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using System.Data.Common;
using System.Data;

namespace EuroCMS.Data
{
    public static class DbHelper
    {
 
        public static Database Database;

        public static int CommandTimeout
        {
            get
            {
                return Int32.Parse(ConfigurationManager.AppSettings["Database.CommandTimeout"].ToString());
            }
            
        }

        static DbHelper() 
        {
            Database = DatabaseFactory.CreateDatabase("eurocms.db"); 
        }


        public static DataSet ExecProc(string ProcName, params object[] Params)
        {

            DbCommand dbcom = Database.GetStoredProcCommand(ProcName, Params);
            return Database.ExecuteDataSet(dbcom);
        }

        public static DataSet ExecSQL(string SQLText)
        {
            DbCommand dbcom = Database.GetSqlStringCommand(SQLText);
            return Database.ExecuteDataSet(dbcom);
        }

        public static DataSet ExecuteDataSet(DbCommand command)
        {
            return Database.ExecuteDataSet(command);
        }

        public static DataSet ExecuteSQLProc(String procName)
        {
            DbCommand command = Database.GetStoredProcCommand(procName);
            return Database.ExecuteDataSet(command);
        }

        public static DataSet ExecuteSQLProc(String procName, params object[] Params)
        {
            DbCommand command = Database.GetStoredProcCommand(procName, Params);
            return Database.ExecuteDataSet(command);
        }

        public static DataTable ExecuteSQLProcWithOut(String procName, out object value, params object[] Params)
        {
            DbCommand command = Database.GetStoredProcCommand(procName, Params);
            DataTable dt = Database.ExecuteDataSet(command).Tables[0];
            value = dt.Rows[0][1].ToString();
            return dt;
        }

        public static DataSet ExecuteSQLString(String commandText)
        {
            DbCommand command = Database.GetSqlStringCommand(commandText);
            return Database.ExecuteDataSet(command);
        }

        public static int ExecuteNonQuery(String procName, params object[] Params)
        {
            DbCommand command = Database.GetStoredProcCommand(procName, Params);
            return Database.ExecuteNonQuery(command);
        }

        
    }
}
