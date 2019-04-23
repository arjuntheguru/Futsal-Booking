using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace goalza.booking.core.IRepository
{
    public interface IRawSqlRepository
    {
        IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class;
    }
}
