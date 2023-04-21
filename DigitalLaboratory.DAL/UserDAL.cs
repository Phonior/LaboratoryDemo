using DemoV4.DAL.Core;
using DemoV4.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DemoV4.DAL
{
    public class UserDAL
    {
        public List<Users> GetAll()
        {
            DataTable res = SqlHelper.ExecuteTable("select * from t_User");

            List<Users> userList = ToModelList(res);

            return userList;
        }
        public Users GetUserById(int id)
        {
            DataRow? dr = null;

            DataTable res = SqlHelper.ExecuteTable(
                "select * from t_User where Id = @Id",
                new SqlParameter("@Id", id));

            if (res.Rows.Count > 0)
            {
                dr = res.Rows[0];
            }
            Users user = ToModel(dr);

            return user;
        }

        public List<Users> GetUserByUserNoAndPassword(string userNo, string password)
        {
            DataTable res = SqlHelper.ExecuteTable(
                "select * from t_User where UserNo=@UserNo and Password=@Password",
                new SqlParameter("@UserNo", userNo),
                new SqlParameter("@Password", password));

            List<Users> userList = ToModelList(res);

            return userList;
        }

        public int AddUser(string userNo, string userName, string password)
        {
            return SqlHelper.ExecuteNonQuery(
                "insert into t_User(UserNo, UserName, Password) values(@UserNo, @UserName, @Password)",
                new SqlParameter("@UserNo", userNo),
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", password));
        }

        public int UpdateUser(int id, string userNo, string userName, string password)
        {
            DataTable dt = SqlHelper.ExecuteTable(
                "select * from t_User where Id = @Id",
                new SqlParameter("@Id", id));
            int rowCount = 0;
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Users user = new Users();
                user.Id = (int)dr["Id"];
                user.UserNo = userNo ?? dr["UserNo"].ToString();
                user.UserName = userName ?? dr["UserName"].ToString();
                user.Passsword = password ?? dr["Password"].ToString();
                rowCount = SqlHelper.ExecuteNonQuery(
                    "update t_User Set UserNo = @UserNo, UserName = @UserName, Password = @Password where Id = @Id",
                    new SqlParameter("@Id", user.Id),
                    new SqlParameter("@UserNo", user.UserNo),
                    new SqlParameter("@UserName", user.UserName),
                    new SqlParameter("@Password", user.Passsword));
            }
            return rowCount;
        }

        public int RemoveUser(int id)
        {
            return SqlHelper.ExecuteNonQuery(
                "delete from t_User where Id = @Id",
                new SqlParameter("@Id", id));
        }

        //DataTable => List<Users>
        private List<Users> ToModelList(DataTable table)
        {
            List<Users> userList = new List<Users>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow dr = table.Rows[i];
                Users user = ToModel(dr);
                userList.Add(user);
            }
            return userList;
        }

        //DataRow => Users
        private Users ToModel(DataRow? dr)
        {
            Users user = new Users();
            if (dr != null)
            {
                user.Id = (int)dr["Id"];
                user.UserNo = dr["UserNo"].ToString();
                user.UserName = dr["UserName"].ToString();
                user.Passsword = dr["Password"].ToString();
                user.IsDelete = (bool)dr["IsDelete"];
            }
            return user;
        }
    }
}