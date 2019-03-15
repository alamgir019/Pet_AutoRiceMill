using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public class DBalanceRepository:Disposable,IBalanceRepository
    {    
        private JohanAgroFoodDBEntities context = null;

        public DBalanceRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {

            this.context = context;    
        }

        public List<tblPayable> GetBalanceInfo()
        {
            var results = from pay in context.tblPayables
                          join pr in context.tblParties on pay.partyId equals pr.ID where pay.isActive==1
                          select new 
                          {
                              pay.ID,
                              pay.partyId,
                              pr.name,
                              pay.amount,
                              pay.date,
                              pay.openingBalance
                          };
            List<tblPayable> payableList = new List<tblPayable>();
            foreach (var item in results)
            {
                tblPayable obj = new tblPayable();
                obj.ID = item.ID;
                obj.partyId = item.partyId;
                obj.partyName = item.name;
                obj.amount = item.amount;
                obj.openingBalance = item.openingBalance;
                obj.date = item.date;
                payableList.Add(obj);
            }
            return payableList;
        }


        public long SaveBalance(tblPayable objPayable)
        {
            try
            {
                #region save payable
                long maxId = context.tblPayables.Select(p => p.ID).DefaultIfEmpty(0).Max();
                objPayable.ID = ++maxId;
                objPayable.isActive = 1;
                var lastPayId =
                    context.tblPayables.Where(p => p.partyId == objPayable.partyId)
                        .Select(p => p.ID)
                        .DefaultIfEmpty()
                        .Max();
                
                if (lastPayId>0)
                {
                    var lastPay= context.tblPayables.Where(p => p.ID == lastPayId).FirstOrDefault();
                    lastPay.isActive = 0;
                    objPayable.openingBalance = lastPay.openingBalance+objPayable.amount;
                }
                else
                {
                    objPayable.openingBalance = objPayable.amount;
                }
                context.tblPayables.Add(objPayable);
                #endregion
                return context.SaveChanges() > 0 ? objPayable.ID : 0;   
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }
        //public bool Edit(tblPayable objPayable)
        //{
        //    var orgEditBalanceInfo = context.tblPayables.Where(ss => ss.ID == objPayable.ID).FirstOrDefault();
        //    orgEditBalanceInfo.partyName = objPayable.partyName;
        //    orgEditBalanceInfo.date = objPayable.date;
        //    if (orgEditBalanceInfo.amount > objPayable.amount)
        //    {
        //        var balanceDecrease = orgEditBalanceInfo.amount-objPayable.amount ;
        //        orgEditBalanceInfo.openingBalance -= Convert.ToInt32(balanceDecrease);
        //        orgEditBalanceInfo.amount = objPayable.amount;
        //    }
        //    else if(orgEditBalanceInfo.amount < objPayable.amount)
        //    {
        //        var balanceIncrease =  objPayable.amount-orgEditBalanceInfo.amount;
        //        orgEditBalanceInfo.openingBalance +=Convert.ToInt32(balanceIncrease);
        //        orgEditBalanceInfo.amount = objPayable.amount;
                
        //    }
        //    else
        //    {
        //        orgEditBalanceInfo.amount = objPayable.amount;
        //    }

        //    return context.SaveChanges() > 0;
        //}

        //public bool Delete(int pk)
        //{
        //    var lastpayable = context.tblPayables.Where(ss => ss.ID == pk).FirstOrDefault();
        //    int maxpayId = context.tblPayables.Select(i => i.ID).DefaultIfEmpty(0).Max();
        //    tblPayable objTblPayable = new tblPayable();
        //    objTblPayable.ID = ++maxpayId;
        //    objTblPayable.partyId = lastpayable.partyId;
        //    objTblPayable.date = lastpayable.date;
        //    objTblPayable.amount = lastpayable.amount;
        //    if (lastpayable != null)
        //    {
        //        lastpayable.isActive = 0;
        //        objTblPayable.openingBalance = lastpayable.openingBalance - lastpayable.amount;
        //    }
        //    else
        //    {
        //        objTblPayable.openingBalance = lastpayable.amount;
        //    }
        //    objTblPayable.isActive = 1;

        //    context.tblPayables.Add(objTblPayable);
        //    return context.SaveChanges() > 0;
        //}
    }
}
