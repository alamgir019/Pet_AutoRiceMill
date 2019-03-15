using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public interface ICarRepository : IDisposable
    {
        bool SaveCar(tblCar objcar);
        List<tblCar> GetCar();
        bool EditCar(tblCar car);
        bool DeleteCar(int pk);
        List<object> GetCarRpt(tblCar carRpt);
        ReportViewModel CarViewModel(List<object> objLst, string fromDate, string toDate);

    }
}
