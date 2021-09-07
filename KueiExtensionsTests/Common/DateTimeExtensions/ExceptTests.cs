using System;
using System.Linq;
using FluentAssertions;
using KueiExtensions;
using KueiExtensions.Models;
using NUnit.Framework;

namespace KueiExtensionsTests.Common.DateTimeExtensions
{
    public class ExceptTests
    {
        [Test]
        public void 沒有排除時間()
        {
            var sourcePeriod = new PeriodDateTimeDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                                     new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new PeriodDateTimeDto[0];

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new PeriodDateTimeDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                                     new DateTime(2020, 2, 3, 17, 0, 0)),
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 一個排除時間_起始時間相同()
        {
            var sourcePeriod = new PeriodDateTimeDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                                     new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new PeriodDateTimeDto(new DateTime(2020, 2, 3, 8, 0, 0),
                                                          new DateTime(2020, 2, 3, 9, 5, 0)),
                                };

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new PeriodDateTimeDto(new DateTime(2020, 2, 3, 9,  5, 0),
                                                     new DateTime(2020, 2, 3, 17, 0, 0)),
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 一個排除時間_在中間()
        {
            var sourcePeriod = new PeriodDateTimeDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                                     new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new PeriodDateTimeDto(new DateTime(2020, 2, 3, 8, 5, 0),
                                                          new DateTime(2020, 2, 3, 9, 5, 0)),
                                };

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new PeriodDateTimeDto(new DateTime(2020, 2, 3, 8, 0, 0),
                                                     new DateTime(2020, 2, 3, 8, 5, 0)),
                               new PeriodDateTimeDto(new DateTime(2020, 2, 3, 9,  5, 0),
                                                     new DateTime(2020, 2, 3, 17, 0, 0)),
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 一個排除時間_結束時間相同()
        {
            var sourcePeriod = new PeriodDateTimeDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                                     new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new PeriodDateTimeDto(new DateTime(2020, 2, 3, 8,  5, 0),
                                                          new DateTime(2020, 2, 3, 17, 0, 0)),
                                };

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new PeriodDateTimeDto(new DateTime(2020, 2, 3, 8, 0, 0),
                                                     new DateTime(2020, 2, 3, 8, 5, 0)),
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 二個排除時間_在中間()
        {
            var sourcePeriod = new PeriodDateTimeDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                                     new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new PeriodDateTimeDto(new DateTime(2020, 2, 3, 8, 5, 0),
                                                          new DateTime(2020, 2, 3, 9, 5, 0)),
                                    new PeriodDateTimeDto(new DateTime(2020, 2, 3, 10, 10, 0),
                                                          new DateTime(2020, 2, 3, 11, 10, 0)),
                                };

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new PeriodDateTimeDto(new DateTime(2020, 2, 3, 8, 0, 0),
                                                     new DateTime(2020, 2, 3, 8, 5, 0)),
                               new PeriodDateTimeDto(new DateTime(2020, 2, 3, 9,  5,  0),
                                                     new DateTime(2020, 2, 3, 10, 10, 0)),
                               new PeriodDateTimeDto(new DateTime(2020, 2, 3, 11, 10, 0),
                                                     new DateTime(2020, 2, 3, 17, 0,  0)),
                           };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
