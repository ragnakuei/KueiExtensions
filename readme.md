
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
  - ToNullableInt()
  
## KueiExtensions.Dapper

- IDbConnection
  - MultipleResult().Query\<T>()

- TypeHandler
  - VarcharToNullDecimalHandler

## KueiExtensions.EntityFrameworkCore

 - DbContext
   - QueryMultiple().Result\<T>()

## KueiExtensions.System.Text.Json

- System.Text.Json
  - ToJson()
  - ParseJson\<T>()
    
- System.Text.Json.JsonConverter
  - StringNullableTimeSpanJsonConverter()

## KueiExtensions.Microsoft.AspNetCore

- String
  - GetContentType()

- Controller
  - RenderViewAsync<T>()
