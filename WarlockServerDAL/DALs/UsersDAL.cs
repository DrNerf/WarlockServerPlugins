using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarlockServerDAL.DALs
{
    public class UsersDAL : BaseDAL
    {
        public GameUser GetUserByUsernameAndPassword(string username, string password)
        {
            return m_DBEntities.GameUsers
                .FirstOrDefault(x => x.Username == username && x.Password == password);
        }

        public GameUser RegisterUser(string username, string email, string password)
        {
            var user = m_DBEntities.GameUsers.Add(new GameUser 
            {
                Username = username,
                Email = email,
                Password = password,
            });
            m_DBEntities.SaveChanges();
            return user;
        }

        public void UpdateLastLogin(int userID)
        {
            var user = m_DBEntities.GameUsers.FirstOrDefault(x => x.UserID == userID);
            if (user == null)
                return;

            user.LastLogin = DateTime.Now;
            m_DBEntities.SaveChanges();
        }

        public void DeleteUser(int userID)
        {
            var user = m_DBEntities.GameUsers.FirstOrDefault(x => x.UserID == userID);
            if (user == null)
                return;

            m_DBEntities.GameUsers.Remove(user);
            m_DBEntities.SaveChanges();
        }
    }
}
