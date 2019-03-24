using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Johan.DATA;

namespace Johan.Repository.IDAL
{
    public interface IReportRepository:IDisposable
    {
        List<object> GetDailySellInfoRpt(tblSell dailySellRpt);
        ReportViewModel GetRepoertViewModel(List<object> objLst,int productId,string todayDate);

        List<object> GetOtherConsumpRpt(string starDate, string endDate,int sectorId);
        ReportViewModel GetOtherConsumpViewModel(List<object> objLst, string startDate, string endDate, int sectorId);
        List<object> GetIncomeRpt(string starDate, string endDate, int sectorId);
        ReportViewModel GetIncomeVM(List<object> objLst, string startDate, string endDate, int sectorId);
        List<object> GetHollarConsumpRpt(string starDate, string endDate,string stockId);
        ReportViewModel GetHollarConsumpViewModel(List<object> objLst, string startDate, string endDate,string stockId);

        List<object> GetPaddyStockRpt(PaddyTransaction stockRpt);

        ReportViewModel GetPaddyStockViewModel(List<object> objLst, int stkId, int pId, string from, string to);

        List<object> GetDailyBuyInfoRpt(tblBuy dailyBuyRpt);
        ReportViewModel GetDailyBuyVM(List<object> objLst, string todayDate);
    }
}
