using Johan.DATA;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public class DParticleSellRepository:Disposable,IParticleSellRepository
    {
        private JohanAgroFoodDBEntities context = null;

        public DParticleSellRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {

            this.context = context;    
        }

        public List<tblSell> GetParticleInfo()
        {
            var data = context.sp_getProductSellInfo(0,11);// 11 is id of Particle
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
                sellobj.incSrcId = item.incSrcId;
                sells.Add(sellobj);
            }
            return sells;
        }

        public tblSell SaveParticle(tblSell particleInfo)
        {
            long maxId = context.tblSells.Select(p => p.ID).DefaultIfEmpty(0).Max();
            particleInfo.ID = ++maxId;
            if (particleInfo.paidAmount > 0)
            {
                #region save into income soruce
                long incId = context.tblIncomeSources.Select(i => i.ID).DefaultIfEmpty(0).Max();
                tblIncomeSource incomeObj = new tblIncomeSource();
                incomeObj.ID = ++incId;
                incomeObj.sourceName = "ক্ষুদ"; // shoul be come from commonelement
                incomeObj.srcDescId = 22; // should be come from commonelemnt
                incomeObj.description = "ক্ষুদ বিক্রয় বাবদ আয়";
                incomeObj.amount = particleInfo.paidAmount;
                incomeObj.date = particleInfo.date;
              
                incomeObj.partyId = particleInfo.partyId;
                context.tblIncomeSources.Add(incomeObj);
                #endregion

                particleInfo.incSrcId = incomeObj.ID;
            }

            #region save payable

                double? totalPr = 0;
                if (particleInfo.transportCostInclude)
                {
                    totalPr = particleInfo.noOfBag * particleInfo.unitPrice * particleInfo.quantity + particleInfo.transportCost;
                }
                else
                {
                    totalPr = particleInfo.noOfBag * particleInfo.unitPrice * particleInfo.quantity;
                    if (particleInfo.transportCost>0)
                    {
                        #region costing source
                        tblCostingSource objTblCostingSource = new tblCostingSource();
                        long maxCost = context.tblCostingSources.Select(i => i.ID).DefaultIfEmpty(0).Max();
                        objTblCostingSource.ID = ++maxCost;
                        objTblCostingSource.amount = Convert.ToDouble(particleInfo.transportCost);
                        objTblCostingSource.sourceName = "ক্ষুদ";
                        objTblCostingSource.srcDescription = "ক্ষুদ বিক্রয়ের পরিবহন খরচ";
                        objTblCostingSource.srcDescId = 20;
                        objTblCostingSource.date = particleInfo.date;
                        objTblCostingSource.sellId = particleInfo.ID;

                        context.tblCostingSources.Add(objTblCostingSource);
                        #endregion
                    }
                }
                if (totalPr>0)
                {
                    long maxpayId = context.tblPayables.Select(i => i.ID).DefaultIfEmpty(0).Max();
                    tblPayable objTblPayable = new tblPayable();
                    objTblPayable.ID = ++maxpayId;
                    objTblPayable.partyId = particleInfo.partyId;
                    objTblPayable.date = particleInfo.date;
                    objTblPayable.sellId = particleInfo.ID;
                    var lastPayable = context.tblPayables.Where(p => p.partyId == particleInfo.partyId && p.isActive == 1).FirstOrDefault();


                    var loan = totalPr - particleInfo.paidAmount;
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

            context.tblSells.Add(particleInfo);

            #region substract particle from stock
            STK_Balance particleStk = context.STK_Balance.Where(ss => ss.stockId == particleInfo.stockId && ss.productId == particleInfo.productId).FirstOrDefault();//&& ss.sackWeight==particleInfo.quantity
            if (particleStk == null)
            {
                var maxStkBalId = context.STK_Balance.Select(p => p.ID).DefaultIfEmpty(0).Max();
                STK_Balance stkBal = new STK_Balance();
                stkBal.ID = ++maxStkBalId;
                stkBal.productId = particleInfo.productId;
                stkBal.stockId = particleInfo.stockId;
                stkBal.sackQuantity = -particleInfo.noOfBag;
                context.STK_Balance.Add(stkBal);

            }
            else
            {
                particleStk.sackQuantity -= particleInfo.noOfBag;
            }
            #endregion

            #region stock transaction
            long maxprdstkId = context.STK_Transaction.Select(p => p.ID).DefaultIfEmpty(0).Max();
            //long laststkId = context.STK_Transaction.Where(s => s.stockId == particleInfo.stockId && s.prodId == particleInfo.productId).Select(l => l.ID).DefaultIfEmpty(0).Max();
            STK_Transaction objStkTrans = new STK_Transaction();
            objStkTrans.ID = maxprdstkId + 1;
            objStkTrans.date = particleInfo.date;
            objStkTrans.rcvQty = 0;
            objStkTrans.sellQty = particleInfo.noOfBag;
            objStkTrans.stockId = particleInfo.stockId.Value;
            objStkTrans.prodId = particleInfo.productId;
            objStkTrans.operation = 1;
            objStkTrans.sellId = particleInfo.ID;
            if (particleStk == null)
            {
                objStkTrans.openingStock = -particleInfo.noOfBag;
            }
            else
            {
                objStkTrans.openingStock = particleStk.sackQuantity.Value;
            }



            //var lastTrans = context.STK_Transaction.Where(ll => ll.ID == laststkId).FirstOrDefault();
            //objStkTrans.openingStock = lastTrans == null ? 0 - particleInfo.noOfBag : lastTrans.openingStock - particleInfo.noOfBag;
            context.STK_Transaction.Add(objStkTrans);
            #endregion

            tblSell newSell = new tblSell();
            newSell.ID = particleInfo.ID;
            newSell.incSrcId = particleInfo.incSrcId;
            return context.SaveChanges() > 0 ? newSell : null;
        }

        public tblSell EditParticleSell(tblSell particleInfo)
        {
            try
            {
                // into payable tabel column amount is 'how many amount is added with opening balance, not paidamount'
                var orgParticleSell = context.tblSells.Where(ss => ss.ID == particleInfo.ID).FirstOrDefault();
                #region edit particle stock
                STK_Balance orgStk = context.STK_Balance.Where(ss => ss.stockId == orgParticleSell.stockId && ss.productId == orgParticleSell.productId).FirstOrDefault();// && ss.sackWeight == particleInfo.quantity
                orgStk.sackQuantity += orgParticleSell.noOfBag;

                STK_Balance newStk = context.STK_Balance.Where(ss => ss.stockId == particleInfo.stockId && ss.productId == particleInfo.productId).FirstOrDefault();// && ss.sackWeight == particleInfo.quantity
                if (newStk == null)
                {
                    STK_Balance stkBal = new STK_Balance();
                    var maxStkBal = context.STK_Balance.Select(p => p.ID).DefaultIfEmpty(0).Max();
                    stkBal.ID = ++maxStkBal;
                    stkBal.productId = particleInfo.productId;
                    stkBal.stockId = particleInfo.stockId;
                    stkBal.sackQuantity = -particleInfo.noOfBag;
                    context.STK_Balance.Add(stkBal);
                }
                else
                {
                    newStk.sackQuantity -= particleInfo.noOfBag;
                }
                #endregion

                double? totalPr = 0;
                if (particleInfo.transportCostInclude)
                {
                    totalPr = particleInfo.noOfBag * particleInfo.unitPrice * particleInfo.quantity + particleInfo.transportCost;
                }
                else
                {
                    totalPr = particleInfo.noOfBag * particleInfo.unitPrice * particleInfo.quantity;

                }

                #region costing source
                tblCostingSource objTblCostingSource = context.tblCostingSources.Where(cc => cc.sellId == particleInfo.ID).FirstOrDefault();
                if (particleInfo.transportCostInclude && objTblCostingSource!=null)
                {
                    context.tblCostingSources.Remove(objTblCostingSource);
                }
                else if (!particleInfo.transportCostInclude && objTblCostingSource==null)
                {
                    tblCostingSource newCost = new tblCostingSource();
                    long maxCost = context.tblCostingSources.Select(i => i.ID).DefaultIfEmpty(0).Max();

                    newCost.ID = ++maxCost;
                    newCost.amount = Convert.ToDouble(particleInfo.transportCost);
                    newCost.sourceName = "ক্ষুদ";
                    newCost.srcDescription = "ক্ষুদ বিক্রয়ের পরিবহন খরচ";
                    newCost.srcDescId = 20;
                    newCost.date = particleInfo.date;
                    newCost.sellId = particleInfo.ID;   
                    context.tblCostingSources.Add(newCost);
                }
                else if (!particleInfo.transportCostInclude && objTblCostingSource!=null)
                {
                    objTblCostingSource.amount = Convert.ToDouble(particleInfo.transportCost);
                    objTblCostingSource.date = particleInfo.date;
                }
                #endregion

                #region edit or insert income source
                var orgIncomeSrc = context.tblIncomeSources.Where(ii => ii.ID == particleInfo.incSrcId).FirstOrDefault(); 

                if (orgIncomeSrc != null)
                {
                    orgIncomeSrc.amount = particleInfo.paidAmount;
                    orgIncomeSrc.date = particleInfo.date;
                    orgIncomeSrc.partyId = particleInfo.partyId;

                   
                }
                else if (orgIncomeSrc == null && particleInfo.paidAmount > 0)
                {
                    long incId = context.tblIncomeSources.Select(i => i.ID).DefaultIfEmpty(0).Max();
                    tblIncomeSource incomeObj = new tblIncomeSource();
                    incomeObj.ID = ++incId;
                    incomeObj.sourceName = "ক্ষুদ"; // shoul be come from commonelement
                    incomeObj.srcDescId = 27; // should be come from commonelemnt
                    incomeObj.description = "ক্ষুদ বিক্রয় বাবদ আয়";
                    incomeObj.amount = particleInfo.paidAmount;
                    incomeObj.date = particleInfo.date;
                    
                    incomeObj.partyId = particleInfo.partyId;
                    context.tblIncomeSources.Add(incomeObj);
                    orgParticleSell.incSrcId = incomeObj.ID;
                                                    
                }
                #endregion

                #region edit payable
                var orgPayable = context.tblPayables.Where(ss => ss.sellId == particleInfo.ID).FirstOrDefault();
                //long lastpayId = context.tblPayables.Where(s => s.partyId == particleInfo.partyId).Select(l => l.ID).DefaultIfEmpty(0).Max();
                //var lastPayable = context.tblPayables.Where(pp => pp.ID == lastpayId).FirstOrDefault();
                if (orgPayable != null)
                {
                    double? difference = totalPr - particleInfo.paidAmount - orgPayable.amount;
                    orgPayable.openingBalance = orgPayable.openingBalance + difference;
                    orgPayable.amount=totalPr-particleInfo.paidAmount;
                    var nextPayables = context.tblPayables.Where(pp =>pp.partyId==particleInfo.partyId && pp.ID>orgPayable.ID);
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
                    objTblPayable.partyId = particleInfo.partyId;
                    objTblPayable.date = particleInfo.date;
                    objTblPayable.sellId = particleInfo.ID;
                    var lastPayable = context.tblPayables.Where(p => p.partyId == particleInfo.partyId && p.isActive == 1).FirstOrDefault();


                    var loan = totalPr - particleInfo.paidAmount;
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

                var orgStkTrans = context.STK_Transaction.Where(ss=>ss.sellId==orgParticleSell.ID).FirstOrDefault();

                double diff = orgStkTrans.sellQty.Value - particleInfo.noOfBag;
                orgStkTrans.openingStock = orgStkTrans.openingStock+orgStkTrans.sellQty.Value;

                orgStkTrans.date = particleInfo.date;
                orgStkTrans.rcvQty = 0;
                orgStkTrans.sellQty = particleInfo.noOfBag;
                orgStkTrans.stockId = particleInfo.stockId.Value;
                orgStkTrans.prodId = particleInfo.productId;
                orgStkTrans.operation = 2;
                orgStkTrans.openingStock = orgStkTrans.openingStock-particleInfo.noOfBag;

                var nextStkTrans = context.STK_Transaction.Where(ss => ss.ID > orgStkTrans.ID && ss.prodId == orgStkTrans.prodId && ss.stockId == orgStkTrans.stockId);
                foreach (var item in nextStkTrans)
                {
                    item.openingStock += diff;
                }
                #endregion

                #region edit tblsell
                orgParticleSell.productId = particleInfo.productId;
                orgParticleSell.productName = particleInfo.productName;
                orgParticleSell.noOfBag = particleInfo.noOfBag;
                orgParticleSell.paidAmount = particleInfo.paidAmount;
                orgParticleSell.partyId = particleInfo.partyId;
                orgParticleSell.partyName = particleInfo.partyName;
                orgParticleSell.quantity = particleInfo.quantity;
                orgParticleSell.stockId = particleInfo.stockId;
                orgParticleSell.stockName = particleInfo.stockName;
                orgParticleSell.unit = particleInfo.unit;
                orgParticleSell.unitPrice = particleInfo.unitPrice;
                orgParticleSell.transportCost = particleInfo.transportCost;
                #endregion

                tblSell newSell = new tblSell();
                newSell.ID = particleInfo.ID;
                newSell.incSrcId = particleInfo.incSrcId;
                return context.SaveChanges() > 0 ? newSell : null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteParticleSell(int pk)
        {
            var orgParticleSell = context.tblSells.Where(ss => ss.ID == pk).FirstOrDefault();

            #region edit particle stock
            STK_Balance particleStk = context.STK_Balance.Where(ss => ss.stockId == orgParticleSell.stockId && ss.productId == orgParticleSell.productId).FirstOrDefault();//&& ss.sackWeight == particleInfo.quantity
            particleStk.sackQuantity += orgParticleSell.noOfBag;
            #endregion

            #region delete stock transaction
            var delStkTrans = context.STK_Transaction.Where(ss => ss.sellId == orgParticleSell.ID).FirstOrDefault();

            var nextStkTrans = context.STK_Transaction.Where(ss => ss.ID > delStkTrans.ID && ss.prodId == delStkTrans.prodId && ss.stockId == delStkTrans.stockId);
            foreach (var item in nextStkTrans)
            {
                item.openingStock += orgParticleSell.noOfBag;
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
            List<tblCostingSource> objTblCostingSource = context.tblCostingSources.Where(cc => cc.sellId == pk).ToList();

            if (objTblCostingSource != null)
            {
                foreach (var item in objTblCostingSource)
                {                                       
                context.tblCostingSources.Remove(item);                    
                }
            }
            #endregion


            // delete income source
            var orgIncomeSrc = context.tblIncomeSources.Where(ii => ii.ID == orgParticleSell.incSrcId).FirstOrDefault();
            if (orgIncomeSrc != null)
            {
                context.tblIncomeSources.Remove(orgIncomeSrc);
            }
            //delete tblsell
            context.tblSells.Remove(orgParticleSell);

            return context.SaveChanges() > 0;
        }
       
        public ReportViewModel ParticleGeneralViewModel(List<object> objLst, string fromDate, string toDate)
        {
            var reportViewModel = new ReportViewModel()
            {
                Format = ReportViewModel.ReportFormat.PDF,
                ViewAsAttachment = false
            };
            //adding the dataset information to the report view model object
            reportViewModel.ReportDataSets.Add(new ReportViewModel.ReportDataSet() { DataSetData = objLst, DatasetName = "dataSet_generalParticle" });
            reportViewModel.ReportParams.Add(new ReportParameter("startDate", fromDate));
            reportViewModel.ReportParams.Add(new ReportParameter("endDate", toDate));
            return reportViewModel;
        }

        public List<object> GetParticleGeneralRpt(tblSell particleRpt)
        {
            var particlegenLst = context.sp_rptParticleGeneral(particleRpt.fromDate, particleRpt.toDate);
            return particlegenLst.ToList<object>();
        }
         
        public List<STK_tblStock> GetStock()
        {
            List<STK_tblStock> stks = context.STK_tblStock.ToList();
            return stks;
        }

        public bool SaveParticleStock(STK_Balance particleStk)
        {
            int maxId = context.STK_Balance.Select(p => p.ID).DefaultIfEmpty(0).Max();
            particleStk.ID = ++maxId;
            context.STK_Balance.Add(particleStk);
            return context.SaveChanges() > 0;
        }

        public List<STK_Balance> GetParticleStock()
        {
            var particlestks = context.STK_Balance.Join(context.tblProducts,
                    particle => particle.productId,
                    prod => prod.ID,
                    (particle, prod) => new
                    {
                        ID = particle.ID,
                        productId = particle.productId,
                        sackQuantity = particle.sackQuantity,
                        stockId = particle.stockId,
                        productName = prod.productName
                    }).Join(context.STK_tblStock, particlep => particlep.stockId, stk => stk.ID,
                    (particlep, stk) => new
                    {
                        ID = particlep.ID,
                        productId = particlep.productId,
                        sackQuantity = particlep.sackQuantity,
                        stockId = particlep.stockId,
                        productName = particlep.productName,
                        stockName = stk.stockName
                    }).ToList();
            List<STK_Balance> rLst = new List<STK_Balance>();

            foreach (var x in particlestks)
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

        public bool DeleteParticleStock(STK_Balance particleStk)
        {
            var orgStk = context.STK_Balance.Where(ss => ss.ID == particleStk.ID).FirstOrDefault();
            context.STK_Balance.Remove(orgStk);
            return context.SaveChanges() > 0;
        }

        public bool EditParticleStock(STK_Balance particleStk)
        {
            var orgStk = context.STK_Balance.Where(ss => ss.ID == particleStk.ID).FirstOrDefault();
            orgStk.productId = particleStk.productId;
            orgStk.sackQuantity = particleStk.sackQuantity;
            orgStk.stockId = particleStk.stockId;

            return context.SaveChanges() > 0;
        }
    }
}