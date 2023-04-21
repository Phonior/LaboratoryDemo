using DemoV4.DAL.Core;
using DemoV4.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoV4.DAL
{
    public class AO_OxygenFlowDAL
    {
        public List<AO_OxygenFlow> GetAll()
        {
            DataTable res = SqlHelper.ExecuteTable("select * from t_AO_OxygenFlow");

            List<AO_OxygenFlow> flowList = ToModelList(res);

            return flowList;
        }

        public AO_OxygenFlow GetFlowById(int oxygenFlowId)
        {
            DataRow? dr = null;

            DataTable res = SqlHelper.ExecuteTable(
                "select * from t_AO_OxygenFlow where OxygenFlowId = @OxygenFlowId",
                new SqlParameter("@OxygenFlowId", oxygenFlowId));

            if (res.Rows.Count > 0)
            {
                dr = res.Rows[0];
            }
            AO_OxygenFlow flow = ToModel(dr);

            return flow;
        }

        public int AddFlow(decimal oxygenFlowRate)
        {
            return SqlHelper.ExecuteNonQuery(
                "insert into t_AO_OxygenFlow" +
                "(OxygenFlowRate) " +
          "values(@OxygenFlowRate)",
                new SqlParameter("@OxygenFlowRate", oxygenFlowRate));
        }

        public int UpdateFlow(int oxygenFlowId, decimal oxygenFlowRate)
        {
            DataTable dt = SqlHelper.ExecuteTable(
                "select * from t_AO_OxygenFlow where OxygenFlowId = @OxygenFlowId",
                new SqlParameter("@OxygenFlowId", oxygenFlowId));
            int rowCount = 0;
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                AO_OxygenFlow flow = new AO_OxygenFlow();
                flow.OxygenFlowId = (int)dr["OxygenFlowId"];
                flow.OxygenFlowRate = oxygenFlowRate;
                rowCount = SqlHelper.ExecuteNonQuery(
                    "update t_AO_OxygenFlow Set " +
                    "OxygenFlowRate = @OxygenFlowRate, " +
                    "Time = getdate() " +
                    "where OxygenFlowId = @OxygenFlowId",
                    new SqlParameter("@OxygenFlowId", flow.OxygenFlowId),
                    new SqlParameter("@OxygenFlowRate", flow.OxygenFlowRate));
            }
            return rowCount;
        }

        public int RemoveFlow(int oxygenFlowId)
        {
            return SqlHelper.ExecuteNonQuery(
                "delete from t_AO_OxygenFlow where OxygenFlowId = @OxygenFlowId",
                new SqlParameter("@OxygenFlowId", oxygenFlowId));
        }

        //DataTable => List<AO_OxygenFlow>
        private List<AO_OxygenFlow> ToModelList(DataTable table)
        {
            List<AO_OxygenFlow> flowList = new List<AO_OxygenFlow>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow dr = table.Rows[i];
                AO_OxygenFlow flow = ToModel(dr);
                flowList.Add(flow);
            }
            return flowList;
        }

        //DataRow => AO_OxygenFlow
        private AO_OxygenFlow ToModel(DataRow? dr)
        {
            AO_OxygenFlow flow = new AO_OxygenFlow();
            if (dr != null)
            {
                flow.OxygenFlowId = (int)dr["OxygenFlowId"];
                flow.OxygenFlowRate = Convert.ToDecimal(dr["OxygenFlowRate"]);
                flow.Time = (DateTime)(dr["Time"]);
            }
            return flow;
        }
    }
}
