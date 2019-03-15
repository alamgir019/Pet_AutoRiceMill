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
    public class DWithdrawRepository:Disposable,IWithdrawRepository
    {
        private JohanAgroFoodDBEntities context = null;

        public DWithdrawRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {

            this.context = context;    
        }
        public tblCostingSource Save(tblCostingSource withdraw)
        {
            try
            {
                long maxId = context.tblCostingSources.Select(p => p.ID).DefaultIfEmpty(0).Max();
                withdraw.ID = ++maxId;
                context.tblCostingSources.Add(withdraw);
                return context.SaveChanges() > 0 ? withdraw : null;
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }
    }
}