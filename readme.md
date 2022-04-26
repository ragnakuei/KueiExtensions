
## KueiExtensions

- IEnumerable\<T>
  - ToDataTable()
  - ForEach()
  - Aggregate()
  - ToPaged()
  - GroupByToDictionary()
  - ToHashSet45()
  - ExceptNoDistinct()
  - Filter()
  - PartialContains()
  - Contains()

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
  
- DataTable
  - Except()
  - MergeByExcept()
  - MergeByDistinct()
  - MergeColumns()
  - LeftJoin()
  - LeftJoinAppendRight()

- Type
  - IsDictionary()
  - IsDictionaryValueType()

## KueiExtensions.Dapper

- IDbConnection
  - QueryMultipleResult().Result\<T>()
  - QueryMultipleResult().Result()

- DynamicParameters
  - Add()

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
  - StringNullableDateTimeJsonConverter()

## KueiExtensions.Microsoft.AspNetCore

Extensions
- String.GetContentType()
- Controller.RenderViewAsync<T>()

Attribute
- PreventSqlInjectionAttribute

Services
- PropertyInfoService
- SqlInjectionValidateStringService

Validate
- BaseValidate

Exceptions
- ApiResponseException
- NotFoundException
- SqlInjectionValidateFailedException
- ValidateFormFailedException

## KueiQueueService

- QueueService
  - QueueActionAsync()

