using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public class DZoneRepository:Disposable,IZoneRepository
    {
        private JohanAgroFoodDBEntities context = null;

        public DZoneRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {
            this.context = context;    
        }
        public List<tblCommonElement> GetZone()
        {
            List<tblCommonElement> zones = context.tblCommonElements.Where(z=>z.elementCode==1).ToList();
            return zones;
        }


    }
}
