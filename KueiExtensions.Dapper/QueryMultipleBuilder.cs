using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace KueiExtensions.Dapper
{
    public class QueryMultipleBuilder
    {
        private readonly QueryMultipleHandler<object> _handler;

        private readonly List<Action<object, SqlMapper.GridReader>> _readerActions = new();

        internal QueryMultipleBuilder(IDbConnection dbConnection, string sql, object param)
        {
            _handler = new QueryMultipleHandler<object>(dbConnection, sql, param);
        }

        public QueryMultipleBuilder Read(Action<SqlMapper.GridReader> readerAction)
        {
            Action<object, SqlMapper.GridReader> newReaderAction = (_, reader) => readerAction.Invoke(reader);

            _readerActions.Add(newReaderAction);

            return this;
        }

        public void Query()
        {
            _handler.Query(_readerActions);
        }
    }
}
