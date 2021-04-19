using System;
using KueiExtensions;
using NUnit.Framework;

namespace KueiExtensionsTests.Common.StringExtensions
{
    public class ToNullableDateTimeTests
    {
        [Test]
        public void 符合()
        {
            var actual = "2010/01/02".ToNullableDateTime("yyyy/MM/dd");

            var expected = new DateTime(2010, 01, 02);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 不符合()
        {
            var actual = "2010/13/02".ToNullableDateTime("yyyy/MM/dd");

            DateTime? expected = null;

            Assert.AreEqual(expected, actual);
        }
    }
}
