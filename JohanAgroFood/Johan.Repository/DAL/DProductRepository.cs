using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public class DProductRepository:Disposable,IProductRepository
    {
        private JohanAgroFoodDBEntities context = null;

        public DProductRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {
            this.context = context;    
        }
        public List<tblProduct> GetProduct(tblProduct prod)
        {
            List<tblProduct> prodList = null;
            if (prod == null)
            {
                prodList = context.tblProducts.ToList();
            }   
            else if (prod.ID>0)
            {
                prodList = context.tblProducts.Where(x => x.ID == prod.ID).ToList();
            }
           else if (prod.parentId>=0)
            {
                prodList = context.tblProducts.Where(x => x.parentId == prod.parentId).ToList();
            }

            List<tblProduct> results = new List<tblProduct>();
            foreach (var item in prodList)
            {
                results.Add(new tblProduct()
                {
                    ID = item.ID,
                    productName = item.productName,
                    parentId=item.parentId
                });
            }

            return results;
        }

        public bool EditProduct(tblProduct product)
        {
            var existProd = context.tblProducts.Where(pp=>pp.ID>=product.ID).FirstOrDefault();
            existProd.productName = product.productName;
            existProd.parentId = product.parentId;
            return context.SaveChanges() > 0;
        }

        public bool DeleteProduct(int prodId)
        {
            var existProd = context.tblProducts.Where(pp => pp.ID >= prodId).FirstOrDefault();
            context.tblProducts.Remove(existProd);
            return context.SaveChanges() > 0;
        }

        public bool SaveProduct(tblProduct product)
        {
            product.parentId = product.parentId == null ? 0 : product.parentId;
            context.tblProducts.Add(product);
            return context.SaveChanges() > 0;
        }

    }
}
