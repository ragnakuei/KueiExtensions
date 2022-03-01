using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using KueiExtensions.Microsoft.AspNetCore;
using KueiExtensions.Microsoft.AspNetCore.Models;
using NUnit.Framework;

namespace KueiExtensionsTests.Microsoft.AspNetCore.PropertyInfoServiceTest;

public class GetValidationRuleEmailAddressTests : GetValidationRuleTests
{
    [SetUp]
    public void Setup()
    {
    }

    private class TestDto
    {
        [EmailAddress]
        public string Name { get; set; }
    }

    [Test]
    public void TestNumberMinMaxLength()
    {
        var target = new PropertyInfoService();

        var actual = GetValidationRulesResult(target.GetProperties(typeof(TestDto)));

        var expected = new Dictionary<string, Dictionary<string, object>>
                       {
                           ["Name"] = new()
                                      {
                                          ["email"] = true,
                                      }
                       };

        actual.Should().BeEquivalentTo(expected);
    }
}
