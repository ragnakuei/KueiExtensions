namespace KueiExtensions
{
    public static class StructExtensions
    {
        public static T? GetValueDefaultOrNull<T>(this T? t) where T : struct
        {
            if (t == null)
            {
                return null;
            }

            return t;
        }
    }
}
