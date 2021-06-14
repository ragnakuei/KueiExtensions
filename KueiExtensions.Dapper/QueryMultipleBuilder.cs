using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace KueiExtensions.Dapper
{
    public class QueryMultipleBuilder
    {
        private readonly QueryMultipleHandler _handler;

        internal QueryMultipleBuilder(IDbConnection dbConnection, string sql, object param)
        {
            _handler = new QueryMultipleHandler(dbConnection, sql, param);
        }

        public T Query<T>(Func<SqlMapper.GridReader, T> readerFunc)
        {
            return _handler.Query(readerFunc);
        }
    }
}
