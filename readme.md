
## KueiExtensions

- IEnumerable\<T>
  - ToDataTable()
  - ForEach()
  - Aggregate()
  - ToPaged()
  - GroupByToDictionary()
  - ToHashSet45()
  - ExceptNoDistinct()

- Decimal
  - ToFix()
  - ToFixAndFillTailZero()
  - Pow()
  - Sqrt()
  - StDev()

- Dictionary\<TKey?, TValue?>()
  - GetValueOrNull()

- ConcurrentDictionary\<TKey?, TValue?>()
  - GetValueOrDefault()

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
  - TrimStart()
  - TrimEnd()
  
## KueiExtensions.Dapper

- IDbConnection
  - QueryMultipleResult().Result\<T>()
  - QueryMultipleResult().Result()

- TypeHandler
  - VarcharToNullDecimalHandler

## KueiExtensions.EntityFrameworkCore

 - DbContext
   - QueryMultiple().Result\<T>()
   - QueryMultiple().Result()

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
