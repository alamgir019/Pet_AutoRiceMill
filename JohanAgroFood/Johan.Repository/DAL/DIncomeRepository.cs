using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public class DIncomeRepository : Disposable, IIncomeRepository
    {
        private JohanAgroFoodDBEntities context = null;

        public DIncomeRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {
            this.context = context;
        }
        public List<tblIncomeSource> GetIncome()
        {
            List<tblIncomeSource> incomes = context.tblIncomeSources.OrderByDescending(dd => dd.date).Take(50).ToList();         
            return incomes;
        }

        public bool SaveIncome(tblIncomeSource income)
        {
            long maxId = context.tblIncomeSources.Select(cc => cc.ID).DefaultIfEmpty(0).Max();
            income.ID = ++maxId;
            context.tblIncomeSources.Add(income);
            return context.SaveChanges() > 0;
        }
        public bool EditIncome(tblIncomeSource income)
        {
            var orgSec = context.tblIncomeSources.Where(ss => ss.ID == income.ID).FirstOrDefault();
            orgSec.amount = income.amount;
            orgSec.date = income.date;
            orgSec.srcDescId = income.srcDescId;
            orgSec.sourceName = income.sourceName;
            orgSec.description = income.description;
            return context.SaveChanges() > 0;
        }
        public bool DeleteIncome(tblIncomeSource income)
        {
            var orgSec = context.tblIncomeSources.Where(ss => ss.ID == income.ID).FirstOrDefault();
            context.tblIncomeSources.Remove(orgSec);
            return context.SaveChanges() > 0;
        }
    }
}
