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
    public class DHuskSellRepository:Disposable,IHuskSellRepository
    {
        private JohanAgroFoodDBEntities context = null;

        public DHuskSellRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {

            this.context = context;    
        }
        public List<tblSell> GetHuskInfo()
        {
            var data = context.sp_getProductSellInfo(0,4);// 4 is id of husk
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
        public List<tblProduct> LoadHusk(int StockId)
        {
            var results = from rc in context.STK_Balance.Where(rr => rr.stockId == StockId)
                          join pr in context.tblProducts.Where(pp => pp.parentId == 4) on rc.productId equals pr.ID // 4 for husk
                          select pr;
            return results.Distinct().ToList();
        }
        public tblSell SaveHusk(tblSell huskInfo)
        {
            try
            {
                long maxId = context.tblSells.Select(p => p.ID).DefaultIfEmpty(0).Max();
                huskInfo.ID = ++maxId;

                if (huskInfo.paidAmount > 0)
                {
                    #region income source
                    long incId = context.tblIncomeSources.Select(i => i.ID).DefaultIfEmpty(0).Max();
                    tblIncomeSource incomeObj = new tblIncomeSource();
                    incomeObj.ID = ++incId;
                    incomeObj.partyId = huskInfo.partyId;
                    incomeObj.sourceName = "তুষ"; // shoul be come from commonelement
                    incomeObj.srcDescId = 21; // should be come from commonelemnt
                    incomeObj.description = "তুষ বিক্রয় বাবদ আয়";
                    incomeObj.amount = huskInfo.paidAmount;
                    incomeObj.date = huskInfo.date;
                    
					context.tblIncomeSources.Add(incomeObj);
                    #endregion
					huskInfo.incSrcId = incomeObj.ID;
				}
				
                #region save payable
                    
                    double? totalPr = 0;
                    if (huskInfo.transportCostInclude)
                    {
                        totalPr = huskInfo.noOfBag * huskInfo.unitPrice * huskInfo.quantity + huskInfo.transportCost;
                    }
                    else
                    {
                        totalPr = huskInfo.noOfBag * huskInfo.unitPrice * huskInfo.quantity;
						if (huskInfo.transportCost > 0)
						{
							#region costing source
							tblCostingSource objTblCostingSource = new tblCostingSource();
                            long maxCost = context.tblCostingSources.Select(i => i.ID).DefaultIfEmpty(0).Max();
							objTblCostingSource.ID = ++maxCost;
						    objTblCostingSource.partyId = huskInfo.partyId;
							objTblCostingSource.amount = Convert.ToDouble(huskInfo.transportCost);
							objTblCostingSource.sourceName = "তুষ";
							objTblCostingSource.srcDescription = "তুষ বিক্রয়ের পরিবহন খরচ";
							objTblCostingSource.srcDescId = 21;
							objTblCostingSource.date = huskInfo.date;
							objTblCostingSource.sellId = huskInfo.ID;

							context.tblCostingSources.Add(objTblCostingSource);
							#endregion
						}
                    }
					if (totalPr > 0)
					{
						long maxpayId = context.tblPayables.Select(i => i.ID).DefaultIfEmpty(0).Max();
						tblPayable objTblPayable = new tblPayable();
						objTblPayable.ID = ++maxpayId;
						objTblPayable.partyId = huskInfo.partyId;
						objTblPayable.date = huskInfo.date;
						objTblPayable.sellId = huskInfo.ID;
						var lastPayable = context.tblPayables.Where(p => p.partyId == huskInfo.partyId && p.isActive == 1).FirstOrDefault();
						
						
						var loan = totalPr - huskInfo.paidAmount;
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
                context.tblSells.Add(huskInfo);
				
                #region substract husk from stock                
                STK_Balance huskStk = context.STK_Balance.Where(ss => ss.stockId == huskInfo.stockId && ss.productId == huskInfo.productId).FirstOrDefault();//&& ss.sackWeight==huskInfo.quantity
                if (huskStk == null)
                {
                    var maxStkBalId = context.STK_Balance.Select(p => p.ID).DefaultIfEmpty(0).Max();
                    STK_Balance stkBal = new STK_Balance();
                    stkBal.ID = ++maxStkBalId;
                    stkBal.productId = huskInfo.productId;
                    stkBal.stockId = huskInfo.stockId;
                    stkBal.sackQuantity = -huskInfo.noOfBag;
                    context.STK_Balance.Add(stkBal);

                }
                else
                {
                    huskStk.sackQuantity -= huskInfo.noOfBag;
                }
				#endregion
				
                #region stock transaction
                long maxprdstkId = context.STK_Transaction.Select(p => p.ID).DefaultIfEmpty(0).Max();
                //long laststkId = context.STK_Transaction.Where(s => s.stockId == huskInfo.stockId && s.prodId == huskInfo.productId).Select(l => l.ID).DefaultIfEmpty(0).Max();
				STK_Transaction objStkTrans = new STK_Transaction();
                objStkTrans.ID = maxprdstkId + 1;
                objStkTrans.date = huskInfo.date;
                objStkTrans.rcvQty = 0;
                objStkTrans.sellQty = huskInfo.noOfBag;
                objStkTrans.stockId = huskInfo.stockId.Value;
                objStkTrans.prodId = huskInfo.productId;
                objStkTrans.operation = 1;
                objStkTrans.sellId = huskInfo.ID;
                if (huskStk == null)
                {
                    objStkTrans.openingStock = -huskInfo.noOfBag;
                }
                else
                {
                    objStkTrans.openingStock = huskStk.sackQuantity.Value;
                }
				
				
				
                //var lastTrans = context.STK_Transaction.Where(ll => ll.ID == laststkId).FirstOrDefault();
                //objStkTrans.openingStock = lastTrans == null ? 0 - huskInfo.noOfBag : lastTrans.openingStock - huskInfo.noOfBag;
                context.STK_Transaction.Add(objStkTrans);
                #endregion
                tblSell newSell = new tblSell();
                newSell.ID = huskInfo.ID;
                newSell.incSrcId = huskInfo.incSrcId;
				
                return context.SaveChanges() > 0 ? newSell : null;
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }
		
        public List<STK_tblStock> GetStock()
        {
            List<STK_tblStock> stks = context.STK_tblStock.ToList();
            return stks;
        }
        public tblSell EditHuskInfo(tblSell huskInfo)
        {
            try
            {
                // into payable tabel column amount is 'how many amount is added with opening balance, not paidamount'
                var orgHuskSell = context.tblSells.Where(ss => ss.ID == huskInfo.ID).FirstOrDefault();
                #region edit husk stock
                STK_Balance orgStk = context.STK_Balance.Where(ss => ss.stockId == orgHuskSell.stockId && ss.productId == orgHuskSell.productId).FirstOrDefault();// && ss.sackWeight == particleInfo.quantity
                orgStk.sackQuantity += orgHuskSell.noOfBag;

                STK_Balance newStk = context.STK_Balance.Where(ss => ss.stockId == huskInfo.stockId && ss.productId == huskInfo.productId).FirstOrDefault();// && ss.sackWeight == particleInfo.quantity
                if (newStk == null)
                {
                    STK_Balance stkBal = new STK_Balance();
                    var maxStkBal = context.STK_Balance.Select(p => p.ID).DefaultIfEmpty(0).Max();
                    stkBal.ID = ++maxStkBal;
                    stkBal.productId = huskInfo.productId;
                    stkBal.stockId = huskInfo.stockId;
                    stkBal.sackQuantity = -huskInfo.noOfBag;
                    context.STK_Balance.Add(stkBal);
                }
                else
                {
                    newStk.sackQuantity -= huskInfo.noOfBag;
                }
                #endregion

                double? totalPr = 0;
                if (huskInfo.transportCostInclude)
                {
                    totalPr = huskInfo.noOfBag * huskInfo.unitPrice * huskInfo.quantity + huskInfo.transportCost;
                }
                else
                {
                    totalPr = huskInfo.noOfBag * huskInfo.unitPrice * huskInfo.quantity;

                }

                #region costing source
                tblCostingSource objCostingSource = context.tblCostingSources.Where(cc => cc.sellId == huskInfo.ID).FirstOrDefault();
                if (huskInfo.transportCostInclude && objCostingSource != null)
                {
                    context.tblCostingSources.Remove(objCostingSource);
                }
                else if (!huskInfo.transportCostInclude && objCostingSource == null)
                {
                    tblCostingSource newCost = new tblCostingSource();
                    long maxCost = context.tblCostingSources.Select(i => i.ID).DefaultIfEmpty(0).Max();

                    newCost.ID = ++maxCost;
                    newCost.amount = Convert.ToDouble(huskInfo.transportCost);
                    newCost.partyId = huskInfo.partyId;
                    newCost.sourceName = "তুষ";
                    newCost.srcDescription = "তুষ বিক্রয়ে পরিবহন খরচ";
                    newCost.srcDescId = 21;
                    newCost.date = huskInfo.date;
                    newCost.sellId = huskInfo.ID;
                    context.tblCostingSources.Add(newCost);
                }
                else if (!huskInfo.transportCostInclude && objCostingSource != null)
                {
                    objCostingSource.amount = Convert.ToDouble(huskInfo.transportCost);
                    objCostingSource.date = huskInfo.date;
                }
                #endregion

                #region edit or insert income source
                var orgIncomeSrc = context.tblIncomeSources.Where(ii => ii.ID == huskInfo.incSrcId).FirstOrDefault();

                if (orgIncomeSrc != null)
                {
                    orgIncomeSrc.amount = huskInfo.paidAmount;
                    orgIncomeSrc.date = huskInfo.date;
                    orgIncomeSrc.partyId = huskInfo.partyId;


                }
                else if (orgIncomeSrc == null && huskInfo.paidAmount > 0)
                {
                    long incId = context.tblIncomeSources.Select(i => i.ID).DefaultIfEmpty(0).Max();
                    tblIncomeSource incomeObj = new tblIncomeSource();
                    incomeObj.ID = ++incId;
                    incomeObj.sourceName = "তুষ"; // shoul be come from commonelement
                    incomeObj.srcDescId = 21; // should be come from commonelemnt
                    incomeObj.description = "তুষ বিক্রয় বাবদ আয়";
                    incomeObj.amount = huskInfo.paidAmount;
                    incomeObj.date = huskInfo.date;
                    
                    incomeObj.partyId = huskInfo.partyId;
                    context.tblIncomeSources.Add(incomeObj);
                    orgHuskSell.incSrcId = incomeObj.ID;

                }
                #endregion

                #region edit payable
                var orgPayable = context.tblPayables.Where(ss => ss.sellId == huskInfo.ID).FirstOrDefault();
                //long lastpayId = context.tblPayables.Where(s => s.partyId == particleInfo.partyId).Select(l => l.ID).DefaultIfEmpty(0).Max();
                //var lastPayable = context.tblPayables.Where(pp => pp.ID == lastpayId).FirstOrDefault();
                if (orgPayable != null)
                {
                    double? difference = totalPr - huskInfo.paidAmount - orgPayable.amount;
                    orgPayable.openingBalance = orgPayable.openingBalance + difference;
                    orgPayable.amount = totalPr - huskInfo.paidAmount;
                    var nextPayables = context.tblPayables.Where(pp => pp.partyId == huskInfo.partyId && pp.ID > orgPayable.ID);
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
                    objTblPayable.partyId = huskInfo.partyId;
                    objTblPayable.date = huskInfo.date;
                    objTblPayable.sellId = huskInfo.ID;
                    var lastPayable = context.tblPayables.Where(p => p.partyId == huskInfo.partyId && p.isActive == 1).FirstOrDefault();


                    var loan = totalPr - huskInfo.paidAmount;
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

                var orgStkTrans = context.STK_Transaction.Where(ss => ss.sellId == orgHuskSell.ID).FirstOrDefault();

                double diff = orgStkTrans.sellQty.Value - huskInfo.noOfBag;
                orgStkTrans.openingStock = orgStkTrans.openingStock + orgStkTrans.sellQty.Value;

                orgStkTrans.date = huskInfo.date;
                orgStkTrans.rcvQty = 0;
                orgStkTrans.sellQty = huskInfo.noOfBag;
                orgStkTrans.stockId = huskInfo.stockId.Value;
                orgStkTrans.prodId = huskInfo.productId;
                orgStkTrans.operation = 2;
                orgStkTrans.openingStock = orgStkTrans.openingStock - huskInfo.noOfBag;

                var nextStkTrans = context.STK_Transaction.Where(ss => ss.ID > orgStkTrans.ID && ss.prodId == orgStkTrans.prodId && ss.stockId == orgStkTrans.stockId);
                foreach (var item in nextStkTrans)
                {
                    item.openingStock += diff;
                }
                #endregion

                #region edit tblsell
                orgHuskSell.productId = huskInfo.productId;
                orgHuskSell.productName = huskInfo.productName;
                orgHuskSell.noOfBag = huskInfo.noOfBag;
                orgHuskSell.paidAmount = huskInfo.paidAmount;
                orgHuskSell.partyId = huskInfo.partyId;
                orgHuskSell.partyName = huskInfo.partyName;
                orgHuskSell.quantity = huskInfo.quantity;
                orgHuskSell.stockId = huskInfo.stockId;
                orgHuskSell.stockName = huskInfo.stockName;
                orgHuskSell.unit = huskInfo.unit;
                orgHuskSell.unitPrice = huskInfo.unitPrice;
                orgHuskSell.transportCost = huskInfo.transportCost;
                #endregion

                tblSell newSell = new tblSell();
                newSell.ID = huskInfo.ID;
                newSell.incSrcId = huskInfo.incSrcId;
                return context.SaveChanges() > 0 ? newSell : null;

			}
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
		
        public bool DeleteHuskSell(int pk)
        {
            var orgHuskSell = context.tblSells.Where(ss => ss.ID == pk).FirstOrDefault();

            #region edit stock
            STK_Balance huskStk = context.STK_Balance.Where(ss => ss.stockId == orgHuskSell.stockId && ss.productId == orgHuskSell.productId).FirstOrDefault();//&& ss.sackWeight == particleInfo.quantity
            huskStk.sackQuantity += orgHuskSell.noOfBag;
            #endregion

            #region edit stock transaction
            var delStkTrans = context.STK_Transaction.Where(ss => ss.sellId == orgHuskSell.ID).FirstOrDefault();

            var nextStkTrans = context.STK_Transaction.Where(ss => ss.ID > delStkTrans.ID && ss.prodId == delStkTrans.prodId && ss.stockId == delStkTrans.stockId);
            foreach (var item in nextStkTrans)
            {
                item.openingStock += orgHuskSell.noOfBag;
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
            var orgIncomeSrc = context.tblIncomeSources.Where(ii => ii.ID == orgHuskSell.incSrcId).FirstOrDefault();
            if (orgIncomeSrc != null)
            {
                context.tblIncomeSources.Remove(orgIncomeSrc);
            }
            //delete tblsell
            context.tblSells.Remove(orgHuskSell);

            return context.SaveChanges() > 0;
        }

        public List<object> GetHuskInfoRpt(tblSell huskRpt)
        {
            int parentId = 4;// 4 for husk
            var huskInfoLst = context.sp_GetHuskInfo(huskRpt.partyId, parentId, huskRpt.fromDate, huskRpt.toDate);
            return huskInfoLst.ToList<object>();
        }
        public List<object> GetHuskIncomeRpt(tblSell huskRpt)
        {
            int parentId = 4;// 4 for husk
            var huskInfoLst = context.sp_GetHuskIncWithd(parentId, huskRpt.fromDate, huskRpt.toDate);
            return huskInfoLst.ToList<object>();
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
            reportViewModel.ReportDataSets.Add(new ReportViewModel.ReportDataSet() { DataSetData = objLst, DatasetName = "dataSet_GetHuskInfo" });
            reportViewModel.ReportParams.Add(new ReportParameter("dues", dues.ToString()));
            reportViewModel.ReportParams.Add(new ReportParameter("prevDue", prevDue.ToString()));
            reportViewModel.ReportParams.Add(new ReportParameter("partyName", pname));
            reportViewModel.ReportParams.Add(new ReportParameter("address", area));
            reportViewModel.ReportParams.Add(new ReportParameter("phone", contact));
            reportViewModel.ReportParams.Add(new ReportParameter("startDate", fromDate));
            reportViewModel.ReportParams.Add(new ReportParameter("endDate", toDate));
            return reportViewModel;
        }
        public ReportViewModel GetIncomeViewModel(List<object> objLst, string fromDate, string toDate)
        {
            var reportViewModel = new ReportViewModel()
            {
                Format = ReportViewModel.ReportFormat.PDF,
                ViewAsAttachment = false
            };
            //adding the dataset information to the report view model object
            reportViewModel.ReportDataSets.Add(new ReportViewModel.ReportDataSet() { DataSetData = objLst, DatasetName = "dataSet_HuskIncWithd" });
            reportViewModel.ReportParams.Add(new ReportParameter("startDate", fromDate));
            reportViewModel.ReportParams.Add(new ReportParameter("endDate", toDate));
            return reportViewModel;
        }
    }
}