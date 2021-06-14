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
    }
}
