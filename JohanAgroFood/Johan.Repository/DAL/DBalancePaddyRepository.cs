using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Johan.Repository.IDAL;

namespace Johan.Repository
{
    public class DBalancePaddyRepository : Disposable, IBalancePaddyRepository
    {
        private JohanAgroFoodDBEntities context = null;

        public DBalancePaddyRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {

            this.context = context;
        }

        public List<tblDue> GetBalancePaddyInfo()
        {
            var results = from due in context.tblDues
                          join pr in context.tblParties on due.partyId equals pr.ID
                          where due.isActive == 1
                          select new
                          {
                              due.ID,
                              due.partyId,
                              pr.name,
                              due.amount,
                              due.date,
                              due.openingBalance
                          };
            List<tblDue> dueList = new List<tblDue>();
            foreach (var item in results)
            {
                tblDue obj = new tblDue();
                obj.ID = item.ID;
                obj.partyId = item.partyId;
                obj.partyName = item.name;
                obj.amount = item.amount;
                obj.openingBalance = item.openingBalance;
                obj.date = item.date;
                dueList.Add(obj);
            }
            return dueList;
        }
        
        public long SaveBalancePaddy(tblDue objDue)
        {
            try
            {
                #region save dueable
                long maxId = context.tblDues.Select(p => p.ID).DefaultIfEmpty(0).Max();
                objDue.ID = ++maxId;
                objDue.isActive = 1;
                var lastDueId =
                    context.tblDues.Where(p => p.partyId == objDue.partyId)
                        .Select(p => p.ID)
                        .DefaultIfEmpty()
                        .Max();

                if (lastDueId > 0)
                {
                    var lastdue = context.tblDues.Where(p => p.ID == lastDueId).FirstOrDefault();
                    lastdue.isActive = 0;
                    objDue.openingBalance = lastdue.openingBalance + objDue.amount;
                }
                else
                {
                    objDue.openingBalance = objDue.amount;
                }
                context.tblDues.Add(objDue);
                #endregion
                return context.SaveChanges() > 0 ? objDue.ID : 0;
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }
        
    }
}
