
## KueiExtensions

- IEnumerable\<T>
  - ToDataTable()
  - ForEach()
  - Aggregate()
  - ToPaged()

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
  
## KueiExtensions.Dapper

- IDbConnection
  - MultipleResult().Read().Query()
  - MultipleResult\<T>().Read().Query()

## KueiExtensions.System.Text.Json

- System.Text.Json
  - ToJson()

