using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public interface IRiceRepository : IDisposable
    {
        List<tblSell> GetRiceInfo();

        tblSell SaveRice(tblSell riceInfo);
                                      
        tblSell EditRiceSell(tblSell riceInfo);
        bool DeleteRiceSell(int pk);
        List<object> GetRiceInfoRpt(tblSell riceRpt);

        List<tblProduct> LoadRice();
        double GetDues(int partyId);
        ReportViewModel GetRepoertViewModel(List<object> objLst, int partyId, string fromDate, string toDate);
    }
}
