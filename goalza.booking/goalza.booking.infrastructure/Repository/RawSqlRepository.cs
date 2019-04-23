using goalza.booking.core.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace goalza.booking.infrastructure.Repository
{
    public class RawSqlRepository : IRawSqlRepository
    {
        private readonly BookingContext _context;

        public RawSqlRepository(BookingContext context)
        {
            _context = context;
        }

        public IQueryable<T> FromSql<T>(string sql, params object[] parameters) where T : class
        {
            return _context.Query<T>().FromSql(sql, parameters);
        }
    }
}
