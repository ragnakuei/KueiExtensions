using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KueiExtensions;
using NUnit.Framework;

namespace KueiExtensionsTests.Common.IEnumerableOfTExtensions
{
    public class ForEachWithIndexTests
    {
        private class Test
        {
            public int Count { get; set; }

            public int Index { get; set; }
        }

        [Test]
        public void Case01()
        {
            var actual = new List<Test>();

            Enumerable.Range(1, 2)
                      .ForEach((v, i) => actual.Add(new Test { Count = v, Index = i }));

            var expected = new List<Test>()
                           {
                               new() { Count = 1, Index = 0 },
                               new() { Count = 2, Index = 1 },
                           };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
