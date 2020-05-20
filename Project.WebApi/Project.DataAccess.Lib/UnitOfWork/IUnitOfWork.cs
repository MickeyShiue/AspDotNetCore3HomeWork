using Microsoft.EntityFrameworkCore;
using System;

namespace Project.DataAccess.Lib.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChange();
    }

}
