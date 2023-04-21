using DemoV4.DAL;
using DemoV4.Models;

namespace DemoV4.BLL
{
    public class UserBLL : IUserBLL
    {
        //继承接口的类必须要实现接口声明的所有方法

        UserDAL userDal = new UserDAL();

        public Users? CheckLogin(string userNo, string password)
        {
            List<Users> userList = userDal.GetUserByUserNoAndPassword(userNo, password);
            if (userList.Count <= 0)
                return default;
            else
            {
                Users? user = userList.Find(m => !m.IsDelete);
                return user;
            }
        }

        public List<Users> GetAll()
        {
            return userDal.GetAll().FindAll(m => !m.IsDelete);
        }

        public string AddUser(string userNo, string userName, string password)
        {
            int rows = userDal.AddUser(userNo, userName, password);
            if (rows > 0)
            {
                return ("插入成功！");
            }
            else
            {
                return ("插入失败！");
            }
        }

        public string UpdateUser(int id, string userNo, string userName, string password)
        {
            int rows = userDal.UpdateUser(id, userNo, userName, password);
            if (rows > 0)
            {
                return ("更新成功！");
            }
            else
            {
                return ("更新失败！");
            }
        }

        public string RemoveUser(int id)
        {
            int rows = userDal.RemoveUser(id);
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