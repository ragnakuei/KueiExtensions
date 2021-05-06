
## KueiExtensions

- IEnumerable\<T>
  - ToDataTable()
  - ForEach()
  - Aggregate()
  - ToPaged()
  - GroupByToDictionary()

- Decimal
  - ToFix()
  - ToFixAndFillTailZero()
  - Pow()
  - Sqrt()
  - StDev()

- Dictionary\<TKey?, TValue?>()
  - .GetValueOrNull()

- String
  - Join()
  - ToNullableDecimal()
  - ToDecimal()
  - Utf8Encode()
  - Utf8Decode()
  - ToNullableGuid()
  - ToNullableDateTime()
  - IsNullOrWhiteSpace()
  - ToNullableTimeSpan()
  - ToNullableLong()
  
## KueiExtensions.Dapper

- IDbConnection
  - MultipleResult().Read().Query()
  - MultipleResult\<T>().Read().Query()

- TypeHandler
  - VarcharToNullDecimalHandler

## KueiExtensions.System.Text.Json

- System.Text.Json
  - ToJson()
  - ParseJson\<T>()

## KueiExtensions.Microsoft.AspNetCore

- String
  - GetContentType()

- Controller
  - RenderViewAsync<T>()
