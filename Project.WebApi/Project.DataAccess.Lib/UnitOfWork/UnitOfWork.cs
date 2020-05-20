using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Lib.Models;
using System;

namespace Project.DataAccess.Lib.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        public UnitOfWork(ContosouniversityContext context)
        {
            this.context = context;
        }

        public int SaveChange()
        {
            return this.context.SaveChanges();
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
