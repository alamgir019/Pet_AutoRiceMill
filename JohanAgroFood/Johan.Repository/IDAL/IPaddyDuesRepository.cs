using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Johan.DATA;

namespace Johan.Repository.IDAL
{
    public interface IPaddyDuesRepository:IDisposable
    {
        tblDue GetDues(int partyId);
        long Save(tblCostingSource objCostSource);          
        BagTransaction GetRemainingBag(int partyId);

        BagTransaction SaveBag(BagTransaction objBag);
    }
}
