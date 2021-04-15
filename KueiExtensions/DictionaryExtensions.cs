using System.Collections.Generic;

namespace KueiExtensions
{
    public static class DictionaryExtensions
    {
        public static TValue? GetValueOrNull<TKey, TValue>(this Dictionary<TKey?, TValue?> dict, TKey? key)
            where TKey : struct
            where TValue : struct
        {
            if (dict                        == null
             || key.GetValueDefaultOrNull() == null)
            {
                return null;
            }

            return dict?.GetValueOrDefault(key);
        }

        public static TValue? GetValueOrNull<TKey, TValue>(this Dictionary<TKey, TValue?> dict, TKey key)
            where TKey : class
            where TValue : struct
        {
            if (dict == null
             || key  == null)
            {
                return null;
            }

            return dict?.GetValueOrDefault(key);
        }

        public static TValue GetValueOrNull<TKey, TValue>(this Dictionary<TKey?, TValue> dict, TKey? key)
            where TKey : struct
            where TValue : class
        {
            if (dict                        == null
             || key.GetValueDefaultOrNull() == null)
            {
                return null;
            }

            return dict?.GetValueOrDefault(key);
        }

        public static TValue GetValueOrNull<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
            where TKey : class
            where TValue : class
        {
            if (dict == null
             || key  == null)
            {
                return null;
            }

            return dict?.GetValueOrDefault(key);
        }
    }
}
