using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarlockServerDAL.DALs
{
    public abstract class BaseDAL : IDisposable
    {
        protected WarlockServerDBEntities m_DBEntities = new WarlockServerDBEntities();
        
        public void Dispose()
        {
            m_DBEntities.Dispose();
        }
    }
}
