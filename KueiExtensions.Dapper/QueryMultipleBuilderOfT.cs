using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace KueiExtensions.Dapper
{
    public class QueryMultipleBuilder<T> where T : class, new()
    {
        private readonly QueryMultipleHandler<T> _handler;

        private readonly List<Action<T, SqlMapper.GridReader>> _readerActions = new();

        internal QueryMultipleBuilder(IDbConnection dbConnection, string sql, object param)
        {
            _handler = new QueryMultipleHandler<T>(dbConnection, sql, param);
        }

        public QueryMultipleBuilder<T> Read(Action<T, SqlMapper.GridReader> readerAction)
        {
            _readerActions.Add(readerAction);

            return this;
        }

        public T Query()
        {
            return _handler.Query(_readerActions);
        }
    }

    public class QueryMultipleBuilderWithFunc<T> where T : class, new()
    {
        private readonly QueryMultipleHandler<T> _handler;

        public delegate void ReaderAction(ref T t, SqlMapper.GridReader reader);

        private readonly List<ReaderAction> _readerActions = new();

        internal QueryMultipleBuilderWithFunc(IDbConnection dbConnection, string sql, object param)
        {
            _handler = new QueryMultipleHandler<T>(dbConnection, sql, param);
        }

        public QueryMultipleBuilderWithFunc<T> Read(ReaderAction readerAction)
        {
            _readerActions.Add(readerAction);

            return this;
        }

        public T Query()
        {
            return _handler.Query(_readerActions);
        }
    }
}
