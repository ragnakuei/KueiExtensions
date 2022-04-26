namespace KueiExtensions.Microsoft.AspNetCore.Models;

public class ValidateFormFailedException : Exception
{
    public ValidateFormFailedException(string message = "")
        : base(message)
    {
    }

    public Dictionary<string, List<string>>? Errors { get; set; }
}