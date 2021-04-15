using System;
using System.Text.Json;

namespace KueiExtensions.System.Text.Json
{
    public static class JsonExtensions
    {
        public static string ToJson(this object obj, JsonSerializerOptions jsonSerializerOptions = null)
        {
            return JsonSerializer.Serialize(obj, jsonSerializerOptions);
        }
    }
}
