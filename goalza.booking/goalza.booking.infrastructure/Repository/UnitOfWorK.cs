using goalza.booking.core.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace goalza.booking.infrastructure.Repository
{
    public class UnitOfWorK :IUnitOfWork
    {
        private readonly BookingContext _context;

        public UnitOfWorK(BookingContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _context.Database.RollbackTransaction();
        }
    }
}
