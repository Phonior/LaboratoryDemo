using DigitalLaboratory.BLL;
using DigitalLaboratory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLaboratory.Controllers
{
    [Route("[controller]")] //restful
    [ApiController]
    public class AO_OxygenFlowController : ControllerBase
    {
        private readonly IAO_OxygenFlowBLL _flowBLL;

        public AO_OxygenFlowController(IAO_OxygenFlowBLL flowBLL)
        {
            this._flowBLL = flowBLL;
        }

        [HttpGet]
        public List<AO_OxygenFlow> GetAll()
        {
            List<AO_OxygenFlow> flowList = _flowBLL.GetAll();
            return flowList;
        }

        [HttpGet("Last/{number}")]
        public List<AO_OxygenFlow> GetLast(int number)
        {
            List<AO_OxygenFlow> flowList = _flowBLL.GetLast(number);
            return flowList;
        }

        [HttpGet("{oxygenFlowId}")]
        public AO_OxygenFlow GetFlowById(int oxygenFlowId)
        {
            AO_OxygenFlow flow = _flowBLL.GetFlowById(oxygenFlowId);
            return flow;
        }

        [HttpPost]
        public string Insert(decimal currentRate, decimal fullRange)
        {
            return _flowBLL.AddFlow(currentRate, fullRange);
        }

        [HttpPut]
        public string Update(int oxygenFlowId,
                             decimal currentRate,
                             decimal fullRange)
        {
            return _flowBLL.UpdateFlow(oxygenFlowId,
                                       currentRate,
                                       fullRange);
        }

        [HttpDelete("{oxygenFlowId}")]
        public string Remove(int oxygenFlowId)
        {
            return _flowBLL.RemoveFlow(oxygenFlowId);
        }
    }
}
