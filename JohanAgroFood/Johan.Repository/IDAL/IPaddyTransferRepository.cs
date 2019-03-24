using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Johan.DATA;

namespace Johan.Repository.IDAL
{
    public interface IPaddyTransferRepository:IDisposable
    {
        List<STK_Balance> GetSackWeights(int productId,int stockId);
        long SavePaddyTransfer(STK_Balance objStkBalance);
        List<STK_Balance> GetPaddyTransferInfos();
    //bool EditPaddyTransfer(STK_Balance objStkBalance);
    //    bool DeletePaddyTransfer(int pk);
    }
}
