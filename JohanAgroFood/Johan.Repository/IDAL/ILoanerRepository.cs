using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public interface ILoanerRepository:IDisposable
    {
        List<tblLoanar> GetLoaner();

        bool SaveLoaner(tblLoanar Loaner);
    }
}
