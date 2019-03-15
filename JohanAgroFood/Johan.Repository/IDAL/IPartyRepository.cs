using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public interface IPartyRepository:IDisposable
    {
        List<tblParty> GetParty();
        List<tblCommonElement> GetDistrict(); 
        bool SaveParty(tblParty party);
        bool EditParty(tblParty objEditParty);
        bool Delete(int pk);
    }
}
