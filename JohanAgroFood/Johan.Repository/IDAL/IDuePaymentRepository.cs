using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Johan.DATA;

namespace Johan.Repository.IDAL
{
    public interface IDuePaymentRepository:IDisposable
    {
        tblPayable GetDuePayment(int objPartyId);
        long Save(tblIncomeSource objDueIncomeSource);
        List<tblIncomeSource> GetDuePaid(DateTime date);
    }
}
