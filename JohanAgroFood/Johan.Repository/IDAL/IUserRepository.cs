using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public interface IUserRepository:IDisposable
    {
        List<tblUser> GetUser();

        bool SaveUser(tblUser user);
        bool Edit(tblUser objUser);
        bool Delete(int pk);
    }
}
