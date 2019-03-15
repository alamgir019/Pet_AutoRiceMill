using Johan.DATA;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;

using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public class DCarRepository:Disposable,ICarRepository
    {
        private JohanAgroFoodDBEntities context = null;

        public DCarRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {

            this.context = context;    
        }
        public bool SaveCar(tblCar objcar)
        {
            int maxId = context.tblCars.Select(p => p.ID).DefaultIfEmpty(0).Max();
            objcar.ID = ++maxId;
            context.tblCars.Add(objcar);
            return context.SaveChanges() > 0;
        }
        public List<tblCar> GetCar()
        {
            List<tblCar> cars = context.tblCars.ToList();
            return cars;
        }
        public bool EditCar(tblCar car)
        {
            
           context.Entry(car).State = EntityState.Modified;

            return context.SaveChanges() > 0;
        }
        public bool DeleteCar(int pk)
        {
            var orgCar = context.tblCars.Where(ss => ss.ID == pk).FirstOrDefault();
            context.tblCars.Remove(orgCar);
            return context.SaveChanges() > 0;
        }
        public List<object> GetParticleGeneralRpt(tblSell particleRpt)
        {
            var particlegenLst = context.sp_rptParticleGeneral(particleRpt.fromDate, particleRpt.toDate);
            return particlegenLst.ToList<object>();
        }
        public List<object> GetCarRpt(tblCar carRpt)
        {
            var carLst = context.tblCars.Where(pp => pp.carWeightDate >= carRpt.fromDate && pp.carWeightDate <= carRpt.toDate).ToList();
            return carLst.ToList<object>();
        }
        public ReportViewModel CarViewModel(List<object> objLst, string fromDate, string toDate)
        {
            var reportViewModel = new ReportViewModel()
            {
                Format = ReportViewModel.ReportFormat.PDF,
                ViewAsAttachment = false
            };
            //adding the dataset information to the report view model object
            reportViewModel.ReportDataSets.Add(new ReportViewModel.ReportDataSet() { DataSetData = objLst, DatasetName = "dataSet_car" });
            reportViewModel.ReportParams.Add(new ReportParameter("startDate", fromDate));
            reportViewModel.ReportParams.Add(new ReportParameter("endDate", toDate));
            return reportViewModel;
        }
    }
}
