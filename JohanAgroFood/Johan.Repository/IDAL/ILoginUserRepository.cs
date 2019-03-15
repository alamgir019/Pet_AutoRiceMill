using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Johan.DATA;

namespace Johan.Repository
{
    public interface ILoginUserRepository : IDisposable
    {
        tblUser GetUserById(tblUser user);        
    }
}