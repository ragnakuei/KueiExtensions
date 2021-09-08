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

            var begin = source.Begin;

            for (int i = 0; i < exceptPeriodsByOrder.Length; i++)
            {
                var exceptPeriod = exceptPeriodsByOrder[i];

                if (begin < exceptPeriod.Begin)
                {
                    yield return new PeriodDateTimeDto(begin, exceptPeriod.Begin);
                    begin = exceptPeriod.End;
                }
                else if (begin == exceptPeriod.Begin)
                {
                    begin = exceptPeriod.End;
                }
                else
                {
                    throw new NotSupportedException("起始時間晚於排除起始時間");
                }
            }

            if (begin < source.End)
            {
                yield return new PeriodDateTimeDto(begin, source.End);
            }
            else if (begin > source.End)
            {
                throw new NotSupportedException("結束時間早於於排除起始時間");
            }
        }
    }
}
