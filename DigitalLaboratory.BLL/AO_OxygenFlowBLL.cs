using DemoV4.DAL;
using DemoV4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoV4.BLL
{
    public class AO_OxygenFlowBLL : IAO_OxygenFlowBLL
    {
        AO_OxygenFlowDAL flowDal = new AO_OxygenFlowDAL();

        public List<AO_OxygenFlow> GetAll()
        {
            return flowDal.GetAll();
        }

        public AO_OxygenFlow GetFlowById(int oxygenFlowId)
        {
            return flowDal.GetFlowById(oxygenFlowId);
        }

        public List<AO_OxygenFlow> GetLast(int number)
        {
            List<AO_OxygenFlow> flowList = flowDal.GetAll();
            List<AO_OxygenFlow> res = new List<AO_OxygenFlow>();
            for (int i = flowList.Count - 1; i >= 0 && i >= flowList.Count - number; i--)
            {
                res.Add(flowList[i]);
            }
            res.Reverse();
            return res;
        }

        public string AddFlow(decimal oxygenFlowRate)
        {
            int rows = flowDal.AddFlow(oxygenFlowRate);
            if (rows > 0)
            {
                return ("插入成功！");
            }
            else
            {
                return ("插入失败！");
            }
        }

        public string UpdateFlow(int oxygenFlowId,
                                 decimal oxygenFlowRate)
        {
            int rows = flowDal.UpdateFlow(oxygenFlowId,
                                          oxygenFlowRate);
            if (rows > 0)
            {
                return ("更新成功！");
            }
            else
            {
                return ("更新失败！");
            }
        }

        public string RemoveFlow(int oxygenFlowId)
        {
            int rows = flowDal.RemoveFlow(oxygenFlowId);
            if (rows > 0)
            {
                return ("删除成功！");
            }
            else
            {
                return ("删除失败！");
            }
        }
    }
}
