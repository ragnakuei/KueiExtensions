using System;
using System.Collections.Generic;
using System.Linq;

namespace KueiExtensions
{
    public static class IEnumerableOfTExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
            {
                return;
            }

            foreach (T obj in source)
            {
                action.Invoke(obj);
            }
        }

        public static void ForEach<TElement>(this IEnumerable<TElement> source, Action<TElement, int> action)
        {
            int index = 0;

            foreach (var item in source)
            {
                action.Invoke(item, index);
                index++;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="func">第三個引數是 Index，從 1 開始執行</param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static TSource Aggregate<TSource>(this IEnumerable<TSource>            source,
                                                 Func<TSource, TSource, int, TSource> func)
        {
            var index = 1;

            var result = source.Aggregate((seed, item) =>
                                          {
                                              seed = func.Invoke(seed, item, index);
                                              index++;
                                              return seed;
                                          });

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="seed"></param>
        /// <param name="func">第三個引數是 Index，從 0 開始執行</param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TAccumulate"></typeparam>
        /// <returns></returns>
        public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource>                    source,
                                                                  TAccumulate                                  seed,
                                                                  Func<TAccumulate, TSource, int, TAccumulate> func)
        {
            var index = 0;

            var result = source.Aggregate(seed: seed,
                                          func: (seed, item) =>
                                                {
                                                    seed = func.Invoke(seed, item, index);
                                                    index++;
                                                    return seed;
                                                });

            return result;
        }

        public static TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource>                    source,
                                                                       TAccumulate                                  seed,
                                                                       Func<TAccumulate, TSource, int, TAccumulate> func,
                                                                       Func<TAccumulate, TResult>                   resultSelector)
        {
            var index = 0;

            var result = source.Aggregate(seed: seed,
                                          func: (seed, item) =>
                                                {
                                                    seed = func.Invoke(seed, item, index);
                                                    index++;
                                                    return seed;
                                                },
                                          resultSelector: seed => resultSelector.Invoke(seed));

            return result;
        }

        /// <summary>
        /// 將集合資料分頁
        /// </summary>
        public static IEnumerable<IEnumerable<T>> ToPaged<T>(this IEnumerable<T> source, int pageSize = 99)
        {
            if (source == null)
            {
                return Enumerable.Empty<IEnumerable<T>>();
            }

            return source.Select((v, i) => new
                                           {
                                               index = i,
                                               value = v
                                           })
                         .GroupBy(a => a.index / pageSize)
                         .Select(d => d.Select(d2 => d2.value));
        }

        /// <summary>
        /// GroupBy + ToDictionary
        /// </summary>
        public static Dictionary<TKey, List<TElement>> GroupByToDictionary<TKey, TElement>(this IEnumerable<TElement> source, Func<TElement, TKey> keySelector)
        {
            var result = new Dictionary<TKey, List<TElement>>();

            foreach (var element in source)
            {
                if (keySelector.Invoke(element) is not TKey elementKey)
                {
                    continue;
                }

                if (result.TryGetValue(elementKey, out var values))
                {
                    values.Add(element);
                }
                else
                {
                    result.Add(elementKey, new List<TElement> { element });
                }
            }

            return result;
        }

        /// <summary>
        /// GroupBy + ToDictionary
        /// </summary>
        public static Dictionary<TKey, List<TElement>> LinqGroupByToDictionary<TKey, TElement>(this IEnumerable<TElement> source, Func<TElement, TKey> keySelector)
        {
            return source.Where(s => keySelector.Invoke(s) != null)
                         .GroupBy(keySelector)
                         .ToDictionary(kv => kv.Key,
                                       kv => kv.ToList());
        }
    }
}
