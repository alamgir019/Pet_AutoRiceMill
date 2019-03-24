using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public interface IZoneRepository:IDisposable
    {
        List<tblCommonElement> GetZone();
    }
}
