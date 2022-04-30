namespace OnlineShopAPI.Options
{
    public static class ConfigurationOptions
    {
        public static string CorsPolicyName = "DefaultCorsPolicy";

        public static class Cloudinary
        {
            public const string Root = "Cloudinary";

            public const string CloudName = Root + ":CloudName";
            public const string ApiKey = Root + ":ApiKey";
            public const string ApiSecret = Root + ":ApiSecret";
        }
    }
}
