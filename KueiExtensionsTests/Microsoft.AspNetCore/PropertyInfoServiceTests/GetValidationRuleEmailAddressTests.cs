using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using KueiExtensions.Microsoft.AspNetCore.Services;
using NUnit.Framework;

namespace KueiExtensionsTests.Microsoft.AspNetCore.PropertyInfoServiceTests;

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
