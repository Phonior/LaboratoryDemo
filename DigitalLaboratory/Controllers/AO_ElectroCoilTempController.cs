using DigitalLaboratory.BLL;
using DigitalLaboratory.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DigitalLaboratory.Controllers
{
    [Route("[controller]")] //restfull
    [ApiController]
    public class AO_ElectroCoilTempController : ControllerBase
    {
        private readonly IAO_ElectroCoilTempBLL _tempBLL;

        public AO_ElectroCoilTempController(IAO_ElectroCoilTempBLL tempBLL)
        {
            this._tempBLL = tempBLL;
        }

        [HttpGet]
        public List<AO_ElectroCoilTemp> GetAll()
        {
            List<AO_ElectroCoilTemp> tempList = _tempBLL.GetAll();
            return tempList;
        }

        [HttpGet("Last/{number}")]
        public List<AO_ElectroCoilTemp> GetLast(int number)
        {
            List<AO_ElectroCoilTemp> tempList = _tempBLL.GetLast(number);
            return tempList;
        }

        [HttpGet("{electroCoilTempId}")]
        public AO_ElectroCoilTemp GetTempById(int electroCoilTempId)
        {
            AO_ElectroCoilTemp temp = _tempBLL.GetTempById(electroCoilTempId);
            return temp;
        }

        [HttpPost]
        public string Insert(decimal electroCoilTemp1, decimal electroCoilTemp2, decimal electroCoilTemp3, decimal electroCoilTemp4,
                             decimal electroCoilTemp5, decimal electroCoilTemp6, decimal electroCoilTemp7, decimal electroCoilTemp8)
        {
            return _tempBLL.AddTemp(electroCoilTemp1, electroCoilTemp2, electroCoilTemp3, electroCoilTemp4,
                                    electroCoilTemp5, electroCoilTemp6, electroCoilTemp7, electroCoilTemp8);
        }

        [HttpPut]
        public string Update(int electroCoilTempId,
                             decimal electroCoilTemp1, decimal electroCoilTemp2, decimal electroCoilTemp3, decimal electroCoilTemp4,
                             decimal electroCoilTemp5, decimal electroCoilTemp6, decimal electroCoilTemp7, decimal electroCoilTemp8)
        {
            return _tempBLL.UpdateTemp(electroCoilTempId,
                                       electroCoilTemp1, electroCoilTemp2, electroCoilTemp3, electroCoilTemp4,
                                       electroCoilTemp5, electroCoilTemp6, electroCoilTemp7, electroCoilTemp8);
        }

        [HttpDelete("{electroCoilTempId}")]
        public string Remove(int electroCoilTempId)
        {
            return _tempBLL.RemoveTemp(electroCoilTempId);
        }
    }
}
