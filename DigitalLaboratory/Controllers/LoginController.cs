using DigitalLaboratory.BLL;
using DigitalLaboratory.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLaboratory.Controllers
{
    //高内聚，低耦合
    //面向抽象/依赖倒置
    //interface可以 规范开发 多态 层间引用

    //Route("[controller]/[action]")]
    [Route("[controller]")] //restfull
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserBLL _userBLL;

        public LoginController(IUserBLL userBLL) 
        {
            this._userBLL = userBLL;
        }

        [HttpGet]
        public List<Users> GetAll()
        {
            List<Users> userList = _userBLL.GetAll();
            return userList;
        }

        //正常返回用户，200为登录成功，204则为登陆失败
        [HttpGet("{userNo}/{password}")]
        public Users? GetLoginRes(string userNo, string password)
        {
            Users? user = _userBLL.CheckLogin(userNo, password);
            return user;
        }

        [HttpPost]
        public string Insert(string userNo, string userName, string password)
        {
            return _userBLL.AddUser(userNo, userName, password);    
        }

        [HttpPut]
        public string Update(int id, string userNo, string userName, string password)
        {
            return _userBLL.UpdateUser(id, userNo, userName, password);
        }

        [HttpDelete]
        public string Remove(int id)
        {
            return _userBLL.RemoveUser(id);
        }
    }
}
