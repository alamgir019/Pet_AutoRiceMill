using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.WebForms;

namespace Johan.Repository
{
    public class DPaddyRepository : Disposable, IPaddyRepository
    {
        private JohanAgroFoodDBEntities context = null;

        public DPaddyRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {

            this.context = context;
        }
        public List<tblBuy> GetPaddyInfo()
        {
            var data = context.sp_GetPaddyBuyInfo();
            List<tblBuy> buys = new List<tblBuy>();
            foreach (var item in data)
            {
                tblBuy buyobj = new tblBuy();
                buyobj.ID = item.ID;
                buyobj.date = item.date;
                buyobj.partyId = item.partyId;
                buyobj.partyName = item.partyName;
                buyobj.productId = item.productId;
                buyobj.productName = item.productName;
                buyobj.noOfBag = item.noOfBag;
                buyobj.stockId = item.stockId;
                buyobj.stockName = item.stockName;
                buyobj.unit = item.unit;
                buyobj.price = item.price;
                buyobj.transportCost = item.transportCost;
                buyobj.truckNumber = item.truckNumber;
                buyobj.labourCostPerBag = item.labourCostPerBag;
                buyobj.quantityPerBag = item.quantityPerBag;
                buyobj.amount = item.amount ?? 0;
                //buyobj.comRcvBag = Convert.ToInt32(item.comRcvBag);
                //buyobj.noOfSackRcvd = Convert.ToInt32(item.noOfSackRcvd);
                buyobj.bagPrice = item.rcvPrice;
                buys.Add(buyobj);
            }
            return buys;
        }


        // just try to avoid opening values
        public long SavePaddy(tblBuy paddyInfo)
        {
            try
            {
                long maxId = context.tblBuys.Select(p => p.ID).DefaultIfEmpty(0).Max();
                paddyInfo.ID = ++maxId;
                if (paddyInfo.amount > 0 || paddyInfo.labourCostPerBag > 0 || paddyInfo.transportCost > 0)
                {
                    #region costing source
                    long cstId = context.tblCostingSources.Select(i => i.ID).DefaultIfEmpty(0).Max();
                    tblCostingSource costObj = new tblCostingSource();
                    costObj.ID = ++cstId;
                    costObj.sourceName = "ধান"; // shoul be come from commonelement
                    costObj.srcDescId = 23; // should be come from commonelemnt
                    costObj.srcDescription = "ধান ক্রয় বাবদ খরচ";
                    // costObj.amount =  paddyInfo.amount + labCost* paddyInfo.noOfBag+ paddyInfo.transportCost ?? 0;
                    costObj.labourCostPerBag = paddyInfo.labourCostPerBag ?? 0;
                    if (paddyInfo.transportCostInclude)
                    {
                        costObj.transportCost = paddyInfo.transportCost ?? 0;
                    }
                    else
                    {
                        costObj.transportCost = 0;
                    }

                    costObj.amount = paddyInfo.amount;
                    costObj.date = paddyInfo.date;
                    costObj.partyId = paddyInfo.partyId;
                    costObj.buyId = paddyInfo.ID;
                    context.tblCostingSources.Add(costObj);

                    #endregion
                }

                #region save dues
                long maxdueId = context.tblDues.Select(i => i.ID).DefaultIfEmpty(0).Max();
                tblDue objDue = new tblDue();
                objDue.ID = ++maxdueId;
                objDue.partyId = paddyInfo.partyId;
                objDue.buyId = paddyInfo.ID;
                objDue.date = paddyInfo.date;

                //tblDue dueItem = context.tblDues.Where(bb => bb.date <= paddyInfo.date && bb.partyId == paddyInfo.partyId).OrderByDescending(oo => oo.date).ThenByDescending(ii => ii.ID).FirstOrDefault();
                //List<tblDue> dueItems = context.tblDues.Where(bb => bb.date > paddyInfo.date && bb.partyId == paddyInfo.partyId).ToList();
                double? totalPr = paddyInfo.price * paddyInfo.quantityPerBag / 40 * paddyInfo.noOfBag;
                var due = totalPr - paddyInfo.amount;
                objDue.amount = due;
                //if (dueItem == null)
                //{
                //    objDue.openingBalance = objDue.amount;
                //}
                //else
                //{
                //    objDue.openingBalance = dueItem.openingBalance + objDue.amount;
                //}
                //if (dueItems != null)
                //{
                //    foreach (var item in dueItems)
                //    {
                //        item.openingBalance += objDue.amount;
                //    }
                //}
                context.tblDues.Add(objDue);
                #endregion

                context.tblBuys.Add(paddyInfo);

                #region stock balance
                STK_Balance padStk = context.STK_Balance.Where(ss => ss.stockId == paddyInfo.stockId && ss.productId == paddyInfo.productId && ss.sackWeight == paddyInfo.quantityPerBag).FirstOrDefault();
                if (padStk != null)
                {
                    padStk.sackQuantity += paddyInfo.noOfBag;
                }
                else
                {
                    STK_Balance newStk = new STK_Balance();
                    int maxbalId = context.STK_Balance.Select(p => p.ID).DefaultIfEmpty(0).Max();
                    newStk.ID = ++maxbalId;
                    newStk.productId = paddyInfo.productId;
                    newStk.stockId = paddyInfo.stockId;
                    newStk.sackWeight = paddyInfo.quantityPerBag;
                    newStk.sackQuantity = paddyInfo.noOfBag;
                    context.STK_Balance.Add(newStk);
                }
                #endregion

                #region stock transaction
                long maxprdstkId = context.PaddyTransactions.Select(p => p.ID).DefaultIfEmpty(0).Max();
                long laststkId = context.PaddyTransactions.Where(s => s.stockId == paddyInfo.stockId && s.prodId == paddyInfo.productId && s.bagWeight == paddyInfo.quantityPerBag).Select(l => l.ID).DefaultIfEmpty(0).Max();
                var lastTrans = context.PaddyTransactions.Where(ll => ll.ID == laststkId).FirstOrDefault();

                PaddyTransaction objStkTrans = new PaddyTransaction();
                objStkTrans.ID = maxprdstkId + 1;
                objStkTrans.date = paddyInfo.date;
                objStkTrans.rcvQty = paddyInfo.noOfBag;
                objStkTrans.releaseQty = 0;
                objStkTrans.stockId = paddyInfo.stockId;
                objStkTrans.prodId = paddyInfo.productId;
                objStkTrans.buyId = paddyInfo.ID;
                objStkTrans.bagWeight = paddyInfo.quantityPerBag;
                objStkTrans.openingStock = lastTrans == null ? paddyInfo.noOfBag : lastTrans.openingStock + paddyInfo.noOfBag;
                context.PaddyTransactions.Add(objStkTrans);
                #endregion

                #region BagTransactions
                long maxBagId = context.BagTransactions.Select(b => b.ID).DefaultIfEmpty(0).Max();
                if (paddyInfo.bagPrice > 0)
                {
                    BagTransaction prevItem = context.BagTransactions.Where(bb => bb.date <= paddyInfo.date && bb.partyId == paddyInfo.partyId && (bb.rcvPrice > 0 || bb.sentPrice>0) ).OrderByDescending(oo => oo.date).ThenByDescending(ii => ii.ID).FirstOrDefault();
                    List<BagTransaction> nextItems = context.BagTransactions.Where(bb => bb.date > paddyInfo.date && bb.partyId == paddyInfo.partyId && (bb.rcvPrice > 0 || bb.sentPrice > 0)).ToList();
                    //int bagCnt = paddyInfo.noOfBag > 0 ? paddyInfo.noOfBag : 1;
                    double? curPriceDue = prevItem == null ? paddyInfo.noOfBag * paddyInfo.bagPrice : prevItem.priceDues + paddyInfo.noOfBag * paddyInfo.bagPrice;

                    BagTransaction bagTrans = new BagTransaction();
                    bagTrans.ID = ++maxBagId;
                    bagTrans.partyId = paddyInfo.partyId;
                    bagTrans.rcvId = paddyInfo.ID;
                    bagTrans.comRcvBag = paddyInfo.noOfBag;
                    bagTrans.rcvPrice = paddyInfo.bagPrice;
                    bagTrans.comSentBag = 0;
                    bagTrans.sentPrice = 0;
                    bagTrans.priceDues = curPriceDue;
                    bagTrans.bagDues = 0;
                    bagTrans.date = paddyInfo.date;
                    context.BagTransactions.Add(bagTrans);

                    if (nextItems != null)
                    {
                        foreach (var item in nextItems)
                        {
                            item.priceDues += curPriceDue;
                        }
                    }
                }
                else
                {
                    BagTransaction prevItem = context.BagTransactions.Where(bb => bb.date <= paddyInfo.date && bb.partyId == paddyInfo.partyId && (bb.rcvPrice == 0 || bb.rcvPrice == null) && (bb.sentPrice == 0 || bb.sentPrice == null)).OrderByDescending(oo => oo.date).ThenByDescending(ii => ii.ID).FirstOrDefault();
                    List<BagTransaction> nextItems = context.BagTransactions.Where(bb => bb.date > paddyInfo.date && bb.partyId == paddyInfo.partyId && (bb.rcvPrice == 0 || bb.rcvPrice == null) && (bb.sentPrice == 0 || bb.sentPrice == null)).ToList();
                    int? curBagDue = prevItem == null ? paddyInfo.noOfBag : prevItem.bagDues + paddyInfo.noOfBag;

                    BagTransaction bagTrans = new BagTransaction();
                    bagTrans.ID = ++maxBagId;
                    bagTrans.partyId = paddyInfo.partyId;
                    bagTrans.rcvId = paddyInfo.ID;
                    bagTrans.comRcvBag = paddyInfo.noOfBag;
                    bagTrans.rcvPrice = 0;
                    bagTrans.comSentBag = 0;
                    bagTrans.sentPrice = 0;
                    bagTrans.priceDues = 0;
                    bagTrans.bagDues = curBagDue;
                    bagTrans.date = paddyInfo.date;
                    context.BagTransactions.Add(bagTrans);

                    if (nextItems != null)
                    {
                        foreach (var item in nextItems)
                        {
                            item.bagDues += curBagDue;
                        }
                    }
                }

                #endregion

                return context.SaveChanges() > 0 ? paddyInfo.ID : 0;
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }

        public long EditPaddyInfo(tblBuy paddyInfo)
        {
            var orgPaddy = context.tblBuys.Where(ss => ss.ID == paddyInfo.ID).FirstOrDefault();

            #region edit paddy stock
            STK_Balance padStk = context.STK_Balance.Where(ss => ss.stockId == orgPaddy.stockId && ss.productId == orgPaddy.productId && ss.sackWeight == orgPaddy.quantityPerBag).FirstOrDefault();
            if (padStk!=null)
            {
                padStk.sackQuantity -= orgPaddy.noOfBag;
            }

            STK_Balance curStk = context.STK_Balance.Where(ss => ss.stockId == paddyInfo.stockId && ss.productId == paddyInfo.productId && ss.sackWeight == paddyInfo.quantityPerBag).FirstOrDefault();
            if (curStk != null)
            {
                curStk.sackQuantity += paddyInfo.noOfBag;
            }
            else
            {
                STK_Balance newStk = new STK_Balance();
                int maxbalId = context.STK_Balance.Select(p => p.ID).DefaultIfEmpty(0).Max();
                newStk.ID = ++maxbalId;
                newStk.productId = paddyInfo.productId;
                newStk.stockId = paddyInfo.stockId;
                newStk.sackWeight = paddyInfo.quantityPerBag;
                newStk.sackQuantity = paddyInfo.noOfBag;
                context.STK_Balance.Add(newStk);
            }
            #endregion

            #region edit costing source
            var orgCostSrc = context.tblCostingSources.Where(ii => ii.buyId == paddyInfo.ID).FirstOrDefault();
            if (orgCostSrc != null)
            {
                orgCostSrc.amount = paddyInfo.amount;
                orgCostSrc.date = paddyInfo.date;
                if (paddyInfo.transportCostInclude)
                {
                    orgCostSrc.transportCost = paddyInfo.transportCost;
                }
                else
                {
                    orgCostSrc.transportCost = 0;
                }
                orgCostSrc.labourCostPerBag = paddyInfo.labourCostPerBag;
                orgCostSrc.partyId = paddyInfo.partyId;
            }
            else if (orgCostSrc == null &&( paddyInfo.amount > 0 || paddyInfo.transportCost>0))
            {
                long cstId = context.tblCostingSources.Select(i => i.ID).DefaultIfEmpty(0).Max();
                tblCostingSource cstObj = new tblCostingSource();
                cstObj.ID = ++cstId;
                cstObj.sourceName = "ধান"; // shoul be come from commonelement
                cstObj.srcDescId = 23; // should be come from commonelemnt
                cstObj.srcDescription = "ধান ক্রয় বাবদ খরচ";
                cstObj.amount = paddyInfo.amount;
                cstObj.date = paddyInfo.date;
                cstObj.buyId = paddyInfo.ID;
                cstObj.partyId = paddyInfo.partyId;
                cstObj.labourCostPerBag = paddyInfo.labourCostPerBag ?? 0;
                if (paddyInfo.transportCostInclude)
                {
                    cstObj.transportCost = paddyInfo.transportCost ?? 0;
                }
                else
                {
                    cstObj.transportCost = 0;
                }
                context.tblCostingSources.Add(cstObj);

            }
            #endregion

            #region edit stock transaction

            var orgStkTrans = context.PaddyTransactions.Where(ss => ss.buyId == orgPaddy.ID).FirstOrDefault();
            if (orgStkTrans.stockId==paddyInfo.stockId)
            {    
                double diff = orgStkTrans.rcvQty.Value - paddyInfo.noOfBag;
                orgStkTrans.openingStock = orgStkTrans.openingStock - orgStkTrans.rcvQty.Value;

                orgStkTrans.date = paddyInfo.date;
                orgStkTrans.rcvQty = paddyInfo.noOfBag;
                orgStkTrans.releaseQty = 0;
                orgStkTrans.stockId = paddyInfo.stockId;
                orgStkTrans.prodId = paddyInfo.productId;
                orgStkTrans.openingStock = orgStkTrans.openingStock + paddyInfo.noOfBag;
                // here may be change for different stock
                var nextStkTrans = context.PaddyTransactions.Where(ss => ss.ID > orgStkTrans.ID && ss.prodId == orgStkTrans.prodId && ss.stockId == orgStkTrans.stockId && ss.bagWeight == orgStkTrans.bagWeight);// && ss.date>=orgStkTrans.date
                foreach (var item in nextStkTrans)
                {
                    item.openingStock -= diff;
                }
            }
            else
            {
                var nextStkTrans = context.PaddyTransactions.Where(ss => ss.ID > orgStkTrans.ID && ss.prodId == orgStkTrans.prodId && ss.stockId == orgStkTrans.stockId && ss.bagWeight == orgStkTrans.bagWeight);// && ss.date >= orgStkTrans.date
                foreach (var item in nextStkTrans)
                {
                    item.openingStock -= orgStkTrans.rcvQty.Value;
                }
                context.PaddyTransactions.Remove(orgStkTrans);

                long maxprdstkId = context.PaddyTransactions.Select(p => p.ID).DefaultIfEmpty(0).Max();
                long laststkId = context.PaddyTransactions.Where(s => s.stockId == paddyInfo.stockId && s.prodId == paddyInfo.productId && s.bagWeight == paddyInfo.quantityPerBag).Select(l => l.ID).DefaultIfEmpty(0).Max();
                var lastTrans = context.PaddyTransactions.Where(ll => ll.ID == laststkId).FirstOrDefault();

                PaddyTransaction objStkTrans = new PaddyTransaction();
                objStkTrans.ID = maxprdstkId + 1;
                objStkTrans.date = paddyInfo.date;
                objStkTrans.rcvQty = paddyInfo.noOfBag;
                objStkTrans.releaseQty = 0;
                objStkTrans.stockId = paddyInfo.stockId;
                objStkTrans.prodId = paddyInfo.productId;
                objStkTrans.buyId = paddyInfo.ID;
                objStkTrans.bagWeight = paddyInfo.quantityPerBag;
                objStkTrans.openingStock = lastTrans == null ? paddyInfo.noOfBag : lastTrans.openingStock + paddyInfo.noOfBag;
                context.PaddyTransactions.Add(objStkTrans);


            }
            #endregion

            #region edit tblbuy
            orgPaddy.productId = paddyInfo.productId;
            orgPaddy.noOfBag = paddyInfo.noOfBag;
            orgPaddy.truckNumber = paddyInfo.truckNumber;
            //orgPaddy.tra nsportCost = paddyInfo.transportCost;
            //orgPaddy.labourCostPerBag = paddyInfo.labourCostPerBag;
            //orgPaddy.amount = paddyInfo.amount;
            orgPaddy.partyId = paddyInfo.partyId;
            orgPaddy.quantityPerBag = paddyInfo.quantityPerBag;
            orgPaddy.stockId = paddyInfo.stockId;
            orgPaddy.unit = paddyInfo.unit;
            orgPaddy.price = paddyInfo.price;
            #endregion

            #region edit dues
            tblDue orgDue = context.tblDues.Where(d => d.buyId == orgPaddy.ID).FirstOrDefault();
            double? totalPr = paddyInfo.price * paddyInfo.quantityPerBag / 40 * paddyInfo.noOfBag;
            var due = totalPr - paddyInfo.amount;
            orgDue.amount = due;

            //tblDue dueItem = context.tblDues.Where(bb => bb.date <= paddyInfo.date && bb.partyId == paddyInfo.partyId && bb.ID < orgDue.ID).OrderByDescending(oo => oo.date).ThenByDescending(ii => ii.ID).FirstOrDefault();
            //List<tblDue> dueItems = context.tblDues.Where(bb => bb.date > paddyInfo.date && bb.partyId == paddyInfo.partyId).ToList();
            //if (dueItem == null)
            //{
            //    orgDue.openingBalance = orgDue.amount;
            //}
            //else
            //{
            //    orgDue.openingBalance = dueItem.openingBalance + orgDue.amount;
            //}
            //if (dueItems != null)
            //{
            //    foreach (var item in dueItems)
            //    {
            //        item.openingBalance += orgDue.amount;
            //    }
            //}

            #endregion

            #region edit sackinfo
            BagTransaction orgItem = context.BagTransactions.Where(bb => bb.rcvId == orgPaddy.ID).FirstOrDefault();
            //remove if section when all price of previous paddy has been applied
            if (orgItem==null)
            {    
                #region BagTransactions
                long maxBagId = context.BagTransactions.Select(b => b.ID).DefaultIfEmpty(0).Max();
                if (paddyInfo.bagPrice > 0)
                {
                    BagTransaction prevItem = context.BagTransactions.Where(bb => bb.date <= paddyInfo.date && bb.partyId == paddyInfo.partyId && (bb.rcvPrice > 0 || bb.sentPrice > 0)).OrderByDescending(oo => oo.date).ThenByDescending(ii => ii.ID).FirstOrDefault();
                    List<BagTransaction> nextItems = context.BagTransactions.Where(bb => bb.date > paddyInfo.date && bb.partyId == paddyInfo.partyId && (bb.rcvPrice > 0 || bb.sentPrice > 0)).ToList();
                    //int bagCnt = paddyInfo.noOfBag > 0 ? paddyInfo.noOfBag : 1;
                    double? curPriceDue = prevItem == null ? paddyInfo.noOfBag * paddyInfo.bagPrice : prevItem.priceDues + paddyInfo.noOfBag * paddyInfo.bagPrice;

                    BagTransaction bagTrans = new BagTransaction();
                    bagTrans.ID = ++maxBagId;
                    bagTrans.partyId = paddyInfo.partyId;
                    bagTrans.rcvId = paddyInfo.ID;
                    bagTrans.comRcvBag = paddyInfo.noOfBag;
                    bagTrans.rcvPrice = paddyInfo.bagPrice;
                    bagTrans.comSentBag = 0;
                    bagTrans.sentPrice = 0;
                    bagTrans.priceDues = curPriceDue;
                    bagTrans.bagDues = 0;
                    bagTrans.date = paddyInfo.date;
                    context.BagTransactions.Add(bagTrans);

                    if (nextItems != null)
                    {
                        foreach (var item in nextItems)
                        {
                            item.priceDues += curPriceDue;
                        }
                    }
                }
                else
                {
                    BagTransaction prevItem = context.BagTransactions.Where(bb => bb.date <= paddyInfo.date && bb.partyId == paddyInfo.partyId && (bb.rcvPrice == 0 || bb.rcvPrice == null) && (bb.sentPrice == 0 || bb.sentPrice == null)).OrderByDescending(oo => oo.date).ThenByDescending(ii => ii.ID).FirstOrDefault();
                    List<BagTransaction> nextItems = context.BagTransactions.Where(bb => bb.date > paddyInfo.date && bb.partyId == paddyInfo.partyId && (bb.rcvPrice == 0 || bb.rcvPrice == null) && (bb.sentPrice == 0 || bb.sentPrice == null)).ToList();
                    int? curBagDue = prevItem == null ? paddyInfo.noOfBag : prevItem.bagDues + paddyInfo.noOfBag;

                    BagTransaction bagTrans = new BagTransaction();
                    bagTrans.ID = ++maxBagId;
                    bagTrans.partyId = paddyInfo.partyId;
                    bagTrans.rcvId = paddyInfo.ID;
                    bagTrans.comRcvBag = paddyInfo.noOfBag;
                    bagTrans.rcvPrice = 0;
                    bagTrans.comSentBag = 0;
                    bagTrans.sentPrice = 0;
                    bagTrans.priceDues = 0;
                    bagTrans.bagDues = curBagDue;
                    bagTrans.date = paddyInfo.date;
                    context.BagTransactions.Add(bagTrans);

                    if (nextItems != null)
                    {
                        foreach (var item in nextItems)
                        {
                            item.bagDues += curBagDue;
                        }
                    }
                }

                #endregion
            }
            else
            {                    
                orgItem.rcvPrice = orgItem.rcvPrice ?? 0;
                paddyInfo.bagPrice = paddyInfo.bagPrice ?? 0;
                if (orgItem.rcvPrice > 0 && paddyInfo.bagPrice > 0)
                {
                    List<BagTransaction> nextItems = context.BagTransactions.Where(bb => bb.date >= paddyInfo.date && bb.partyId == paddyInfo.partyId && (bb.rcvPrice > 0 || bb.sentPrice > 0)).ToList(); // && bb.ID>orgItem.ID
                    double? prevPriceDue = orgItem.priceDues - orgItem.rcvPrice * orgItem.comRcvBag;
                    double? curPriceDue = prevPriceDue + paddyInfo.noOfBag * paddyInfo.bagPrice;
                    if (nextItems != null)
                    {
                        foreach (var item in nextItems)
                        {
                            item.priceDues = item.priceDues - orgItem.priceDues + curPriceDue;
                        }
                    }
                    orgItem.partyId = paddyInfo.partyId;
                    orgItem.comRcvBag = paddyInfo.noOfBag;
                    orgItem.rcvPrice = paddyInfo.bagPrice;
                    orgItem.priceDues = curPriceDue;
                    orgItem.date = paddyInfo.date;
                }
                else if ((orgItem.rcvPrice == 0 || orgItem.rcvPrice == null) && (paddyInfo.bagPrice == 0 || paddyInfo.bagPrice == null))
                {
                    List<BagTransaction> nextItems = context.BagTransactions.Where(bb => bb.date >= paddyInfo.date && bb.partyId == paddyInfo.partyId && (bb.rcvPrice == 0 || bb.rcvPrice == null) && (bb.sentPrice == 0 || bb.sentPrice == null)).ToList(); //&& bb.ID > orgItem.ID
                    int? prevBagDue = orgItem.bagDues - orgItem.comRcvBag;
                    int? curBagDue = prevBagDue + paddyInfo.noOfBag;
                    if (nextItems != null)
                    {
                        foreach (var item in nextItems)
                        {
                            item.bagDues = item.bagDues - orgItem.bagDues + curBagDue;
                        }
                    }
                    orgItem.partyId = paddyInfo.partyId;
                    orgItem.comRcvBag = paddyInfo.noOfBag;
                    orgItem.bagDues = curBagDue;
                    orgItem.date = paddyInfo.date;
                }
                else if (orgItem.rcvPrice > 0 && (paddyInfo.bagPrice == 0 || paddyInfo.bagPrice == null))
                {

                    List<BagTransaction> nextItemsWithPric = context.BagTransactions.Where(bb => bb.date >= paddyInfo.date && bb.partyId == paddyInfo.partyId && (bb.rcvPrice > 0 || bb.sentPrice > 0)).ToList();//&& bb.ID > orgItem.ID
                    List<BagTransaction> nextItemsWithoutPric = context.BagTransactions.Where(bb => bb.date >= paddyInfo.date && bb.partyId == paddyInfo.partyId && (bb.rcvPrice == 0 || bb.rcvPrice == null) && (bb.sentPrice == 0 || bb.sentPrice == null)).ToList();//&& bb.ID > orgItem.ID
                    int? prevBagDue = orgItem.bagDues - orgItem.comRcvBag;
                    int? curBagDue = prevBagDue + paddyInfo.noOfBag;
                    if (nextItemsWithPric != null)
                    {
                        foreach (var item in nextItemsWithPric)
                        {
                            item.priceDues = item.priceDues - orgItem.priceDues;
                        }
                    }
                    if (nextItemsWithoutPric != null)
                    {
                        foreach (var item in nextItemsWithoutPric)
                        {
                            item.bagDues = item.bagDues + paddyInfo.noOfBag;
                        }
                    }

                    orgItem.partyId = paddyInfo.partyId;
                    orgItem.comRcvBag = paddyInfo.noOfBag;
                    orgItem.rcvPrice = 0;
                    orgItem.priceDues = 0;
                    orgItem.bagDues = curBagDue;
                    orgItem.date = paddyInfo.date;
                }
                else if ((orgItem.rcvPrice == 0 || orgItem.rcvPrice == null) && paddyInfo.bagPrice > 0)
                {
                    List<BagTransaction> nextItemsWithPric = context.BagTransactions.Where(bb => bb.date >= paddyInfo.date && bb.partyId == paddyInfo.partyId && (bb.rcvPrice > 0 || bb.sentPrice > 0)).ToList();// && bb.ID > orgItem.ID
                    List<BagTransaction> nextItemsWithoutPric = context.BagTransactions.Where(bb => bb.date >= paddyInfo.date && bb.partyId == paddyInfo.partyId && (bb.rcvPrice == 0 || bb.rcvPrice == null) && (bb.sentPrice == 0 || bb.sentPrice == null)).ToList();// && bb.ID > orgItem.ID
                    double? prevPriceDue = orgItem.priceDues - orgItem.rcvPrice * orgItem.comRcvBag;
                    double? curPriceDue = prevPriceDue + paddyInfo.noOfBag * paddyInfo.bagPrice;
                    if (nextItemsWithPric != null)
                    {
                        foreach (var item in nextItemsWithPric)
                        {
                            item.priceDues = item.priceDues + paddyInfo.noOfBag * paddyInfo.bagPrice;
                        }
                    }
                    if (nextItemsWithoutPric != null)
                    {
                        foreach (var item in nextItemsWithoutPric)
                        {
                            item.bagDues = item.bagDues - orgItem.comRcvBag;
                        }
                    }

                    orgItem.partyId = paddyInfo.partyId;
                    orgItem.comRcvBag = paddyInfo.noOfBag;
                    orgItem.rcvPrice = paddyInfo.bagPrice;
                    orgItem.priceDues = curPriceDue;
                    orgItem.bagDues = 0;
                    orgItem.date = paddyInfo.date;
                }
            }
            #endregion
                                             
            return context.SaveChanges() > 0 ? paddyInfo.ID : 0;
        }

        public bool DeletePaddy(int pk)
        {
            var orgPaddy = context.tblBuys.Where(ss => ss.ID == pk).FirstOrDefault();

            #region edit stock transaction
            var orgStkTrans = context.PaddyTransactions.Where(ss => ss.buyId == orgPaddy.ID).FirstOrDefault();
            var nextStkTrans = context.PaddyTransactions.Where(ss => ss.ID > orgStkTrans.ID && ss.prodId == orgStkTrans.prodId && ss.stockId == orgStkTrans.stockId && ss.bagWeight == orgStkTrans.bagWeight);
            foreach (var item in nextStkTrans)
            {
                item.openingStock -= orgStkTrans.rcvQty.Value;
            }
            context.PaddyTransactions.Remove(orgStkTrans);
            #endregion

            #region edit paddy stock
            STK_Balance padStk = context.STK_Balance.Where(ss => ss.stockId == orgPaddy.stockId && ss.productId == orgPaddy.productId && ss.sackWeight == orgPaddy.quantityPerBag).FirstOrDefault();//&& ss.sackWeight == riceInfo.quantity
            padStk.sackQuantity -= orgPaddy.noOfBag;
            #endregion

            #region delete costing source
            var orgCstSrc = context.tblCostingSources.Where(ii => ii.buyId == orgPaddy.ID).FirstOrDefault();
            if (orgCstSrc != null)
            {
                context.tblCostingSources.Remove(orgCstSrc);
            }
            #endregion

            #region delete sackInfo
            var orgSack = context.BagTransactions.Where(k => k.rcvId == pk).FirstOrDefault();
            if (orgSack != null)
            {
                if (orgSack.rcvPrice > 0)
                {
                    List<BagTransaction> nextItems = context.BagTransactions.Where(ss => ss.date >= orgPaddy.date && ss.partyId == orgPaddy.partyId && ss.ID > orgSack.ID && ss.rcvPrice > 0).ToList();
                    if (nextItems != null)
                    {
                        foreach (var item in nextItems)
                        {
                            item.priceDues -= orgSack.rcvPrice * orgSack.comRcvBag;
                        }
                    }
                }
                else
                {
                    List<BagTransaction> nextItems = context.BagTransactions.Where(ss => ss.date >= orgPaddy.date && ss.partyId == orgPaddy.partyId && ss.ID > orgSack.ID && (ss.rcvPrice == 0 || ss.rcvPrice == null)).ToList();
                    if (nextItems != null)
                    {
                        foreach (var item in nextItems)
                        {
                            item.bagDues -= orgSack.comRcvBag;
                        }
                    }
                }
                context.BagTransactions.Remove(orgSack);
            }
            #endregion

            #region delete dues
            tblDue orgDue = context.tblDues.Where(d => d.buyId == orgPaddy.ID).FirstOrDefault();
            //List<tblDue> dueItems = context.tblDues.Where(bb => bb.date >= orgDue.date && bb.partyId == orgDue.partyId && bb.ID > orgDue.ID).ToList();

            //if (dueItems != null)
            //{
            //    foreach (var item in dueItems)
            //    {
            //        item.openingBalance -= orgDue.amount;
            //    }
            //}
            context.tblDues.Remove(orgDue);
            #endregion

            #region delete tblbuy
            context.tblBuys.Remove(orgPaddy);
            #endregion

            return context.SaveChanges() > 0;
        }

        public List<object> GetPaddyInfoRpt(tblBuy paddyRpt)
        {
            int parentId = 1;// 1 for paddy
            var paddyInfoLst = context.sp_GetPaddyInfo(paddyRpt.partyId, parentId, paddyRpt.fromDate, paddyRpt.toDate);
            return paddyInfoLst.ToList<object>();
        }
        public double GetDues(int partyId,DateTime from,DateTime to)
        {
            // calculating from openingbalance
            var lastStore = context.OpeningBalances.Where(pp=>pp.partyId==partyId).OrderByDescending(pp=>pp.date).FirstOrDefault();
            List<tblDue> dues = new List<tblDue>();
            double amtdue = 0;
            if (lastStore!=null)
            {
                amtdue = lastStore.balance;
                dues = context.tblDues.Where(pp => pp.partyId == partyId && pp.date > lastStore.date && pp.date <= to).ToList();
            }
            else
            {
                dues = context.tblDues.Where(pp => pp.partyId == partyId && pp.date <= to).ToList();
            }
            foreach (var item in dues)
            {
                amtdue += item.amount ?? 0;
            }
            double bagpr = 0;
            var bagtr = context.BagTransactions.Where(bb => bb.partyId == partyId && (bb.sentPrice > 0 || bb.rcvPrice > 0) && bb.date <= to).ToList();
            foreach (var item in bagtr)
            {
                double sentpr = item == null ? 0 : item.sentPrice??0;
                double rcvpr = item == null ? 0 : item.rcvPrice??0;
                bagpr += rcvpr * item.comRcvBag??0 - sentpr * item.comSentBag??0;
            }
            return (amtdue + bagpr);

        }
        public double GetDuesOnDate(int partyId, DateTime date)
        {
            // calculating from openingbalance
            var lastStore = context.OpeningBalances.Where(pp => pp.partyId == partyId&&pp.date<date).OrderByDescending(pp => pp.date).FirstOrDefault();
            List<tblDue> dues = new List<tblDue>();
            double amtdue = 0;
            if (lastStore != null)
            {                   
                amtdue = lastStore.balance;
                dues = context.tblDues.Where(pp => pp.partyId == partyId && pp.date > lastStore.date && pp.date < date).ToList();
            }
            else
            {
                dues = context.tblDues.Where(pp => pp.partyId == partyId && pp.date <date).ToList();
            }
            //var dues = context.tblDues.Where(pp => pp.partyId == partyId && pp.date <date).ToList();
            //double amtdue = 0;
            double bagpr = 0;
            foreach (var item in dues)
            {
                amtdue += item.amount ?? 0;
            }
            var bagtr = context.BagTransactions.Where(bb => bb.partyId == partyId && (bb.sentPrice > 0 || bb.rcvPrice > 0) && bb.date <date).ToList();
            foreach (var item in bagtr)
            {
                double sentpr = item == null ? 0 : item.sentPrice ?? 0;
                double rcvpr = item == null ? 0 : item.rcvPrice ?? 0;
                bagpr += rcvpr * item.comRcvBag ?? 0 - sentpr * item.comSentBag ?? 0;
            }
            return (amtdue + bagpr);

        }

        public ReportViewModel GetReportViewModel(List<object> objLst, int partyId, string fromDate, string toDate)
        {
            var reportViewModel = new ReportViewModel()
            {
                LeftMainTitle = "Union Group",
                LeftSubTitle = "IT",
                Name = "Sales Report",
                //ReportDate = DateTime.Now,
                ReportLogo = "~/Content/logo.jpg",
                //ReportTitle = "Retailerwise Sales Report",
                ReportLanguage = "en-US",
                //UserNamPrinting = UserPrinting,
                Format = ReportViewModel.ReportFormat.PDF,
                ViewAsAttachment = false
            };
            tblParty party = context.tblParties.Where(pp => pp.ID == partyId).FirstOrDefault();
            string pname = party.name ?? "N/A";
            string area = party.area ?? "N/A";
            string contact = party.contactNo != null ? party.contactNo.ToString() : "N/A";
            double dues = GetDues(partyId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
            double prevDue = GetDuesOnDate(partyId, Convert.ToDateTime(fromDate));
            BagTransaction bagDue = GetBagDues(partyId);
            //adding the dataset information to the report view model object
            reportViewModel.ReportDataSets.Add(new ReportViewModel.ReportDataSet() { DataSetData = objLst, DatasetName = "DataSet_GetPaddyInfo" });
            reportViewModel.ReportParams.Add(new ReportParameter("dues", dues.ToString()));
            reportViewModel.ReportParams.Add(new ReportParameter("prevDue", prevDue.ToString()));
            reportViewModel.ReportParams.Add(new ReportParameter("comRcvBag", bagDue.comRcvBag.ToString()));
            reportViewModel.ReportParams.Add(new ReportParameter("comSentBag", bagDue.comSentBag.ToString()));
            reportViewModel.ReportParams.Add(new ReportParameter("bagDues", bagDue.bagDues.ToString()));
            reportViewModel.ReportParams.Add(new ReportParameter("partyName", pname));
            reportViewModel.ReportParams.Add(new ReportParameter("address", area));
            reportViewModel.ReportParams.Add(new ReportParameter("phone", contact));
            reportViewModel.ReportParams.Add(new ReportParameter("startDate", fromDate));
            reportViewModel.ReportParams.Add(new ReportParameter("endDate", toDate));
            return reportViewModel;
        }

        private BagTransaction GetBagDues(int partyId)
        {
            var comRcvBag = context.BagTransactions.Where(bb => bb.partyId == partyId).Sum(ss => ss.comRcvBag);
            var comSentBag = context.BagTransactions.Where(bb => bb.partyId == partyId).Sum(ss => ss.comSentBag);
            var bagDues = comRcvBag ?? 0 - comSentBag ?? 0;
            BagTransaction bt = new BagTransaction();
            bt.comRcvBag = comRcvBag ?? 0;
            bt.comSentBag = comSentBag ?? 0;
            bt.bagDues = bagDues;
            return bt;
        }
    }
}