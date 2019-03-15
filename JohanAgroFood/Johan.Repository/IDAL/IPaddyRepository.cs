using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public interface IPaddyRepository : IDisposable
    {
        List<tblBuy> GetPaddyInfo();
        long SavePaddy(tblBuy paddyInfo);
        long EditPaddyInfo(tblBuy paddyInfo);
        bool DeletePaddy(int pk);
        List<object> GetPaddyInfoRpt(tblBuy paddyRpt);
        double GetDues(int partyId, DateTime fromDate, DateTime toDate);
        ReportViewModel GetReportViewModel(List<object> objLst, int partyId, string fromDate, string toDate);
    }
}
