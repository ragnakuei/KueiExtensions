using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace KueiExtensions
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// 轉成 DataTable， T 的欄位順序必須要與 user defined table type 的欄位順序一致 !
        /// </summary>
        public static DataTable ToDataTable<T>(this IEnumerable<T> objs)
        {
            var dt = new DataTable();

            var propertyInfos = typeof(T).GetProperties()
                                         .Where(p => CustomAttributeExtensions.GetCustomAttribute<IgnoreDataTableAttribute>(p) == null);

            foreach (var p in propertyInfos)
            {
                var dc = new DataColumn(p.Name, Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType);
                dc.AllowDBNull = true;
                dt.Columns.Add(dc);
            }

            foreach (T entity in objs)
            {
                DataRow dr = dt.NewRow();

                foreach (PropertyInfo p in propertyInfos)
                {
                    var value = p.GetValue(entity);
                    dr[p.Name] = value ?? DBNull.Value;
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}
