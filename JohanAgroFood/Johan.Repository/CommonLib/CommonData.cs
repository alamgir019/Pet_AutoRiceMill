using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public static class CommonData
    {
        public static List<tblProduct> GetAllProduct(JohanAgroFoodDBEntities context)
        {
            return context.tblProducts.ToList();
        }

        public static List<tblParty> GetAllParty(JohanAgroFoodDBEntities context)
        {
            List<tblParty> partys = context.tblParties.ToList();
            return partys;
        }

        public static List<STK_tblStock> GetAllStock(JohanAgroFoodDBEntities context)
        {
            List<STK_tblStock> stocks = context.STK_tblStock.ToList();
            return stocks;
        }

        public static List<tblCommonElement> GetAllSector(JohanAgroFoodDBEntities context)
        {
            List<tblCommonElement> sectors = context.tblCommonElements.Where(cc => cc.elementCode == 3).ToList();
            return sectors;
        } 
        public static tblProduct GetProductByName(JohanAgroFoodDBEntities context, string name)
        {
            return context.tblProducts.Where(pp => pp.productName == name).FirstOrDefault();
        }
    }
}
