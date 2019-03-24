using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Johan.DATA;
using Johan.Repository.IDAL;
using Microsoft.Reporting.WebForms;

namespace Johan.Repository.DAL
{
    public class DReportRepository:Disposable,IReportRepository
    {
        private JohanAgroFoodDBEntities context = null;

        public DReportRepository(JohanAgroFoodDBEntities context):base(context)
        {
            this.context = context;
        }
        public List<object> GetDailySellInfoRpt(tblSell dailySellRpt)
        {

            var dailySellInfoLst = context.sp_GetDailySellInfo(dailySellRpt.parentProd, dailySellRpt.toDate);
            return dailySellInfoLst.ToList<object>();
        }
        public ReportViewModel GetRepoertViewModel(List<object> objLst,int productId,string todayDate)
        {
            var reportViewModel = new ReportViewModel()
            {
                LeftMainTitle = "Johan Group",
                LeftSubTitle = "IT",
                Name = "Daily Sell Report",
                //ReportDate = DateTime.Now,
                ReportLogo = "~/Content/logo.jpg",
                //ReportTitle = "Retailerwise Sales Report",
                ReportLanguage = "en-US",
                //UserNamPrinting = UserPrinting,
                Format = ReportViewModel.ReportFormat.PDF,
                ViewAsAttachment = false
            };
            string product = "";
            if (productId == 1)
            {
                product = "প্রোডাক্টের";
            }
            else if (productId == 2)
            {
                product = "তুষের";
            }


            //adding the dataset information to the report view model object
            reportViewModel.ReportDataSets.Add(new ReportViewModel.ReportDataSet() { DataSetData = objLst, DatasetName = "DataSet_GetDailySellInfo" });
            //reportViewModel.ReportDataSets.Add(new ReportViewModel.ReportDataSet("productName", prod.productName));
           // reportViewModel.ReportDataSets.Add(new ReportViewModel.ReportDataSet("parentProd",productId));
            reportViewModel.ReportParams.Add(new ReportParameter("todayDate", todayDate));
            reportViewModel.ReportParams.Add(new ReportParameter("productName", product));
            return reportViewModel;
        }

        public List<object> GetDailyBuyInfoRpt(tblBuy dailyBuyRpt)
        {
            var dailySellInfoLst = context.sp_getDailyPaddyBuy(dailyBuyRpt.toDate);
            return dailySellInfoLst.ToList<object>();
        }
        public ReportViewModel GetDailyBuyVM(List<object> objLst, string todayDate)
        {
            var reportViewModel = new ReportViewModel()
            {
                LeftMainTitle = "Johan Group",
                LeftSubTitle = "IT",
                Name = "Daily Sell Report",
                //ReportDate = DateTime.Now,
                ReportLogo = "~/Content/logo.jpg",
                //ReportTitle = "Retailerwise Sales Report",
                ReportLanguage = "en-US",
                //UserNamPrinting = UserPrinting,
                Format = ReportViewModel.ReportFormat.PDF,
                ViewAsAttachment = false
            };
            string product = "ধানের";

            //adding the dataset information to the report view model object
            reportViewModel.ReportDataSets.Add(new ReportViewModel.ReportDataSet() { DataSetData = objLst, DatasetName = "DataSet_getDailyPaddyBuy" });
            //reportViewModel.ReportDataSets.Add(new ReportViewModel.ReportDataSet("productName", prod.productName));
            // reportViewModel.ReportDataSets.Add(new ReportViewModel.ReportDataSet("parentProd",productId));
            reportViewModel.ReportParams.Add(new ReportParameter("todayDate", todayDate));
            reportViewModel.ReportParams.Add(new ReportParameter("productName", product));
            return reportViewModel;
        }

        public List<object> GetOtherConsumpRpt(string starDate,string endDate,int sectorId)
        {
            DateTime fromD = Convert.ToDateTime(starDate);
            DateTime toD = Convert.ToDateTime(endDate);

            var otherConsumpLst = from consump in context.tblCostingSources.Where(cc => cc.date >= fromD && cc.date <= toD && cc.amount > 0 && (sectorId == 0 || cc.srcDescId == sectorId))
                                  select new
                                  {
                                      consump.date,
                                      consump.srcDescription,
                                      consump.amount
                                  }; //here 49 is id of other consumption
            List<object> result = otherConsumpLst.ToList<object>();

            if (sectorId == 0)
            {
                var salaryLst = from sal in context.SalaryPayments.Where(ss => ss.date >= fromD && ss.date <= toD)
                                join emp in context.Employees on sal.empId equals emp.ID
                                select new
                                {
                                    sal.date,
                                    srcDescription = emp.empName + " এর বেতন",
                                    amount = sal.paidAmount
                                };

                if (salaryLst != null)
                {
                    foreach (var item in salaryLst.ToList<object>())
                    {
                        result.Add(item);
                    }
                }
            }
            //result.OrderBy(m => m.GetType().GetProperties().First(n => n.Name == "date").GetValue(m, null)); // its required more time
            return result;
        }
        
        public ReportViewModel GetOtherConsumpViewModel(List<object> objLst, string startDate, string endDate, int sectorId)
        {
            var reportViewModel = new ReportViewModel()
            {
                LeftMainTitle = "Johan Group",
                LeftSubTitle = "IT",
                Name = "Daily Sell Report",
                //ReportDate = DateTime.Now,
                ReportLogo = "~/Content/logo.jpg",
                //ReportTitle = "Retailerwise Sales Report",
                ReportLanguage = "en-US",
                //UserNamPrinting = UserPrinting,
                Format = ReportViewModel.ReportFormat.PDF,
                ViewAsAttachment = false
            };
            var sec=context.tblCommonElements.Where(cc => cc.ID == sectorId).FirstOrDefault();
            string sectorName = sec == null ? "সকল" :sec.elementName+ " বাবদ";
            //adding the dataset information to the report view model object
            reportViewModel.ReportDataSets.Add(new ReportViewModel.ReportDataSet() { DataSetData = objLst, DatasetName = "datset_OtherConsump" });
            reportViewModel.ReportParams.Add(new ReportParameter("startDate", startDate));
            reportViewModel.ReportParams.Add(new ReportParameter("endDate", endDate));
            reportViewModel.ReportParams.Add(new ReportParameter("sectorName",sectorName));
            return reportViewModel;
        }
        public List<object> GetIncomeRpt(string starDate, string endDate, int sectorId)
        {
            DateTime fromD = Convert.ToDateTime(starDate);
            DateTime toD = Convert.ToDateTime(endDate);

            var incomeLst = from inc in context.tblIncomeSources.Where(cc => cc.date >= fromD && cc.date <= toD && cc.amount > 0 && (sectorId == 0 || cc.srcDescId == sectorId))
                                  select new
                                  {
                                      inc.date,
                                      inc.description,
                                      inc.amount
                                  }; //here 49 is id of other consumption
            List<object> result = incomeLst.ToList<object>();

        //    if (sectorId == 0)
        //    {
        //        var salaryLst = from sal in context.SalaryPayments.Where(ss => ss.date >= fromD && ss.date <= toD)
        //                        join emp in context.Employees on sal.empId equals emp.ID
        //                        select new
        //                        {
        //                            sal.date,
        //                            srcDescription = emp.empName + " এর বেতন",
        //                            amount = sal.paidAmount
        //                        };

        //        if (salaryLst != null)
        //        {
        //            foreach (var item in salaryLst.ToList<object>())
        //            {
        //                result.Add(item);
        //            }
        //        }
        //    }
            return result;
        }
        public ReportViewModel GetIncomeVM(List<object> objLst, string startDate, string endDate, int sectorId)
        {
            var reportViewModel = new ReportViewModel()
            {
                LeftMainTitle = "Johan Group",
                LeftSubTitle = "IT",
                Name = "Daily Sell Report",
                //ReportDate = DateTime.Now,
                ReportLogo = "~/Content/logo.jpg",
                //ReportTitle = "Retailerwise Sales Report",
                ReportLanguage = "en-US",
                //UserNamPrinting = UserPrinting,
                Format = ReportViewModel.ReportFormat.PDF,
                ViewAsAttachment = false
            };
            var sec = context.tblCommonElements.Where(cc => cc.ID == sectorId).FirstOrDefault();
            string sectorName = sec == null ? "সকল" : sec.elementName + " বাবদ";
            //adding the dataset information to the report view model object
            reportViewModel.ReportDataSets.Add(new ReportViewModel.ReportDataSet() { DataSetData = objLst, DatasetName = "datset_income" });
            reportViewModel.ReportParams.Add(new ReportParameter("startDate", startDate));
            reportViewModel.ReportParams.Add(new ReportParameter("endDate", endDate));
            reportViewModel.ReportParams.Add(new ReportParameter("sectorName", sectorName));
            return reportViewModel;
        }     
        public List<object> GetHollarConsumpRpt(string starDate, string endDate, string stockId)
        {
            DateTime fromD = Convert.ToDateTime(starDate);
            DateTime toD = Convert.ToDateTime(endDate);
            int stock = Convert.ToInt32(stockId);
            STK_tblStock hollar = context.STK_tblStock.Where(ss => ss.stockName == "হোলার").FirstOrDefault();
            var hollarConsumpLst = from paddTrans in context.PaddyTransactions.Where(cc => cc.stockId == hollar.ID && cc.date >= fromD && cc.date <= toD &&(stock==0 || cc.fromStock==stock)) // here 5 is id of hollar
                                   join prod in context.tblProducts on paddTrans.prodId equals prod.ID
                                   //join stk in context.STK_tblStock on paddTrans.fromStock equals stk.ID
                                   select new
                                   {
                                       paddTrans.date,
                                       paddTrans.serial,
                                       paddTrans.bagWeight,
                                       paddTrans.rcvQty,
                                       paddTrans.millCost,
                                       prod.productName
                                       //stk.stockName
                                   };
            return hollarConsumpLst.ToList<object>();
        }
        public ReportViewModel GetHollarConsumpViewModel(List<object> objLst, string startDate, string endDate,string stockId)
        {
            var reportViewModel = new ReportViewModel()
            {
                LeftMainTitle = "Johan Group",
                LeftSubTitle = "IT",
                Name = "Hollar Cost",
                //ReportDate = DateTime.Now,
                ReportLogo = "~/Content/logo.jpg",
                //ReportTitle = "Retailerwise Sales Report",
                ReportLanguage = "en-US",
                //UserNamPrinting = UserPrinting,
                Format = ReportViewModel.ReportFormat.PDF,
                ViewAsAttachment = false
            };
            int stock = Convert.ToInt32(stockId);
            STK_tblStock stk = context.STK_tblStock.Where(ss=>ss.ID==stock).FirstOrDefault();
            string stkName = stk == null ? "উভয় মিল" : stk.stockName;
            //adding the dataset information to the report view model object
            reportViewModel.ReportDataSets.Add(new ReportViewModel.ReportDataSet() { DataSetData = objLst, DatasetName = "datset_HollarConsump" });
            
            reportViewModel.ReportParams.Add(new ReportParameter("startDate", startDate));
            reportViewModel.ReportParams.Add(new ReportParameter("endDate", endDate));
            reportViewModel.ReportParams.Add(new ReportParameter("stock", stkName));
            return reportViewModel;
        }

        public List<object> GetPaddyStockRpt(PaddyTransaction stockRpt)
        {

            var stockInfoLst = context.sp_RptPaddyStockInfo(stockRpt.stockId,stockRpt.prodId, stockRpt.fromDate, stockRpt.toDate);
            return stockInfoLst.ToList<object>();
        }
        public ReportViewModel GetPaddyStockViewModel(List<object> objLst, int stockId, int productId, string fromDate, string toDate)
        {
            var reportViewModel = new ReportViewModel()
            {
                LeftMainTitle = "Johan Group",
                LeftSubTitle = "IT",
                Name = "Stock Report",
                //ReportDate = DateTime.Now,
                ReportLogo = "~/Content/logo.jpg",
                //ReportTitle = "Retailerwise Sales Report",
                ReportLanguage = "en-US",
                //UserNamPrinting = UserPrinting,
                Format = ReportViewModel.ReportFormat.PDF,
                ViewAsAttachment = false
            };
            tblProduct product = context.tblProducts.Where(pp => pp.ID == productId).FirstOrDefault();
            string pname = product.productName ?? "N/A";

            STK_tblStock stk = context.STK_tblStock.Where(ss => ss.ID == stockId).FirstOrDefault();
            string stockName = stk.stockName ?? "N/A";
            DateTime fdate = Convert.ToDateTime(fromDate);
            DateTime tdate = Convert.ToDateTime(toDate);
            var result = context.func_paddyparta(stockId, productId, fdate, tdate).ToList();
            var prodStocks = context.STK_Balance.Where(ss => ss.stockId == stockId && ss.productId == productId).ToList();
            double totBag = 0, totQty = 0;
            foreach (var item in prodStocks)
            {
                totBag += item.sackQuantity ?? 0;
                totQty += (item.sackQuantity * item.sackWeight) ?? 0;
            }
            double avgparta = result.Count > 0 ? result[0].Value : 0;

            //adding the dataset information to the report view model object
            reportViewModel.ReportDataSets.Add(new ReportViewModel.ReportDataSet() { DataSetData = objLst, DatasetName = "dataSet_PaddyStockInfo" });

            reportViewModel.ReportParams.Add(new ReportParameter("stockName", stockName));
            reportViewModel.ReportParams.Add(new ReportParameter("productName", pname));
            reportViewModel.ReportParams.Add(new ReportParameter("startDate", fromDate));
            reportViewModel.ReportParams.Add(new ReportParameter("endDate", toDate));
            reportViewModel.ReportParams.Add(new ReportParameter("avgParta", avgparta.ToString())); //"#.##"
            reportViewModel.ReportParams.Add(new ReportParameter("totBag", totBag.ToString())); //"#.##"
            reportViewModel.ReportParams.Add(new ReportParameter("totQty", totQty.ToString())); //"#.##"
            return reportViewModel;
        }


    }
}
