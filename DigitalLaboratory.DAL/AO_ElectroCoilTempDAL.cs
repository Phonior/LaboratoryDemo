using DemoV4.DAL.Core;
using DemoV4.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DemoV4.DAL
{
    public class AO_ElectroCoilTempDAL
    {
        public List<AO_ElectroCoilTemp> GetAll()
        {
            DataTable res = SqlHelper.ExecuteTable("select * from t_AO_ElectroCoilTemp");

            List<AO_ElectroCoilTemp> tempList = ToModelList(res);

            return tempList;
        }

        public AO_ElectroCoilTemp GetTempById(int electroCoilTempId)
        {
            DataRow? dr = null;

            DataTable res = SqlHelper.ExecuteTable(
                "select * from t_AO_ElectroCoilTemp where ElectroCoilTempId = @ElectroCoilTempId",
                new SqlParameter("@ElectroCoilTempId", electroCoilTempId));

            if (res.Rows.Count > 0)
            {
                dr = res.Rows[0];
            }
            AO_ElectroCoilTemp temp = ToModel(dr);

            return temp;
        }

        public int AddTemp(decimal electroCoilTemp1, decimal electroCoilTemp2, decimal electroCoilTemp3, decimal electroCoilTemp4,
                           decimal electroCoilTemp5, decimal electroCoilTemp6, decimal electroCoilTemp7, decimal electroCoilTemp8)
        {
            return SqlHelper.ExecuteNonQuery(
                "insert into t_AO_ElectroCoilTemp" +
                "(ElectroCoilTemp1, ElectroCoilTemp2, ElectroCoilTemp3, ElectroCoilTemp4," +
                " ElectroCoilTemp5, ElectroCoilTemp6, ElectroCoilTemp7, ElectroCoilTemp8) " +
          "values(@ElectroCoilTemp1, @ElectroCoilTemp2, @ElectroCoilTemp3, @ElectroCoilTemp4," +
                " @ElectroCoilTemp5, @ElectroCoilTemp6, @ElectroCoilTemp7, @ElectroCoilTemp8)",
                new SqlParameter("@ElectroCoilTemp1", electroCoilTemp1),
                new SqlParameter("@ElectroCoilTemp2", electroCoilTemp2),
                new SqlParameter("@ElectroCoilTemp3", electroCoilTemp3),
                new SqlParameter("@ElectroCoilTemp4", electroCoilTemp4),
                new SqlParameter("@ElectroCoilTemp5", electroCoilTemp5),
                new SqlParameter("@ElectroCoilTemp6", electroCoilTemp6),
                new SqlParameter("@ElectroCoilTemp7", electroCoilTemp7),
                new SqlParameter("@ElectroCoilTemp8", electroCoilTemp8));
        }

        public int UpdateTemp(int electroCoilTempId,
                              decimal electroCoilTemp1, decimal electroCoilTemp2, decimal electroCoilTemp3, decimal electroCoilTemp4,
                              decimal electroCoilTemp5, decimal electroCoilTemp6, decimal electroCoilTemp7, decimal electroCoilTemp8)
        {
            DataTable dt = SqlHelper.ExecuteTable(
                "select * from t_AO_ElectroCoilTemp where ElectroCoilTempId = @ElectroCoilTempId",
                new SqlParameter("@ElectroCoilTempId", electroCoilTempId));
            int rowCount = 0;
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                AO_ElectroCoilTemp temp = new AO_ElectroCoilTemp();
                temp.ElectroCoilTempId = (int)dr["ElectroCoilTempId"];
                temp.ElectroCoilTemp1 = electroCoilTemp1;
                temp.ElectroCoilTemp2 = electroCoilTemp2;
                temp.ElectroCoilTemp3 = electroCoilTemp3;
                temp.ElectroCoilTemp4 = electroCoilTemp4;
                temp.ElectroCoilTemp5 = electroCoilTemp5;
                temp.ElectroCoilTemp6 = electroCoilTemp6;
                temp.ElectroCoilTemp7 = electroCoilTemp7;
                temp.ElectroCoilTemp8 = electroCoilTemp8;
                rowCount = SqlHelper.ExecuteNonQuery(
                    "update t_AO_ElectroCoilTemp Set " +
                    "ElectroCoilTemp1 = @ElectroCoilTemp1, ElectroCoilTemp2 = @ElectroCoilTemp2, " +
                    "ElectroCoilTemp3 = @ElectroCoilTemp3, ElectroCoilTemp4 = @ElectroCoilTemp4, " +
                    "ElectroCoilTemp5 = @ElectroCoilTemp5, ElectroCoilTemp6 = @ElectroCoilTemp6, " +
                    "ElectroCoilTemp7 = @ElectroCoilTemp7, ElectroCoilTemp8 = @ElectroCoilTemp8, " +
                    "Time = getdate() " +
                    "where ElectroCoilTempId = @ElectroCoilTempId",
                    new SqlParameter("@ElectroCoilTempId", temp.ElectroCoilTempId),
                    new SqlParameter("@ElectroCoilTemp1", temp.ElectroCoilTemp1),
                    new SqlParameter("@ElectroCoilTemp2", temp.ElectroCoilTemp2),
                    new SqlParameter("@ElectroCoilTemp3", temp.ElectroCoilTemp3),
                    new SqlParameter("@ElectroCoilTemp4", temp.ElectroCoilTemp4),
                    new SqlParameter("@ElectroCoilTemp5", temp.ElectroCoilTemp5),
                    new SqlParameter("@ElectroCoilTemp6", temp.ElectroCoilTemp6),
                    new SqlParameter("@ElectroCoilTemp7", temp.ElectroCoilTemp7),
                    new SqlParameter("@ElectroCoilTemp8", temp.ElectroCoilTemp8));
            }
            return rowCount;
        }

        public int RemoveTemp(int electroCoilTempId)
        {
            return SqlHelper.ExecuteNonQuery(
                "delete from t_AO_ElectroCoilTemp where ElectroCoilTempId = @ElectroCoilTempId",
                new SqlParameter("@ElectroCoilTempId", electroCoilTempId));
        }

        //DataTable => List<AO_ElectroCoilTemp>
        private List<AO_ElectroCoilTemp> ToModelList(DataTable table)
        {
            List<AO_ElectroCoilTemp> tempList = new List<AO_ElectroCoilTemp>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow dr = table.Rows[i];
                AO_ElectroCoilTemp temp = ToModel(dr);
                tempList.Add(temp);
            }
            return tempList;
        }

        //DataRow => AO_ElectroCoilTemp
        private AO_ElectroCoilTemp ToModel(DataRow? dr)
        {
            AO_ElectroCoilTemp temp = new AO_ElectroCoilTemp();
            if (dr != null)
            {
                temp.ElectroCoilTempId = (int)dr["ElectroCoilTempId"];
                temp.ElectroCoilTemp1 = Convert.ToDecimal(dr["ElectroCoilTemp1"]);
                temp.ElectroCoilTemp2 = Convert.ToDecimal(dr["ElectroCoilTemp2"]);
                temp.ElectroCoilTemp3 = Convert.ToDecimal(dr["ElectroCoilTemp3"]);
                temp.ElectroCoilTemp4 = Convert.ToDecimal(dr["ElectroCoilTemp4"]);
                temp.ElectroCoilTemp5 = Convert.ToDecimal(dr["ElectroCoilTemp5"]);
                temp.ElectroCoilTemp6 = Convert.ToDecimal(dr["ElectroCoilTemp6"]);
                temp.ElectroCoilTemp7 = Convert.ToDecimal(dr["ElectroCoilTemp7"]);
                temp.ElectroCoilTemp8 = Convert.ToDecimal(dr["ElectroCoilTemp8"]);
                temp.Time = (DateTime)(dr["Time"]);
            }
            return temp;
        }
    }
}
