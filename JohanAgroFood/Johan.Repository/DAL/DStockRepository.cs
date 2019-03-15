using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.WebForms;

namespace Johan.Repository
{
    public class DStockRepository:Disposable,IStockRepository
    {
        private JohanAgroFoodDBEntities context = null;

        public DStockRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {
            this.context = context;    
        }
        public List<STK_tblStock> GetStock(STK_tblStock stk)
        {
            List<STK_tblStock> stkList = null;
            if (stk == null)
            {
                stkList = context.STK_tblStock.ToList();
            }   
            else if (stk.ID>0)
            {
                stkList = context.STK_tblStock.Where(x => x.ID == stk.ID).ToList();
            }

            return stkList;
        }

        public bool EditStock(STK_tblStock stk)
        {
            var existStk = context.STK_tblStock.Where(ss=>ss.ID>=stk.ID).FirstOrDefault();
            existStk.stockName = stk.stockName;
            existStk.stockAddress = stk.stockAddress;
            return context.SaveChanges() > 0;
        }

        public bool DeleteStock(int stkId)
        {
            var existStk = context.STK_tblStock.Where(ss => ss.ID >= stkId).FirstOrDefault();
            context.STK_tblStock.Remove(existStk);
            return context.SaveChanges() > 0;
        }

        public bool SaveStock(STK_tblStock stk)
        {
            stk.createDate = DateTime.Now;
            context.STK_tblStock.Add(stk);
            return context.SaveChanges() > 0;
        }

        public bool SaveProdStock(STK_Balance prodStk)
        {
            #region stock transaction
            long maxprdstkId = context.STK_Transaction.Select(p => p.ID).DefaultIfEmpty(0).Max();
            long laststkId = context.STK_Transaction.Where(s => s.stockId == prodStk.stockId && s.prodId == prodStk.productId).Select(l => l.ID).DefaultIfEmpty(0).Max();
            STK_Transaction objStkTrans = new STK_Transaction();
            objStkTrans.ID = maxprdstkId+1;
            objStkTrans.date = prodStk.date;
            objStkTrans.rcvQty = prodStk.sackQuantity;
            objStkTrans.sellQty = 0;
            objStkTrans.stockId = prodStk.stockId.Value;
            objStkTrans.prodId = prodStk.productId.Value;
            objStkTrans.operation = 1;
            var lastTrans = context.STK_Transaction.Where(ll => ll.ID == laststkId).FirstOrDefault();
            objStkTrans.openingStock = lastTrans == null ? prodStk.sackQuantity.Value : lastTrans.openingStock + prodStk.sackQuantity.Value;
            context.STK_Transaction.Add(objStkTrans);
            #endregion

            #region stock balance
            int maxId = context.STK_Balance.Select(p => p.ID).DefaultIfEmpty(0).Max();
            var existStk = context.STK_Balance.Where(ss=>ss.productId==prodStk.productId&&ss.stockId==prodStk.stockId).FirstOrDefault();
            if (existStk==null)
            {
                prodStk.ID = ++maxId;
                context.STK_Balance.Add(prodStk);
            }
            else
            {
                existStk.sackQuantity += prodStk.sackQuantity;
            }
            #endregion

            return context.SaveChanges() > 0;
        }

        public List<STK_Balance> GetProdStocks()
        {
            var prodstks = context.STK_Balance.Join(context.tblProducts,
                    stk => stk.productId,
                    prod => prod.ID,
                    (stk, prod) => new
                    {
                        ID = stk.ID,
                        productId = stk.productId,
                        sackQuantity = stk.sackQuantity,
                        stockId = stk.stockId,
                        productName = prod.productName
                    }).Join(context.STK_tblStock, ricep => ricep.stockId, stk => stk.ID,
                    (ricep, stk) => new
                    {
                        ID = ricep.ID,
                        productId = ricep.productId,
                        sackQuantity = ricep.sackQuantity,
                        stockId = ricep.stockId,
                        productName = ricep.productName,
                        stockName = stk.stockName
                    }).ToList();
            List<STK_Balance> rLst = new List<STK_Balance>();

            foreach (var x in prodstks)
            {
                STK_Balance objR = new STK_Balance();

                objR.ID = x.ID;
                objR.productId = x.productId;
             
                objR.sackQuantity = x.sackQuantity;
                objR.stockId = x.stockId;
                objR.productName = x.productName;
                objR.stockName = x.stockName;
                rLst.Add(objR);
            }
            return rLst;
        }

        public bool DeleteProdStock(STK_Balance prodStock)
        {
            #region stock transaction
            long maxprdstkId = context.STK_Transaction.Select(p => p.ID).DefaultIfEmpty(0).Max();
            long laststkId = context.STK_Transaction.Where(s => s.stockId == prodStock.stockId && s.prodId == prodStock.productId).Select(l => l.ID).DefaultIfEmpty(0).Max();

            STK_Transaction objStkTrans = new STK_Transaction();
            objStkTrans.ID = maxprdstkId + 1;
            objStkTrans.date = prodStock.date;
            objStkTrans.rcvQty = 0;
            objStkTrans.sellQty = 0;
            objStkTrans.stockId = prodStock.stockId.Value;
            objStkTrans.prodId = prodStock.productId.Value;
            objStkTrans.operation = 3;
            objStkTrans.date = DateTime.Now;
            var lastTrans = context.STK_Transaction.Where(ll => ll.ID == laststkId).FirstOrDefault();
            objStkTrans.openingStock = lastTrans.openingStock - (prodStock.sackQuantity == null ? 0 : prodStock.sackQuantity.Value);
            context.STK_Transaction.Add(objStkTrans);
            #endregion

            var orgStk = context.STK_Balance.Where(ss => ss.ID == prodStock.ID).FirstOrDefault();
            context.STK_Balance.Remove(orgStk);
            return context.SaveChanges() > 0;
        }

        public bool EditProdStock(STK_Balance prodStock)
        {
            #region stock balance
            var orgStk = context.STK_Balance.Where(ss => ss.ID == prodStock.ID).FirstOrDefault();        

            #region stock transaction
            long maxprdstkId = context.STK_Transaction.Select(p => p.ID).DefaultIfEmpty(0).Max();
            long laststkId = context.STK_Transaction.Where(s => s.stockId == prodStock.stockId && s.prodId == prodStock.productId).Select(l => l.ID).DefaultIfEmpty(0).Max();

            STK_Transaction objStkTrans = new STK_Transaction();
            objStkTrans.ID = maxprdstkId + 1;
            objStkTrans.date = prodStock.date;
            objStkTrans.rcvQty = prodStock.sackQuantity;
            objStkTrans.sellQty = 0;
            objStkTrans.stockId = prodStock.stockId.Value;
            objStkTrans.prodId = prodStock.productId.Value;
            objStkTrans.operation = 2;
            var lastTrans = context.STK_Transaction.Where(ll => ll.ID == laststkId).FirstOrDefault();
            objStkTrans.openingStock = lastTrans.openingStock - orgStk.sackQuantity==null?0:orgStk.sackQuantity + prodStock.sackQuantity==null?0:prodStock.sackQuantity.Value;
            context.STK_Transaction.Add(objStkTrans);
            #endregion

            orgStk.productId = prodStock.productId;
            orgStk.sackQuantity = prodStock.sackQuantity;
            orgStk.stockId = prodStock.stockId;
            orgStk.date = prodStock.date;
            #endregion


            return context.SaveChanges() > 0;
        }
        public List<object> GetStockInfoRpt(STK_Transaction stockRpt)
        {

            var stockInfoLst = context.sp_GetStockInfo(stockRpt.stockId, stockRpt.prodId, stockRpt.fromDate, stockRpt.toDate);
            return stockInfoLst.ToList<object>();
        }
        public ReportViewModel GetRepoertViewModel(List<object> objLst, int stockId, int productId, string fromDate, string toDate)
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
            STK_tblStock stock = context.STK_tblStock.Where(pp => pp.ID == stockId).FirstOrDefault();
            tblProduct product = context.tblProducts.Where(pp => pp.ID == productId).FirstOrDefault();
            string sname = stock.stockName ?? "N/A";
            string pname = product.productName ?? "N/A";
            
            
            //adding the dataset information to the report view model object
            reportViewModel.ReportDataSets.Add(new ReportViewModel.ReportDataSet() { DataSetData = objLst, DatasetName = "dataSet_GetStockInfo" });
            
            reportViewModel.ReportParams.Add(new ReportParameter("stockName", sname));
            reportViewModel.ReportParams.Add(new ReportParameter("productName", pname));
            
            reportViewModel.ReportParams.Add(new ReportParameter("startDate", fromDate));
            reportViewModel.ReportParams.Add(new ReportParameter("endDate", toDate));
            return reportViewModel;
        }
    }
}
