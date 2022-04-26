using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KueiExtensions.Microsoft.AspNetCore.Models;
using KueiExtensions.Microsoft.AspNetCore.Services;
using NUnit.Framework;

namespace KueiExtensionsTests.Microsoft.AspNetCore.PropertyInfoServiceTests;

public class GetValidationRuleNumberRangeTests : GetValidationRuleTests
{
    [SetUp]
    public void Setup()
    {
    }


    private class TestNumberMinMaxLengthDto
    {
        [global::System.ComponentModel.DataAnnotations.Range(10, 20)]
        public string Name { get; set; }
    }

    [Test]
    public void TestNumberMinMaxLength()
    {
        var target = new PropertyInfoService();

        var actual = GetValidationRulesResult(target.GetProperties(typeof(TestNumberMinMaxLengthDto)));

        var expected = new Dictionary<string, Dictionary<string, object>>
                       {
                           ["Name"] = new()
                                      {
                                          ["min"] = 10,
                                          ["max"] = 20,
                                      }
                       };

        actual.Should().BeEquivalentTo(expected);
    }

    protected Dictionary<string, Dictionary<string, object>> GetValidationRulesResult(Dictionary<string, PropertyInfoDto> properties)
    {
        var result = properties.ToDictionary(kv => kv.Key, kv => kv.Value.ValidationRules);
        return result;
    }
}
