namespace IdentityServer
{
    public static class Roles
    {
        public const string Admin = "admin";
        public const string Moder = "moderator";
        public const string Seller = "seller";
        public const string Buyer = "buyer";

        public const string AdminOrModer = Admin + "," + Moder;

        public const string AdminOrModerOrSeller = AdminOrModer + "," + Seller;
    }
}
