using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Context : IDisposable
    {
        public SqlConnection con;

        public SqlTransaction tran;

        public void Commit()
        {
            tran.Commit();
        }

        public void Rollback()
        {
            tran.Rollback();
        }

        
        public Context()
        {
            //------------------ Manage Connection ----------------------//
            con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["MasterPiece"].ConnectionString
            };
            con.Open();

            tran = con.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void Dispose()
        {
            if (tran != null )
            {
                tran.Dispose();
            }
            if( con!=null && con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }
    }
}
