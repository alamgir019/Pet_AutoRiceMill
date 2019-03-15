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
    public class DPaddyDuesRepository : Disposable, IPaddyDuesRepository
    {
        private JohanAgroFoodDBEntities context = null;
        public DPaddyDuesRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {
            this.context = context;
        }

        public tblDue GetDues(int partyId)
        {
            var results = from pay in context.tblDues
                          join pr in context.tblParties on pay.partyId equals pr.ID
                          where pay.partyId == partyId && pay.isActive == 1
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
        public BagTransaction GetRemainingBag(int partyId)
        {
            var results = from sack in context.BagTransactions
                          join pr in context.tblParties on sack.partyId equals pr.ID
                          where sack.partyId == partyId && (sack.rcvPrice == 0 || sack.rcvPrice == null) && (sack.sentPrice == 0 || sack.sentPrice == null)
                          orderby sack.date descending, sack.ID descending
                          select new
                          {
                              sack.ID,
                              sack.partyId,
                              pr.name,
                              sack.comRcvBag,
                              sack.comSentBag,
                              sack.bagDues,
                              sack.date
                          };
            int bgdue = 0;
            foreach (var item in results)
            {
                bgdue += item.comRcvBag ?? 0 - item.comSentBag ?? 0;
            }
            var pric = (from sack in context.BagTransactions
                        join pr in context.tblParties on sack.partyId equals pr.ID
                        where sack.partyId == partyId && (sack.rcvPrice > 0 || sack.sentPrice > 0)
                        orderby sack.date descending, sack.ID descending
                        select sack);


            double price = 0;
            foreach (var item in pric)
            {
                price += item.comRcvBag ?? 0 * item.rcvPrice ?? 0 - item.comSentBag ?? 0 * item.sentPrice ?? 0;
            }

            //var item = results.FirstOrDefault();
            BagTransaction objSackInfo = new BagTransaction();
            //if (item == null && price==null)
            //{
            //    return objSackInfo;
            //}
            //objSackInfo.ID = item==null?price.ID: item.ID;
            //objSackInfo.partyId = item==null?price.partyId: item.partyId;
            //objSackInfo.partyName = item==null?price.partyName: item.name;
            objSackInfo.priceDues = price;
            //objSackInfo.date =item==null?price.date:item.date;
            objSackInfo.bagDues = bgdue;
            return objSackInfo;
        }
        public BagTransaction SaveBag(BagTransaction objBag)
        {
            long maxId = context.BagTransactions.Select(s => s.ID).DefaultIfEmpty(0).Max();

            var results = from sack in context.BagTransactions
                          where sack.partyId == objBag.partyId.Value && sack.date <= objBag.date && (sack.rcvPrice == 0 || sack.rcvPrice == null) && (sack.sentPrice == 0 || sack.sentPrice == null)
                          orderby sack.date descending, sack.ID descending
                          select sack;
            var prevDue = results.FirstOrDefault();

            var pric = (from sack in context.BagTransactions
                        where sack.partyId == objBag.partyId.Value && sack.date <= objBag.date && (sack.rcvPrice > 0 || sack.sentPrice > 0)
                        orderby sack.date descending, sack.ID descending
                        select sack);
            var prevprice = pric.FirstOrDefault();

            if (objBag.sentPrice > 0)
            {
                int bag = objBag.comSentBag > 0 ? objBag.comSentBag.Value : 1;
                objBag.priceDues = prevprice.priceDues - objBag.sentPrice * bag;

                var nextprices = from sack in context.BagTransactions
                                 where sack.partyId == objBag.partyId.Value && sack.date > objBag.date && (sack.rcvPrice > 0 || sack.sentPrice > 0)
                                 select sack;
                foreach (var item in nextprices)
                {
                    item.priceDues -= objBag.sentPrice * bag;
                }

            }
            else
            {
                objBag.bagDues = prevDue.bagDues - objBag.comSentBag;

                var nextBags = from sack in context.BagTransactions
                               where sack.partyId == objBag.partyId.Value && sack.date > objBag.date && (sack.rcvPrice == 0 || sack.rcvPrice == null) && (sack.sentPrice == 0 || sack.sentPrice == null)
                               select sack;
                foreach (var item in nextBags)
                {
                    item.bagDues -= objBag.comSentBag;
                }
            }

            objBag.ID = ++maxId;
            context.BagTransactions.Add(objBag);
            return context.SaveChanges() > 0 ? objBag : null;
        }
        public long Save(tblCostingSource objCostSource)
        {
            try
            {
                //int maxId = context.tblCostingSources.Select(ss => ss.ID).DefaultIfEmpty(0).Max();
                //long maxDueId = context.tblDues.Select(pp => pp.ID).DefaultIfEmpty(0).Max();
                //objCostSource.ID = ++maxId;
                //objCostSource.description = objCostSource.sourceName + " বকেয়া বাবদ আয়";
                //objCostSource.isDue = 1;

                //context.tblCostingSources.Add(objCostSource);

                //tblDue payableChange =
                //    context.tblDues.Where(ss => ss.partyId == objCostSource.partyId && ss.isActive==1).FirstOrDefault();
                //payableChange.isActive = 0;
                //tblDue newdue = new tblDue();
                //newdue.ID = ++maxDueId;
                //newdue.isActive = 1;
                //newdue.amount = (-1)*objCostSource.amount;
                //newdue.openingBalance = payableChange.openingBalance - objCostSource.amount;
                //newdue.date = objCostSource.date;
                //newdue.partyId = objCostSource.partyId.Value;
                //newdue.incSrcId = objCostSource.ID;
                //context.tblDues.Add(newdue);

                return context.SaveChanges() > 0 ? objCostSource.ID : 0;
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }
    }
}
