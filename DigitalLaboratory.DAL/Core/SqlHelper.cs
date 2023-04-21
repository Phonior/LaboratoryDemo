using Microsoft.Data.SqlClient;
using System.Data;

namespace DemoV4.DAL.Core
{
    public class SqlHelper
    {
        public static string ConnectionString { get; set; } = "server = DESKTOP-PGRAUNH; database=LaboratoryDB; Integrated Security = True";
    
        public static DataTable ExecuteTable(string cmdText, params SqlParameter[] sqlParameters)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddRange(sqlParameters);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }
            return ds.Tables[0];
        }

        public static int ExecuteNonQuery(string cmdText, params SqlParameter[] sqlParameters)
        {
            int res = -1;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddRange(sqlParameters);
                res = cmd.ExecuteNonQuery();
            }
            return res;
        }
    }
}
