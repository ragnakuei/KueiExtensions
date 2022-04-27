namespace KueiExtensions.Microsoft.AspNetCore.Services;

public interface IHashService
{
    string Hash(string salt, string text);
}