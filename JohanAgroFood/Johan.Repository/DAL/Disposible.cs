using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public class Disposable
    {
        private bool disposed = false;
        private JohanAgroFoodDBEntities context;
        public Disposable(JohanAgroFoodDBEntities context)
        {
            this.context = context;
            this.context.Configuration.ProxyCreationEnabled = false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
