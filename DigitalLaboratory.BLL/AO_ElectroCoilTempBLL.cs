using DemoV4.DAL;
using DemoV4.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoV4.BLL
{
    public class AO_ElectroCoilTempBLL : IAO_ElectroCoilTempBLL
    {
        AO_ElectroCoilTempDAL tempDal = new AO_ElectroCoilTempDAL();

        public List<AO_ElectroCoilTemp> GetAll()
        {
            return tempDal.GetAll();
        }



        public AO_ElectroCoilTemp GetTempById(int electroCoilTempId)
        {
            return tempDal.GetTempById(electroCoilTempId);
        }

        public List<AO_ElectroCoilTemp> GetLast(int number)
        {
            List<AO_ElectroCoilTemp> tempList = tempDal.GetAll();
            List<AO_ElectroCoilTemp> res = new List<AO_ElectroCoilTemp>();
            for (int i = tempList.Count - 1; i >= 0 && i >= tempList.Count - number; i--)
            {
                res.Add(tempList[i]);
            }
            res.Reverse();
            return res;
        }

        public string AddTemp(decimal electroCoilTemp1, decimal electroCoilTemp2, decimal electroCoilTemp3, decimal electroCoilTemp4,
                              decimal electroCoilTemp5, decimal electroCoilTemp6, decimal electroCoilTemp7, decimal electroCoilTemp8)
        {
            int rows = tempDal.AddTemp(electroCoilTemp1, electroCoilTemp2, electroCoilTemp3, electroCoilTemp4,
                                       electroCoilTemp5, electroCoilTemp6, electroCoilTemp7, electroCoilTemp8);
            if (rows > 0)
            {
                return ("插入成功！");
            }
            else
            {
                return ("插入失败！");
            }
        }

        public string UpdateTemp(int electroCoilTempId,
                                 decimal electroCoilTemp1, decimal electroCoilTemp2, decimal electroCoilTemp3, decimal electroCoilTemp4,
                                 decimal electroCoilTemp5, decimal electroCoilTemp6, decimal electroCoilTemp7, decimal electroCoilTemp8)
        {
            int rows = tempDal.UpdateTemp(electroCoilTempId,
                                          electroCoilTemp1, electroCoilTemp2, electroCoilTemp3, electroCoilTemp4,
                                          electroCoilTemp5, electroCoilTemp6, electroCoilTemp7, electroCoilTemp8);
            if (rows > 0)
            {
                return ("更新成功！");
            }
            else
            {
                return ("更新失败！");
            }
        }

        public string RemoveTemp(int electroCoilTempId)
        {
            int rows = tempDal.RemoveTemp(electroCoilTempId);
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
