using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public interface IProductRepository:IDisposable
    {
        List<tblProduct> GetProduct(tblProduct prod);

        bool SaveProduct(tblProduct product);

        bool EditProduct(tblProduct product);
        bool DeleteProduct(int prodId);
    }
}
