using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Johan.DATA;
using Johan.Repository.IDAL;

namespace Johan.Repository.DAL
{
    public class DDuePaymentRepository : Disposable, IDuePaymentRepository
    {
        private JohanAgroFoodDBEntities context = null;
        public DDuePaymentRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {
            this.context = context;
        }

        public tblPayable GetDuePayment(int objPartyId)
        {
            var results = from pay in context.tblPayables
                          join pr in context.tblParties on pay.partyId equals pr.ID
                          where pay.partyId == objPartyId && pay.isActive==1
                          select new
                          {
                              pay.ID,
                              pay.partyId,
                              pr.name,
                              pay.amount,
                              pay.date,
                              pay.openingBalance
                          };
            var item = results.FirstOrDefault();
            tblPayable objPay = new tblPayable();
            if (item==null)
            {
                return objPay;
            }
            objPay.ID = item.ID;
            objPay.partyId = item.partyId;
            objPay.partyName = item.name;
            objPay.amount = item.amount;
            objPay.date = item.date;
            objPay.openingBalance = item.openingBalance;
            return objPay;
        }

        public List<tblIncomeSource> GetDuePaid(DateTime date)
        {
            var results = from paid in context.tblIncomeSources
                join pr in context.tblParties on paid.partyId equals pr.ID
                where paid.isDue == 1 && paid.date == date
                select new
                {
                    pr.name,
                    paid.amount
                };
            List<tblIncomeSource> objList=new List<tblIncomeSource>();
            
            foreach (var item in results)
            {
                tblIncomeSource objIncomeSource=new tblIncomeSource();
                objIncomeSource.partyName = item.name;
                objIncomeSource.amount = item.amount;
                objList.Add(objIncomeSource);

            }
            return objList;
        }

        public long Save(tblIncomeSource objDueIncomeSource)
        {
            try
            {
                long maxId = context.tblIncomeSources.Select(ss => ss.ID).DefaultIfEmpty(0).Max();
                long maxPayId = context.tblPayables.Select(pp => pp.ID).DefaultIfEmpty(0).Max();
                objDueIncomeSource.ID = ++maxId;
                if (objDueIncomeSource.description!=null)
                {
                    objDueIncomeSource.description = objDueIncomeSource.sourceName + " বকেয়া বাবদ আয়" + "," + objDueIncomeSource.description;
                }
                else
                {
                    objDueIncomeSource.description = objDueIncomeSource.sourceName + " বকেয়া বাবদ আয়";
                }
               
                objDueIncomeSource.isDue = 1;
                
                context.tblIncomeSources.Add(objDueIncomeSource);

                tblPayable payableChange =
                    context.tblPayables.Where(ss => ss.partyId == objDueIncomeSource.partyId && ss.isActive==1).FirstOrDefault();
                payableChange.isActive = 0;
                tblPayable newPayable = new tblPayable();
                newPayable.ID = ++maxPayId;
                newPayable.isActive = 1;
                newPayable.amount = (-1)*objDueIncomeSource.amount;
                newPayable.openingBalance = payableChange.openingBalance - objDueIncomeSource.amount;
                newPayable.date = objDueIncomeSource.date;
                newPayable.partyId = objDueIncomeSource.partyId.Value;
                newPayable.incSrcId = objDueIncomeSource.ID;
                context.tblPayables.Add(newPayable);

                return context.SaveChanges() > 0 ? objDueIncomeSource.ID : 0;
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }
    }
}
