using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Johan.DATA;

namespace Johan.Repository
{
    public interface IBalanceRepository:IDisposable
    {
        List<tblPayable> GetBalanceInfo();
        long SaveBalance(tblPayable objPayable);
        //bool Edit(tblPayable objPayable);
        //bool Delete(int pk);
    }
}
