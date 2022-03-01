using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using KueiExtensions.Microsoft.AspNetCore;
using KueiExtensions.Microsoft.AspNetCore.Models;
using NUnit.Framework;

namespace KueiExtensionsTests.Microsoft.AspNetCore.PropertyInfoServiceTest;

public class GetValidationRuleRequiredTests : GetValidationRuleTests
{
    private class TestRequiredDto
    {
        [Required]
        public string Name { get; set; }
    }

    [Test]
    public void TestRequiredDto01()
    {
        var target = new PropertyInfoService();

        var actual = GetValidationRulesResult(target.GetProperties(typeof(TestRequiredDto)));

        var expected = new Dictionary<string, Dictionary<string, object>>
                       {
                           ["Name"] = new()
                                      {
                                          ["required"] = true,
                                      }
                       };

        actual.Should().BeEquivalentTo(expected);
    }
}
