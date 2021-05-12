using System.Data;

namespace KueiExtensions.Dapper
{
    public static class DapperExtension
    {
        public static QueryMultipleBuilder MultipleResult(this IDbConnection dbConnection,
                                                          string             sql,
                                                          object             param = null)
        {
            return new(dbConnection, sql, param);
        }

        public static QueryMultipleBuilderWithFunc<T> MultipleResult<T>(this IDbConnection dbConnection,
                                                                        string             sql,
                                                                        object             param = null) where T : class, new()
        {
            return new(dbConnection, sql, param);
        }
    }
}
