using DemoV4.Models;
using System.Data;

namespace DemoV4.BLL
{
    public interface IAO_ElectroCoilTempBLL
    {
        string AddTemp(decimal electroCoilTemp1, decimal electroCoilTemp2, decimal electroCoilTemp3, decimal electroCoilTemp4,
                       decimal electroCoilTemp5, decimal electroCoilTemp6, decimal electroCoilTemp7, decimal electroCoilTemp8);

        List<AO_ElectroCoilTemp> GetAll();

        AO_ElectroCoilTemp GetTempById(int electroCoilTempId);

        List<AO_ElectroCoilTemp> GetLast(int number);

        string RemoveTemp(int electroCoilTempId);

        string UpdateTemp(int electroCoilTempId,
                          decimal electroCoilTemp1, decimal electroCoilTemp2, decimal electroCoilTemp3, decimal electroCoilTemp4,
                          decimal electroCoilTemp5, decimal electroCoilTemp6, decimal electroCoilTemp7, decimal electroCoilTemp8);
    }
}