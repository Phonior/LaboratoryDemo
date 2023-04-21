using DemoV4.Models;

namespace DemoV4.BLL
{
    public interface IAO_OxygenFlowBLL
    {
        string AddFlow(decimal oxygenFlowRate);

        List<AO_OxygenFlow> GetAll();

        AO_OxygenFlow GetFlowById(int oxygenFlowId);

        List<AO_OxygenFlow> GetLast(int number);

        string RemoveFlow(int oxygenFlowId);

        string UpdateFlow(int oxygenFlowId, decimal oxygenFlowRate);
    }
}