using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Johan.DATA;
using Johan.Repository.IDAL;

namespace Johan.Repository.DAL
{
    public class DLoanPaymentRepository : Disposable, ILoanPaymentRepository
    {
        private JohanAgroFoodDBEntities context = null;
        public DLoanPaymentRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {
            this.context = context;
        }
        public tblDue GetLoanPayment(int objPartyId)
        {
            var results = from due in context.tblDues
                          join pr in context.tblParties on due.partyId equals pr.ID
                          where due.partyId == objPartyId
                          orderby due.date descending, due.ID descending
                          select new
                          {
                              due.ID,
                              due.partyId,
                              pr.name,
                              due.amount,
                              due.date,
                              due.openingBalance
                          };
            var item = results.FirstOrDefault();
            tblDue objDue = new tblDue();
            if (item == null)
            {
                return objDue;
            }
            objDue.ID = item.ID;
            objDue.partyId = item.partyId;
            objDue.partyName = item.name;
            objDue.amount = item.amount;
            objDue.date = item.date;
            objDue.openingBalance = item.openingBalance;
            return objDue;
        }
        public long Save(tblCostingSource objLoanCostingSource)
        {
            try
            {
                long maxId = context.tblCostingSources.Select(ss => ss.ID).DefaultIfEmpty(0).Max();
                long maxDueId = context.tblDues.Select(pp => pp.ID).DefaultIfEmpty(0).Max();
                objLoanCostingSource.ID = ++maxId;
                objLoanCostingSource.srcDescription = "ধান ক্রয় বাবদ ব্যায় পরিশোধ";
                objLoanCostingSource.sourceName = "ধান";
                objLoanCostingSource.srcDescId = 23;
                objLoanCostingSource.isDue = 1;
                context.tblCostingSources.Add(objLoanCostingSource);

                tblDue latestDue =
                    context.tblDues.Where(ss => ss.partyId == objLoanCostingSource.partyId && ss.date <= objLoanCostingSource.date).OrderByDescending(ss => ss.date).ThenByDescending(ss => ss.ID).FirstOrDefault();
                List<tblDue> nextDues = context.tblDues.Where(dd => dd.partyId == objLoanCostingSource.partyId && dd.date > objLoanCostingSource.date).ToList();
                //dueChange.isActive = 0;
                tblDue newDue = new tblDue();
                newDue.ID = ++maxDueId;
                //newDue.isActive = 1;
                newDue.costingId = objLoanCostingSource.ID;
                newDue.amount = (-1) * objLoanCostingSource.amount;
                if (latestDue == null)
                {
                    newDue.openingBalance = newDue.amount;
                }
                else
                {
                    newDue.openingBalance = latestDue.openingBalance + newDue.amount;
                }
                newDue.date = objLoanCostingSource.date;
                newDue.partyId = objLoanCostingSource.partyId.Value;

                if (nextDues != null)
                {
                    foreach (var item in nextDues)
                    {
                        item.openingBalance += newDue.amount;
                    }
                }

                context.tblDues.Add(newDue);

                return context.SaveChanges() > 0 ? objLoanCostingSource.ID : 0;
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }
        public List<tblCostingSource> GetLoanPaid(DateTime date)
        {
            var results = from loan in context.tblCostingSources
                          join pr in context.tblParties on loan.partyId equals pr.ID
                          where loan.isDue == 1 && loan.date == date
                          select new
                          {
                              pr.name,
                              loan.amount
                          };
            List<tblCostingSource> objList = new List<tblCostingSource>();

            foreach (var item in results)
            {
                tblCostingSource objCostingSource = new tblCostingSource();
                objCostingSource.partyName = item.name;
                objCostingSource.amount = item.amount;
                objList.Add(objCostingSource);

            }
            return objList;
        }

    }
}
