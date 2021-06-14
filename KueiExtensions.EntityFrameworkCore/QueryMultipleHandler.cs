using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace KueiExtensions.EntityFrameworkCore
{
    internal class QueryMultipleHandler
    {
        private readonly DbContext      _dbContext;
        private readonly string         _sql;
        private readonly SqlParameter[] _parameters;
        private readonly CommandType    _sqlCommandType;

        internal QueryMultipleHandler(DbContext      dbContext,
                                      string         sql,
                                      SqlParameter[] parameters     = null,
                                      CommandType    sqlCommandType = CommandType.Text)
        {
            _dbContext      = dbContext;
            _sql            = sql;
            _parameters     = parameters ?? Array.Empty<SqlParameter>();
            _sqlCommandType = sqlCommandType;
        }

        internal TResult Result<TResult>(Func<DbDataReader, TResult> readerFunc)
        {
            var dbConnection = _dbContext.Database.GetDbConnection();
            var sqlCommand   = dbConnection.CreateCommand();
            sqlCommand.CommandType = _sqlCommandType;
            sqlCommand.CommandText = _sql;

            foreach (var parameter in _parameters)
            {
                sqlCommand.Parameters.Add(parameter);
            }

            try
            {
                dbConnection.Open();
                using (var reader = sqlCommand.ExecuteReader())
                {
                    return readerFunc.Invoke(reader);
                }
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
