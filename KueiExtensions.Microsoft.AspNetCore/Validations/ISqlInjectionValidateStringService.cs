namespace KueiExtensions.Microsoft.AspNetCore.Validations;

public interface ISqlInjectionValidateStringService
{
    void Validate(string str);
}