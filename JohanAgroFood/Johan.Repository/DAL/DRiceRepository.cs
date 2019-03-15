using System.Runtime.InteropServices;
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
    public class DRiceRepository:Disposable,IRiceRepository
    {
        private JohanAgroFoodDBEntities context = null;
        
        public DRiceRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {

            this.context = context;    
        }
        
        public List<tblSell> GetRiceInfo()
        {
            var data = context.sp_getProductSellInfo(0,2);// 2 is parent for ricetype
            List<tblSell> sells = new List<tblSell>();
            foreach (var item in data.Take(100))
            {
                tblSell sellobj = new tblSell();
                sellobj.ID = item.ID;
                sellobj.date = item.date;
                sellobj.noOfBag= item.noOfBag;
                sellobj.partyId= item.partyId;
                sellobj.partyName= item.partyName;
                sellobj.productId = item.productId;
                sellobj.productName = item.productName;
                sellobj.quantity= item.quantity;
                sellobj.stockId = item.stockId;
                sellobj.stockName = item.stockName;
                sellobj.unit=item.unit;
                sellobj.productName = item.productName;
                sellobj.unitPrice = item.unitPrice;
                sellobj.paidAmount = Convert.ToDouble(item.amount);
                sellobj.transportCost = item.transportCost;
                sellobj.truckNumber = item.truckNumber;
                sellobj.description = item.description;
                sellobj.incSrcId = item.incSrcId;
                sells.Add(sellobj);
            }
            //List<tblLoanar> results = new List<tblLoanar>();
            //foreach (var item in loaners)
            //{
            //    results.Add(new tblParty() { ID=item.ID, name=item.name, contactNo=item.contactNo, area=item.area,
            //     district=item.district, zoneId=item.zoneId, productId=item.productId, isCashParty=item.isCashParty});
            //}
            return sells;
        }
        
        public List<tblProduct> LoadRice()
        {
            var results = context.tblProducts.Where(pp => pp.parentId == 2).ToList();
            return results;
        }
        
        public tblSell SaveRice(tblSell riceInfo)
        {
            try
            {
                long maxId = context.tblSells.Select(p => p.ID).DefaultIfEmpty(0).Max();
                riceInfo.ID = ++maxId;
               
                if (riceInfo.paidAmount > 0)
                {
                    #region income source
                    long incId = context.tblIncomeSources.Select(i => i.ID).DefaultIfEmpty(0).Max();
                    tblIncomeSource incomeObj = new tblIncomeSource();
                    incomeObj.ID = ++incId;
                    incomeObj.sourceName = "চাল"; // shoul be come from commonelement
                    incomeObj.srcDescId = 20; // should be come from commonelemnt
                    incomeObj.description = "চাল বিক্রয় বাবদ আয়";
                    incomeObj.amount = riceInfo.paidAmount;
                    incomeObj.date = riceInfo.date;
                   
                    incomeObj.partyId = riceInfo.partyId;
                    context.tblIncomeSources.Add(incomeObj);
                    #endregion    
                    riceInfo.incSrcId = incomeObj.ID;
                }         

                #region save payable

                double? totalPr = 0;
                if (riceInfo.transportCostInclude)
                {
                    totalPr = riceInfo.noOfBag * riceInfo.unitPrice * riceInfo.quantity + riceInfo.transportCost;
                }
                else
                {
                    totalPr = riceInfo.noOfBag * riceInfo.unitPrice * riceInfo.quantity;
                    if (riceInfo.transportCost > 0)
                    {
                        #region costing source
                        tblCostingSource objTblCostingSource = new tblCostingSource();
                        long maxCost = context.tblCostingSources.Select(i => i.ID).DefaultIfEmpty(0).Max();
                        objTblCostingSource.ID = ++maxCost;
                        objTblCostingSource.partyId = riceInfo.partyId;
                        objTblCostingSource.amount = Convert.ToDouble(riceInfo.transportCost);
                        objTblCostingSource.sourceName = "চাল";
                        objTblCostingSource.srcDescription = "চাল বিক্রয়ের পরিবহন খরচ";
                        objTblCostingSource.srcDescId = 20;
                        objTblCostingSource.date = riceInfo.date;
                        objTblCostingSource.sellId = riceInfo.ID;

                        context.tblCostingSources.Add(objTblCostingSource);
                        #endregion
                    }
                }
                if (totalPr > 0)
                {
                    long maxpayId = context.tblPayables.Select(i => i.ID).DefaultIfEmpty(0).Max();
                    tblPayable objTblPayable = new tblPayable();
                    objTblPayable.ID = ++maxpayId;
                    objTblPayable.partyId = riceInfo.partyId;
                    objTblPayable.date = riceInfo.date;
                    objTblPayable.sellId = riceInfo.ID;
                    var lastPayable = context.tblPayables.Where(p => p.partyId == riceInfo.partyId && p.isActive == 1).FirstOrDefault();


                    var loan = totalPr - riceInfo.paidAmount;
                    objTblPayable.amount = loan;
                    if (lastPayable != null)
                    {
                        lastPayable.isActive = 0;
                        objTblPayable.openingBalance = objTblPayable.amount + lastPayable.openingBalance;
                    }
                    else
                    {
                        objTblPayable.openingBalance = objTblPayable.amount;
                    }

                    objTblPayable.isActive = 1;

                    context.tblPayables.Add(objTblPayable);
                }
                #endregion
                context.tblSells.Add(riceInfo);
                   
                #region substract rice from stock
                STK_Balance riceStk = context.STK_Balance.Where(ss => ss.stockId == riceInfo.stockId && ss.productId == riceInfo.productId).FirstOrDefault();//&& ss.sackWeight==riceInfo.quantity
                STK_Balance stkBal=new STK_Balance();
                if (riceStk==null)
                {
                    var maxStkBal = context.STK_Balance.Select(p => p.ID).DefaultIfEmpty(0).Max();
                    stkBal.ID = ++maxStkBal;
                    stkBal.productId = riceInfo.productId;
                    stkBal.stockId = riceInfo.stockId;
                    stkBal.sackQuantity = -riceInfo.noOfBag;
                    context.STK_Balance.Add(stkBal);
                }
                else
                {
                    riceStk.sackQuantity -= riceInfo.noOfBag;
                }
                
                #endregion

                #region stock transaction
                long maxprdstkId = context.STK_Transaction.Select(p => p.ID).DefaultIfEmpty(0).Max();
                //long laststkId = context.STK_Transaction.Where(s => s.stockId == particleInfo.stockId && s.prodId == particleInfo.productId).Select(l => l.ID).DefaultIfEmpty(0).Max();
                STK_Transaction objStkTrans = new STK_Transaction();
                objStkTrans.ID = maxprdstkId + 1;
                objStkTrans.date = riceInfo.date;
                objStkTrans.rcvQty = 0;
                objStkTrans.sellQty = riceInfo.noOfBag;
                objStkTrans.stockId = riceInfo.stockId.Value;
                objStkTrans.prodId = riceInfo.productId;
                objStkTrans.operation = 1;
                objStkTrans.sellId = riceInfo.ID;
                //objStkTrans.openingStock = riceStk.sackQuantity.Value;
                if(riceStk==null)
                {
                    objStkTrans.openingStock = -riceInfo.noOfBag;
                }
                else
                {
                    objStkTrans.openingStock = riceStk.sackQuantity.Value;
                }



                //var lastTrans = context.STK_Transaction.Where(ll => ll.ID == laststkId).FirstOrDefault();
                //objStkTrans.openingStock = lastTrans == null ? 0 - particleInfo.noOfBag : lastTrans.openingStock - particleInfo.noOfBag;
                context.STK_Transaction.Add(objStkTrans);
                #endregion
                tblSell newSell = new tblSell();
                newSell.ID = riceInfo.ID;
                newSell.incSrcId = riceInfo.incSrcId;

                return context.SaveChanges() > 0 ? newSell : null;
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }

        public tblSell EditRiceSell(tblSell riceInfo)
        {
            try
            {
                // into payable tabel column amount is 'how many amount is added with opening balance, not paidamount'
                var orgRiceSell = context.tblSells.Where(ss => ss.ID == riceInfo.ID).FirstOrDefault();
                #region edit rice stock
                STK_Balance orgStk = context.STK_Balance.Where(ss => ss.stockId == orgRiceSell.stockId && ss.productId == orgRiceSell.productId).FirstOrDefault();// && ss.sackWeight == particleInfo.quantity
                orgStk.sackQuantity += orgRiceSell.noOfBag;

                STK_Balance newStk = context.STK_Balance.Where(ss => ss.stockId == riceInfo.stockId && ss.productId == riceInfo.productId).FirstOrDefault();// && ss.sackWeight == particleInfo.quantity
                if (newStk == null)
                {
                    STK_Balance stkBal = new STK_Balance();
                    var maxStkBal = context.STK_Balance.Select(p => p.ID).DefaultIfEmpty(0).Max();
                    stkBal.ID = ++maxStkBal;
                    stkBal.productId = riceInfo.productId;
                    stkBal.stockId = riceInfo.stockId;
                    stkBal.sackQuantity = -riceInfo.noOfBag;
                    context.STK_Balance.Add(stkBal);
                }
                else
                {
                    newStk.sackQuantity -= riceInfo.noOfBag;
                }
                #endregion

                double? totalPr = 0;
                if (riceInfo.transportCostInclude)
                {
                    totalPr = riceInfo.noOfBag * riceInfo.unitPrice * riceInfo.quantity + riceInfo.transportCost;
                }
                else
                {
                    totalPr = riceInfo.noOfBag * riceInfo.unitPrice * riceInfo.quantity;

                }

                #region costing source
                tblCostingSource objCostingSource = context.tblCostingSources.Where(cc => cc.sellId == riceInfo.ID).FirstOrDefault();
                if (riceInfo.transportCostInclude && objCostingSource != null)
                {
                    context.tblCostingSources.Remove(objCostingSource);
                }
                else if (!riceInfo.transportCostInclude && objCostingSource == null)
                {
                    tblCostingSource newCost = new tblCostingSource();
                    long maxCost = context.tblCostingSources.Select(i => i.ID).DefaultIfEmpty(0).Max();

                    newCost.ID = ++maxCost;
                    newCost.amount = Convert.ToDouble(riceInfo.transportCost);
                    newCost.sourceName = "চাল";
                    newCost.srcDescription = "চাল বিক্রয়ের পরিবহন খরচ";
                    newCost.srcDescId = 20;
                    newCost.date = riceInfo.date;
                    newCost.sellId = riceInfo.ID;
                    context.tblCostingSources.Add(newCost);
                }
                else if (!riceInfo.transportCostInclude && objCostingSource != null)
                {
                    objCostingSource.amount = Convert.ToDouble(riceInfo.transportCost);
                    objCostingSource.date = riceInfo.date;
                }
                #endregion

                #region edit or insert income source
                var orgIncomeSrc = context.tblIncomeSources.Where(ii => ii.ID == riceInfo.incSrcId).FirstOrDefault();

                if (orgIncomeSrc != null)
                {
                    orgIncomeSrc.amount = riceInfo.paidAmount;
                    orgIncomeSrc.date = riceInfo.date;
                    orgIncomeSrc.partyId = riceInfo.partyId;


                }
                else if (orgIncomeSrc == null && riceInfo.paidAmount > 0)
                {
                    long incId = context.tblIncomeSources.Select(i => i.ID).DefaultIfEmpty(0).Max();
                    tblIncomeSource incomeObj = new tblIncomeSource();
                    incomeObj.ID = ++incId;
                    incomeObj.sourceName = "চাল"; // shoul be come from commonelement
                    incomeObj.srcDescId = 27; // should be come from commonelemnt
                    incomeObj.description = "চাল বিক্রয় বাবদ আয়";
                    incomeObj.amount = riceInfo.paidAmount;
                    incomeObj.date = riceInfo.date;
                    
                    incomeObj.partyId = riceInfo.partyId;
                    context.tblIncomeSources.Add(incomeObj);
                    orgRiceSell.incSrcId = incomeObj.ID;

                }
                #endregion

                #region edit payable
                var orgPayable = context.tblPayables.Where(ss => ss.sellId == riceInfo.ID).FirstOrDefault();
                //long lastpayId = context.tblPayables.Where(s => s.partyId == particleInfo.partyId).Select(l => l.ID).DefaultIfEmpty(0).Max();
                //var lastPayable = context.tblPayables.Where(pp => pp.ID == lastpayId).FirstOrDefault();
                if (orgPayable != null)
                {
                    double? difference = totalPr - riceInfo.paidAmount - orgPayable.amount;
                    orgPayable.openingBalance = orgPayable.openingBalance + difference;
                    orgPayable.amount = totalPr - riceInfo.paidAmount;
                    var nextPayables = context.tblPayables.Where(pp => pp.partyId == riceInfo.partyId && pp.ID > orgPayable.ID);
                    foreach (var item in nextPayables)
                    {
                        item.openingBalance = item.openingBalance + difference;
                    }
                }
                else if (orgPayable == null && totalPr > 0)
                {
                    long maxpayId = context.tblPayables.Select(i => i.ID).DefaultIfEmpty(0).Max();
                    tblPayable objTblPayable = new tblPayable();
                    objTblPayable.ID = ++maxpayId;
                    objTblPayable.partyId = riceInfo.partyId;
                    objTblPayable.date = riceInfo.date;
                    objTblPayable.sellId = riceInfo.ID;
                    var lastPayable = context.tblPayables.Where(p => p.partyId == riceInfo.partyId && p.isActive == 1).FirstOrDefault();


                    var loan = totalPr - riceInfo.paidAmount;
                    objTblPayable.amount = loan;
                    if (lastPayable != null)
                    {
                        lastPayable.isActive = 0;
                        objTblPayable.openingBalance = objTblPayable.amount + lastPayable.openingBalance;
                    }
                    else
                    {
                        objTblPayable.openingBalance = objTblPayable.amount;
                    }

                    objTblPayable.isActive = 1;

                    context.tblPayables.Add(objTblPayable);
                }
                #endregion

                #region edit stock transaction

                var orgStkTrans = context.STK_Transaction.Where(ss => ss.sellId == orgRiceSell.ID).FirstOrDefault();

                double diff = orgStkTrans.sellQty.Value - riceInfo.noOfBag;
                orgStkTrans.openingStock = orgStkTrans.openingStock + orgStkTrans.sellQty.Value;

                orgStkTrans.date = riceInfo.date;
                orgStkTrans.rcvQty = 0;
                orgStkTrans.sellQty = riceInfo.noOfBag;
                orgStkTrans.stockId = riceInfo.stockId.Value;
                orgStkTrans.prodId = riceInfo.productId;
                orgStkTrans.operation = 2;
                orgStkTrans.openingStock = orgStkTrans.openingStock - riceInfo.noOfBag;

                var nextStkTrans = context.STK_Transaction.Where(ss => ss.ID > orgStkTrans.ID && ss.prodId == orgStkTrans.prodId && ss.stockId == orgStkTrans.stockId);
                foreach (var item in nextStkTrans)
                {
                    item.openingStock += diff;
                }
                #endregion

                #region edit tblsell
                orgRiceSell.productId = riceInfo.productId;
                orgRiceSell.productName = riceInfo.productName;
                orgRiceSell.date = riceInfo.date;
                orgRiceSell.noOfBag = riceInfo.noOfBag;
                orgRiceSell.paidAmount = riceInfo.paidAmount;
                orgRiceSell.partyId = riceInfo.partyId;
                orgRiceSell.partyName = riceInfo.partyName;
                orgRiceSell.quantity = riceInfo.quantity;
                orgRiceSell.stockId = riceInfo.stockId;
                orgRiceSell.stockName = riceInfo.stockName;
                orgRiceSell.unit = riceInfo.unit;
                orgRiceSell.unitPrice = riceInfo.unitPrice;
                orgRiceSell.transportCost = riceInfo.transportCost;
                #endregion

                tblSell newSell = new tblSell();
                newSell.ID = riceInfo.ID;
                newSell.incSrcId = riceInfo.incSrcId;
                return context.SaveChanges() > 0 ? newSell : null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteRiceSell(int pk)
        {
            var orgRiceSell = context.tblSells.Where(ss => ss.ID == pk).FirstOrDefault();

            #region edit rice stock
            STK_Balance riceStk = context.STK_Balance.Where(ss => ss.stockId == orgRiceSell.stockId && ss.productId == orgRiceSell.productId).FirstOrDefault();//&& ss.sackWeight == particleInfo.quantity
            riceStk.sackQuantity += orgRiceSell.noOfBag;
            #endregion

            #region edit stock transaction
            var delStkTrans = context.STK_Transaction.Where(ss => ss.sellId == orgRiceSell.ID).FirstOrDefault();

            var nextStkTrans = context.STK_Transaction.Where(ss => ss.ID > delStkTrans.ID && ss.prodId == delStkTrans.prodId && ss.stockId == delStkTrans.stockId);
            foreach (var item in nextStkTrans)
            {
                item.openingStock += orgRiceSell.noOfBag;
            }
            context.STK_Transaction.Remove(delStkTrans);
            #endregion

            #region delete tblPayable
            var orgPayable = context.tblPayables.Where(ss => ss.sellId == pk).FirstOrDefault();
            if (orgPayable != null)
            {
                double? difference = orgPayable.amount;
                var nextPayables = context.tblPayables.Where(pp => pp.partyId == orgPayable.partyId && pp.ID > orgPayable.ID);
                if (nextPayables.Count() <= 0)
                {
                    var lastPayable = context.tblPayables.Where(pp => pp.partyId == orgPayable.partyId && pp.ID < orgPayable.ID).OrderByDescending(pp => pp.ID).FirstOrDefault();
                    if (lastPayable != null)
                        lastPayable.isActive = 1;
                }
                foreach (var item in nextPayables)
                {
                    item.openingBalance = item.openingBalance - difference;
                }
                context.tblPayables.Remove(orgPayable);
            }
            #endregion

            #region delete costing source
            List<tblCostingSource> objCostingSource = context.tblCostingSources.Where(cc => cc.sellId == pk).ToList();

            if (objCostingSource != null)
            {
                foreach (var item in objCostingSource)
                {
                    context.tblCostingSources.Remove(item);
                }
            }
            #endregion


            // delete income source
            var orgIncomeSrc = context.tblIncomeSources.Where(ii => ii.ID == orgRiceSell.incSrcId).FirstOrDefault();
            if (orgIncomeSrc != null)
            {
                context.tblIncomeSources.Remove(orgIncomeSrc);
            }
            //delete tblsell
            context.tblSells.Remove(orgRiceSell);

            return context.SaveChanges() > 0;
        }

        public List<object> GetRiceInfoRpt(tblSell riceRpt)
        {
            int parentId = 2;// 2 for rice
            var riceInfoLst = context.sp_GetProductInfo(riceRpt.partyId, parentId, riceRpt.fromDate, riceRpt.toDate).OrderBy(ss => ss.date);
            return riceInfoLst.ToList<object>();
        }

        public double GetDues(int partyId)
        {
            var due = context.tblPayables.Where(pp => pp.partyId == partyId && pp.isActive == 1).FirstOrDefault();
            double? dues =due==null?0:due.openingBalance;
            return dues ?? 0;
        }

        public double GetDuesOnDate(int partyId, DateTime date)
        {
            var due = context.tblPayables.Where(pp => pp.partyId == partyId && pp.date < date).OrderByDescending(tt => tt.ID).FirstOrDefault();
            double? dues = due == null ? 0 : due.openingBalance;
            return dues ?? 0;
 
        }

        public ReportViewModel GetRepoertViewModel(List<object> objLst, int partyId, string fromDate, string toDate)
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
            string contact = party.contactNo!=null?party.contactNo.ToString(): "N/A";
            double dues = GetDues(partyId);
            double prevDue = GetDuesOnDate(partyId,Convert.ToDateTime(fromDate));
            //adding the dataset information to the report view model object
            reportViewModel.ReportDataSets.Add(new ReportViewModel.ReportDataSet() { DataSetData = objLst, DatasetName = "datset_GetProductInfo" });
            reportViewModel.ReportParams.Add(new ReportParameter("dues", dues.ToString()));
            reportViewModel.ReportParams.Add(new ReportParameter("prevDue", prevDue.ToString()));
            reportViewModel.ReportParams.Add(new ReportParameter("partyName", pname));
            reportViewModel.ReportParams.Add(new ReportParameter("address", area));
            reportViewModel.ReportParams.Add(new ReportParameter("phone", contact));
            reportViewModel.ReportParams.Add(new ReportParameter("startDate", fromDate));
            reportViewModel.ReportParams.Add(new ReportParameter("endDate", toDate));
            return reportViewModel;
        }
    }
}