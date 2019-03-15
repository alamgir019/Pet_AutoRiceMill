using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public interface IHuskSellRepository : IDisposable
    {
        List<tblSell> GetHuskInfo();

        tblSell SaveHusk(tblSell huskInfo);

        List<STK_tblStock> GetStock();
        List<tblProduct> LoadHusk(int stockId);
        bool DeleteHuskSell(int pk);
        tblSell EditHuskInfo(tblSell huskInfo);
        List<object> GetHuskInfoRpt(tblSell huskRpt);
        List<object> GetHuskIncomeRpt(tblSell huskRpt);
        ReportViewModel GetRepoertViewModel(List<object> objLst, int partyId, string fromDate, string toDate);
        ReportViewModel GetIncomeViewModel(List<object> objLst, string fromDate, string toDate);
    }
}
