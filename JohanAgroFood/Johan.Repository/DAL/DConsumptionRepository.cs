using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public class DConsumptionRepository : Disposable, IConsumptionRepository
    {
        private JohanAgroFoodDBEntities context = null;

        public DConsumptionRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {
            this.context = context;
        }
        public List<tblCostingSource> GetConsumption()
        {
            List<tblCostingSource> consumptions = context.tblCostingSources.Where(cc => cc.amount > 0).OrderByDescending(dd => dd.date).Take(100).ToList();
            //List<tblCostingSource> consumptions = context.tblCostingSources.Where(cc => cc.amount > 0).ToList();         
            return consumptions;
        }

        public bool SaveConsumption(tblCostingSource consumption)
        {
            long maxId = context.tblCostingSources.Select(cc => cc.ID).DefaultIfEmpty(0).Max();
            consumption.ID = ++maxId;
            context.tblCostingSources.Add(consumption);
            return context.SaveChanges() > 0;
        }
        public bool EditConsumption(tblCostingSource consumption)
        {
            var orgSec = context.tblCostingSources.Where(ss => ss.ID == consumption.ID).FirstOrDefault();
            orgSec.amount = consumption.amount;
            orgSec.date = consumption.date;
            orgSec.srcDescId = consumption.srcDescId;
            orgSec.sourceName = consumption.sourceName;
            orgSec.srcDescription = consumption.srcDescription;
            return context.SaveChanges() > 0;
        }
        public bool DeleteConsumption(tblCostingSource consumption)
        {
            var orgSec = context.tblCostingSources.Where(ss => ss.ID == consumption.ID).FirstOrDefault();
            context.tblCostingSources.Remove(orgSec);
            return context.SaveChanges() > 0;
        }

    }
}
