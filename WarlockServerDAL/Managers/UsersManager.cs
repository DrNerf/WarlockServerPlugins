using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarlockServerDAL.DALs;

namespace WarlockServerDAL.Managers
{
    public class UsersManager : BaseManager<UsersDAL>
    {
        public UsersManager()
        {
            m_DAL = new UsersDAL();
        }

        public bool TryLogin(string username, string password, out GameUser user)
        {
            var dal = m_DAL;
            user = dal.GetUserByUsernameAndPassword(username, password);
            bool isSuccess = user != null;
            if (isSuccess)
            {
                dal.UpdateLastLogin(user.UserID);
            }
            return isSuccess;
        }

        public GameUser RegisterUser(string username, string email, string password)
        {
            var user = m_DAL.RegisterUser(username, email, password);
            return user;
        }

        public void DeleteUser(GameUser user)
        {
            m_DAL.DeleteUser(user.UserID);
        }
    }
}
