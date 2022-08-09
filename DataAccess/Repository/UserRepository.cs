using Application.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRepository
    {
        public List<UserLogin> TestUsers;
        public UserRepository()
        {
            TestUsers = new List<UserLogin>();
            TestUsers.Add(new UserLogin() { UserName = "User1", Password  = "Pass1" });
            TestUsers.Add(new UserLogin() { UserName = "User2", Password = "Pass2" });
        }
        public UserLogin GetUser(string username)
        {
            try
            {
                return TestUsers.First(user => user.UserName.Equals(username));
            }
            catch
            {
                return null;
            }
        }
    }
}
