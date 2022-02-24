using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using KueiExtensions.Microsoft.AspNetCore;
using KueiExtensions.Microsoft.AspNetCore.Models;
using NUnit.Framework;

namespace KueiExtensionsTests.Microsoft.AspNetCore.PropertyInfoServiceTest;

public class GetValidationRuleTests
{
    [SetUp]
    public void Setup()
    {
    }

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

    private class TestStringMaxLengthDto
    {
        [StringLength(10)]
        public string Name { get; set; }
    }

    [Test]
    public void TestStringMaxLength()
    {
        var target = new PropertyInfoService();

        var actual = GetValidationRulesResult(target.GetProperties(typeof(TestStringMaxLengthDto)));

        var expected = new Dictionary<string, Dictionary<string, object>>
                       {
                           ["Name"] = new()
                                      {
                                          ["maxLength"] = 10,
                                      }
                       };

        actual.Should().BeEquivalentTo(expected);
    }

    private class TestStringMaxMinLengthDto
    {
        [StringLength(10, MinimumLength = 3)]
        public string Name { get; set; }
    }

    [Test]
    public void TestStringMaxMinLength()
    {
        var target = new PropertyInfoService();

        var actual = GetValidationRulesResult(target.GetProperties(typeof(TestStringMaxMinLengthDto)));

        var expected = new Dictionary<string, Dictionary<string, object>>
                       {
                           ["Name"] = new()
                                      {
                                          ["maxLength"] = 10,
                                          ["minLength"] = 3,
                                      }
                       };

        actual.Should().BeEquivalentTo(expected);
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

    private Dictionary<string, Dictionary<string, object>> GetValidationRulesResult(Dictionary<string, PropertyInfoDto> properties)
    {
        var result = properties.ToDictionary(kv => kv.Key, kv => kv.Value.ValidationRules);
        return result;
    }
}
