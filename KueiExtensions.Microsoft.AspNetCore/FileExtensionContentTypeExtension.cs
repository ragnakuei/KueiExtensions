using Microsoft.AspNetCore.StaticFiles;

namespace KueiExtensions.Microsoft.AspNetCore
{
    public static class FileExtensionContentTypeExtension
    {
        public static string GetContentType(this string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            provider.TryGetContentType(fileName, out var contentType);
            return contentType;
        }
    }
}
