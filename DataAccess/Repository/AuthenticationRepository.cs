using Application.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AuthenticationRepository
    {
        public List<Authentication> TestUsers;
        public AuthenticationRepository()
        {
            TestUsers = new List<Authentication>();
            TestUsers.Add(new Authentication() { UserName = "User1", Password  = "Pass1" });
            TestUsers.Add(new Authentication() { UserName = "User2", Password = "Pass2" });
        }
        public Authentication GetUser(string username)
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
