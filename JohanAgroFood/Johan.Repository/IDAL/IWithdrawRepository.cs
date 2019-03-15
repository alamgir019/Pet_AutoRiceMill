using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public interface IWithdrawRepository : IDisposable
    {
        tblCostingSource Save(tblCostingSource withdraw);

    }
}
