using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public interface IIncomeRepository:IDisposable
    {
        List<tblIncomeSource> GetIncome();

        bool SaveIncome(tblIncomeSource income);

        bool EditIncome(tblIncomeSource income);

        bool DeleteIncome(tblIncomeSource income);
    }
}
