using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public interface IConsumptionRepository:IDisposable
    {
        List<tblCostingSource> GetConsumption();

        bool SaveConsumption(tblCostingSource Consumption);

        bool EditConsumption(tblCostingSource consumption);

        bool DeleteConsumption(tblCostingSource consumption);
    }
}
