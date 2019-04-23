using System;
using System.Collections.Generic;
using System.Text;

namespace goalza.booking.core.IRepository
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
