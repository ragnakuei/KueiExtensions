using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace KueiExtensions.Dapper
{
    internal class QueryMultipleHandler<T> where T : class, new()
    {
        private IDbConnection _dbConnection;
        private string        _sql;
        private object        _param;

        internal QueryMultipleHandler(IDbConnection dbConnection, string sql, object param)
        {
            _dbConnection = dbConnection;
            _sql          = sql;
            _param        = param;
        }

        public T Query(List<Action<T, SqlMapper.GridReader>> readerActions)
        {
            var result = new T();

            using (_dbConnection)
            {
                var reader = _dbConnection.QueryMultiple(_sql, _param);

                foreach (var readerAction in readerActions)
                {
                    readerAction.Invoke(result, reader);
                }
            }

            return result;
        }

        public T Query(List<QueryMultipleBuilderWithFunc<T>.ReaderAction> readerActions)
        {
            var result = new T();

            using (_dbConnection)
            {
                var reader = _dbConnection.QueryMultiple(_sql, _param);

                foreach (var readerAction in readerActions)
                {
                    readerAction(ref result, reader);
                }
            }

            return result;
        }

        public T Query(List<Func<T, SqlMapper.GridReader, T>> readerActions)
        {
            var result = new T();

            using (_dbConnection)
            {
                var reader = _dbConnection.QueryMultiple(_sql, _param);

                foreach (var readerAction in readerActions)
                {
                    result = readerAction.Invoke(result, reader);
                }
            }

            return result;
        }
    }
}
