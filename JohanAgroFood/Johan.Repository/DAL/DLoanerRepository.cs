using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public class DLoanerRepository:Disposable,ILoanerRepository
    {
        private JohanAgroFoodDBEntities context = null;

        public DLoanerRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {
            this.context = context;    
        }
        public List<tblLoanar> GetLoaner()
        {
            List<tblLoanar> loaners = context.tblLoanars.ToList();
            //List<tblLoanar> results = new List<tblLoanar>();
            //foreach (var item in loaners)
            //{
            //    results.Add(new tblParty() { ID=item.ID, name=item.name, contactNo=item.contactNo, area=item.area,
            //     district=item.district, zoneId=item.zoneId, productId=item.productId, isCashParty=item.isCashParty});
            //}
            return loaners;
        }

        public bool SaveLoaner(tblLoanar loaner)
        {
            int maxId = context.tblLoanars.Select(p => p.ID).DefaultIfEmpty(0).Max();
            loaner.ID = ++maxId;
            context.tblLoanars.Add(loaner);
            return context.SaveChanges() > 0;
        }

    }
}
