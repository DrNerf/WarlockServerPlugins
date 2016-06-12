using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarlockServerDAL.DALs;

namespace WarlockServerDAL.Managers
{
    public abstract class BaseManager<T> : IDisposable
    {
        protected T m_DAL;

        public void Dispose()
        {
            (m_DAL as BaseDAL).Dispose();
        }
    }
}
