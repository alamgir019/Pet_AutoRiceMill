using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Johan.DATA;

namespace Johan.Repository.IDAL
{
    public interface IBalancePaddyRepository : IDisposable
    {

        List<tblDue> GetBalancePaddyInfo();
        long SaveBalancePaddy(tblDue objDue);
            
        
    }
}
