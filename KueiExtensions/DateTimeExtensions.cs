using System;
using System.Collections.Generic;
using System.Linq;
using KueiExtensions.Models;

namespace KueiExtensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// source 排除 exceptPeriods
        /// <remarks>邊檢查邊回傳</remarks>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="exceptPeriods"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static IEnumerable<PeriodDateTimeDto> Except(this PeriodDateTimeDto         source,
                                                            IEnumerable<PeriodDateTimeDto> exceptPeriods)
        {
            // 先依照起始日期排序
            var exceptPeriodsByOrder = exceptPeriods?.OrderBy(p => p.Begin).ToArray()
                                    ?? new PeriodDateTimeDto[0];

            // 沒有排除項目，回傳 source
            if (exceptPeriodsByOrder.Length == 0)
            {
                return new[] { source };
            }

            if (exceptPeriodsByOrder.Length == 1)
            {
                return ExceptPeriodsCount1(source, exceptPeriodsByOrder);
            }


            // // 檢查第一個排除時間
            // var checkCurrentPeriod = exceptPeriodsByOrder[0];
            //
            // // begin 處理
            //
            // if (source.Begin > checkCurrentPeriod.Begin)
            // {
            //     throw new NotSupportedException("source 起始時間晚於排除起始時間");
            // }
            //
            // if (source.Begin == checkCurrentPeriod.Begin)
            // {
            //     begin = checkCurrentPeriod.End;
            //
            //     // end 處理
            //
            //     if (source.End < checkCurrentPeriod.End)
            //     {
            //         throw new NotSupportedException("source 結束時間早於排除結束時間");
            //     }
            //
            //     if (source.End == checkCurrentPeriod.End)
            //     {
            //         yield break;
            //     }
            //
            //     // if (source.End > checkCurrentPeriod.End)
            //     // {
            //     //     yield return new PeriodDateTimeDto(begin, checkCurrentPeriod.End);
            //     //     begin = checkCurrentPeriod.End;
            //     // }
            // }
            //
            // if (source.Begin < checkCurrentPeriod.Begin)
            // {
            //     yield return new PeriodDateTimeDto(source.Begin, checkCurrentPeriod.Begin);
            //     begin = checkCurrentPeriod.End;
            // }
            //
            // // 檢查 exceptPeriods 彼此是否重疊
            // for (int i = 1; i < exceptPeriodsByOrder.Length; i++)
            // {
            //     var checkNextPeriod = exceptPeriodsByOrder[1];
            //
            //     // begin 處理
            //
            //     if (checkCurrentPeriod.Begin > checkNextPeriod.Begin)
            //     {
            //         throw new NotSupportedException($"except[{i - 1}] 結束時間晚於 except[{i}]結束時間");
            //     }
            //
            //     if (checkCurrentPeriod.Begin == checkNextPeriod.Begin)
            //     {
            //         begin = checkCurrentPeriod.End;
            //     }
            //
            //     if (checkCurrentPeriod.Begin < checkNextPeriod.Begin)
            //     {
            //         yield return new PeriodDateTimeDto(begin, checkNextPeriod.Begin);
            //
            //         begin              = checkNextPeriod.End;
            //         checkCurrentPeriod = checkNextPeriod;
            //         continue;
            //     }
            //
            //     // end 處理
            //
            //     if (checkCurrentPeriod.End > checkNextPeriod.End)
            //     {
            //         throw new NotSupportedException($"except[{i - 1}] 結束時間晚於 except[{i}]結束時間");
            //     }
            //
            //     if (checkCurrentPeriod.End == checkNextPeriod.End)
            //     {
            //         yield break;
            //     }
            //
            //     if (checkCurrentPeriod.End < checkNextPeriod.End)
            //     {
            //         yield return new PeriodDateTimeDto(begin, checkNextPeriod.End);
            //         begin = checkCurrentPeriod.End;
            //     }
            //
            //     checkCurrentPeriod = checkNextPeriod;
            // }
            //
            // yield return new PeriodDateTimeDto(begin, source.End);

            return null;
        }

        private static IEnumerable<PeriodDateTimeDto> ExceptPeriodsCount1(PeriodDateTimeDto source, PeriodDateTimeDto[] exceptPeriodsByOrder)
        {
            // 檢查第一個排除時間
            var checkCurrentPeriod = exceptPeriodsByOrder[0];

            // 先建立回傳用的 Begin
            DateTime begin = source.Begin;

            // begin 處理
            if (source.Begin > checkCurrentPeriod.Begin)
            {
                throw new NotSupportedException("source 起始時間晚於排除起始時間");
            }

            if (source.Begin == checkCurrentPeriod.Begin)
            {
                begin = checkCurrentPeriod.End;

                // end 處理

                if (source.End < checkCurrentPeriod.End)
                {
                    throw new NotSupportedException("source 結束時間早於排除結束時間");
                }

                if (source.End == checkCurrentPeriod.End)
                {
                    yield break;
                }
            }

            if (source.Begin < checkCurrentPeriod.Begin)
            {
                yield return new PeriodDateTimeDto(source.Begin, checkCurrentPeriod.Begin);
            }
        }
    }
}
