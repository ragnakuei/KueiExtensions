using System.Collections.Generic;
using System.Linq;
using KueiExtensions.Microsoft.AspNetCore.Models;
using NUnit.Framework;

namespace KueiExtensionsTests.Microsoft.AspNetCore.PropertyInfoServiceTest;

public class GetValidationRuleTests
{
    [SetUp]
    public void Setup()
    {
    }

    protected Dictionary<string, Dictionary<string, object>> GetValidationRulesResult(Dictionary<string, PropertyInfoDto> properties)
    {
        var result = properties.ToDictionary(kv => kv.Key, kv => kv.Value.ValidationRules);
        return result;
    }
}
