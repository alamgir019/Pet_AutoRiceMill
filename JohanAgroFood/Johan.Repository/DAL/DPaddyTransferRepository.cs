using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Johan.DATA;
using Johan.Repository.IDAL;

namespace Johan.Repository.DAL
{
    public class DPaddyTransferRepository:Disposable,IPaddyTransferRepository
    {
        private JohanAgroFoodDBEntities context = null;
        public DPaddyTransferRepository(JohanAgroFoodDBEntities context) : base(context)
        {
            this.context = context;
        }

        public List<STK_Balance> GetSackWeights(int productId, int stockId)
        {
            List<STK_Balance> sackTypeList=null;
            if (productId != 0)
            {
                sackTypeList = context.STK_Balance.Where(ss => ss.productId == productId && ss.stockId == stockId).ToList();
            }
            return sackTypeList;
        }

        public long SavePaddyTransfer(STK_Balance objStkBalance)
        {
            var transferFrom =
                context.STK_Balance.Where(
                    ss => ss.stockId == objStkBalance.stockId && ss.productId == objStkBalance.productId && ss.sackWeight == objStkBalance.sackWeight).FirstOrDefault();

            transferFrom.sackQuantity = transferFrom.sackQuantity - objStkBalance.sackQuantity;
            if (transferFrom.sackQuantity<0)
            {
                return 0;
            }
            var transferTo = context.STK_Balance.Where(
                    ss => ss.stockId == objStkBalance.targetStockId && ss.productId == objStkBalance.productId && ss.sackWeight == objStkBalance.sackWeight).FirstOrDefault();
            if (transferTo!=null)
            {

                transferTo.sackQuantity += objStkBalance.sackQuantity;
            }
            else 
            {
                var maxId = context.STK_Balance.Select(ss => ss.ID).DefaultIfEmpty(0).Max();
                STK_Balance stockTo = new STK_Balance();
                stockTo.ID = ++maxId;
                stockTo.stockId = objStkBalance.targetStockId;
                stockTo.sackWeight = objStkBalance.sackWeight;
                context.STK_Balance.Add(stockTo);
            }
            #region paddy transaction
            var maxTransId = context.PaddyTransactions.Select(ss => ss.ID).DefaultIfEmpty(0).Max();
            PaddyTransaction exstRlsStk = context.PaddyTransactions.Where(pp=>pp.prodId==objStkBalance.productId && pp.stockId==objStkBalance.stockId && pp.bagWeight==objStkBalance.sackWeight).FirstOrDefault();
            PaddyTransaction objTransRls = new PaddyTransaction();
            objTransRls.ID = ++maxTransId;
            objTransRls.date = objStkBalance.date;
            objTransRls.bagWeight = objStkBalance.sackWeight;
            objTransRls.stockId = objStkBalance.stockId.Value;
            objTransRls.prodId = objStkBalance.productId.Value;
            objTransRls.releaseQty = objStkBalance.sackQuantity;
            objTransRls.openingStock = exstRlsStk == null ? 0 : exstRlsStk.openingStock - objStkBalance.sackQuantity.Value;
            context.PaddyTransactions.Add(objTransRls);

            PaddyTransaction exstRcvStk = context.PaddyTransactions.Where(pp => pp.prodId == objStkBalance.productId && pp.stockId == objStkBalance.targetStockId && pp.bagWeight == objStkBalance.sackWeight).FirstOrDefault();
            PaddyTransaction objTransRcv = new PaddyTransaction();
            objTransRcv.ID = ++maxTransId;
            objTransRcv.date = objStkBalance.date;
            objTransRcv.bagWeight = objStkBalance.sackWeight;
            objTransRcv.stockId = objStkBalance.targetStockId;
            objTransRcv.prodId = objStkBalance.productId.Value;
            objTransRcv.rcvQty = objStkBalance.sackQuantity;
            objTransRcv.openingStock = exstRcvStk == null ? 0 : exstRcvStk.openingStock + objStkBalance.sackQuantity.Value;
            objTransRcv.serial = objStkBalance.serial;
            objTransRcv.millCost = objStkBalance.millCost;
            objTransRcv.fromStock = objStkBalance.stockId;
            context.PaddyTransactions.Add(objTransRcv);
            #endregion
            return context.SaveChanges() > 0 ? objTransRcv.ID : 0;
        }

        public List<STK_Balance> GetPaddyTransferInfos()
        {
            var results = from bal in context.STK_Balance
                join stk in context.STK_tblStock on bal.stockId equals stk.ID
                join prod in context.tblProducts on bal.productId equals prod.ID where prod.parentId==2054
                select new
                {
                    stk.stockName,
                    prod.productName,
                    bal.sackWeight,
                    bal.sackQuantity
                };
            List<STK_Balance> balanceList=new List<STK_Balance>();
            foreach (var items in results)
            {
                STK_Balance objStkBalance=new STK_Balance();
                 objStkBalance.stockName= items.stockName;
                objStkBalance.productName = items.productName;
                objStkBalance.sackWeight = items.sackWeight;
                objStkBalance.sackQuantity = items.sackQuantity;
                balanceList.Add(objStkBalance);

            }
            return balanceList;
        }

        //public bool EditPaddyTransfer(STK_Balance objStkBalance)
        //{
        //    var editOrg = context.STK_Balance.Where(ss => ss.ID == objStkBalance.ID).FirstOrDefault();
        //    editOrg.stockId = objStkBalance.stockId;
        //    editOrg.productId = objStkBalance.productId;
        //    editOrg.sackQuantity = objStkBalance.sackQuantity;
        //    editOrg.sackWeight = objStkBalance.bagWeight;
        //    return context.SaveChanges() > 0;
        //}
        //public bool DeletePaddyTransfer(int pk)
        //{
        //    var deleteOrg = context.STK_Balance.Where(ss => ss.ID == pk).FirstOrDefault();
        //    context.STK_Balance.Remove(deleteOrg);

        //    return context.SaveChanges() > 0;
        //}
    }
}
