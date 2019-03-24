using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public class DSectorRepository : Disposable, ISectorRepository
    {
        private JohanAgroFoodDBEntities context = null;

        public DSectorRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {
            this.context = context;
        }
        public List<tblCommonElement> GetSector(int elemCode)
        {
            List<tblCommonElement> sectors = context.tblCommonElements.Where(ss => ss.elementCode == elemCode).ToList(); // 3 for consumption or income sector           
            return sectors;
        }

        public bool SaveSector(tblCommonElement sector)
        {
            int maxId = context.tblCommonElements.Select(cc => cc.ID).DefaultIfEmpty(0).Max();
            sector.ID = ++maxId;
            //sector.elementCode = 3;
            context.tblCommonElements.Add(sector);
            return context.SaveChanges() > 0;
        }
        public bool EditSector(tblCommonElement sector)
        {
            var orgSec = context.tblCommonElements.Where(ss => ss.ID == sector.ID).FirstOrDefault();
            orgSec.elementName = sector.elementName;
            return context.SaveChanges() > 0;
        }
        public bool DeleteSector(tblCommonElement sector)
        {
            var orgSec = context.tblCommonElements.Where(ss => ss.ID == sector.ID).FirstOrDefault();
            context.tblCommonElements.Remove(orgSec);
            return context.SaveChanges() > 0;
        }
    }
}
