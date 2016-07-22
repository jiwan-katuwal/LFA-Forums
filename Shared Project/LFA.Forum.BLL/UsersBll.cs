using LFA.Forum.BLL.Model;
using LFA.Forum.DAL.Ado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LFA.Forum.BLL
{
    public class UsersBll
    {
        private SqlHelper _sql;

        public UsersBll()
        {
            _sql = new SqlHelper();
        }
        public List<Users> GetAll()
        {
            List<Users> lstUsers = new List<Users>();
            SqlDataReader dr = _sql.ExecuteReader("SELECT * FROM dbo.Users");
            while (dr.Read())
            {
                Users u = new Users();
                u.Created = dr["Created"].ToString() == "" ? 0 : int.Parse(dr["Created"].ToString());
                u.Email = dr["Email"].ToString();
                u.FirstName = dr["FirstName"].ToString();
                u.HashPassword = dr["HashPassword"].ToString();
                u.IsModerator = bool.Parse(dr["IsModerator"].ToString());
                u.LastActivity = DateTime.Parse(dr["LastActivity"].ToString());
                u.LastName = dr["LastName"].ToString();
                u.UserName = dr["UserName"].ToString();
                u.UserStatus = int.Parse(dr["UserStatus"].ToString());

                lstUsers.Add(u);
            }
            dr.Close();
            return lstUsers;
        }

        public int Add(Users u)
        {

            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@UserName",u.UserName),
                new SqlParameter("@HashPassword",u.HashPassword),
                new SqlParameter("@FirstName",u.FirstName),
                new SqlParameter("@LastName",u.LastName),
                new SqlParameter("@Email",u.Email)
            };


            return _sql.ExecuteNonQueryProc("uspAddUser", param);
        }

        public int Update(Users u)
        {
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@UserName",u.UserName),
                new SqlParameter("@HashPassword",u.HashPassword),
                new SqlParameter("@FirstName",u.FirstName),
                new SqlParameter("@LastName",u.LastName),
                new SqlParameter("@Email",u.Email)
            };

            return _sql.ExecuteNonQueryProc("uspUpdateUserByUserName", param);
        }

        public int Delete(int userID)
        {
            return _sql.ExecuteNonQuery("DELETE FROM dbo.Users WHERE ID =@ID", "@ID", userID);
        }

    }
}
