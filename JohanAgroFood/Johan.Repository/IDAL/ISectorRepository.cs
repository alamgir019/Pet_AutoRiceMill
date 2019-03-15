using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public interface ISectorRepository:IDisposable
    {
        List<tblCommonElement> GetSector(int elemCode);

        bool SaveSector(tblCommonElement Sector);

        bool EditSector(tblCommonElement sector);

        bool DeleteSector(tblCommonElement sector);
    }
}
