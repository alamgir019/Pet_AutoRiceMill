using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Johan.DATA;

namespace Johan.Repository.IDAL
{
    public interface ILoanPaymentRepository:IDisposable
    {
        tblDue GetLoanPayment(int objPartyId);
        long Save(tblCostingSource objLoanCostingSource);
        List<tblCostingSource> GetLoanPaid(DateTime date);
    }
}
