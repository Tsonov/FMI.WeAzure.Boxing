using FMI.WeAzure.Boxing.Common;
using FMI.WeAzure.Boxing.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Business.Handlers
{
    public abstract class BaseHandler : IDisposable
    {
        protected readonly BoxingDbContext Context;

        protected BaseHandler(BoxingDbContext context)
        {
            Check.ThrowIfNull(context, "context", "Provided database context can not be null");
            this.Context = new BoxingDbContext();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
        }
    }
}
