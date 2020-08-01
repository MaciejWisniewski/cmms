namespace CMMS.Domain.Identity
{
    public static class UserRole
    {
        public const string Admin = "Admin";
        public const string Leader = "Leader";
        public const string User = "User";
        public const string AdminOrLeader = Admin + "," + Leader;
        public const string Default = User;
    }
}
