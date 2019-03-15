using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public interface IStockRepository:IDisposable
    {
        List<STK_tblStock> GetStock(STK_tblStock stk);
        bool SaveStock(STK_tblStock stk);
        bool EditStock(STK_tblStock stk);
        bool DeleteStock(int stkId);

        bool SaveProdStock(STK_Balance prodStk);
        List<STK_Balance> GetProdStocks();
        bool DeleteProdStock(STK_Balance prodStock);
        bool EditProdStock(STK_Balance prodStock);
        List<object> GetStockInfoRpt(STK_Transaction stockRpt);
        ReportViewModel GetRepoertViewModel(List<object> objLst, int stockId,int productId, string fromDate, string toDate);
    }
}
