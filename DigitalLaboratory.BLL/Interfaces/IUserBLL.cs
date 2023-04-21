using DemoV4.Models;

namespace DemoV4.BLL
{
    public interface IUserBLL
    {
        string AddUser(string userNo, string userName, string password);
        
        Users? CheckLogin(string userNo, string password);
        
        List<Users> GetAll();
        
        string RemoveUser(int id);
        
        string UpdateUser(int id, string userNo, string userName, string password);
    }
}
