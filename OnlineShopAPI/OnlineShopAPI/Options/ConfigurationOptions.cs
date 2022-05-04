namespace OnlineShopAPI.Options
{
    public static class ConfigurationOptions
    {
        public const string CorsPolicyName = "DefaultCorsPolicy";
        public const string Token = "TokenKey";

        public static class Cloudinary
        {
            public const string Root = "Cloudinary";

            public const string CloudName = Root + ":CloudName";
            public const string ApiKey = Root + ":ApiKey";
            public const string ApiSecret = Root + ":ApiSecret";
        }
    }
}
