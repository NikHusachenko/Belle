using Belle.Database.Enums;

namespace Belle.Services.UserServices.Helpers
{
    public class UserTypeHelper
    {
        public static UserType? GetTypeAsEnum(string type)
        {
            switch (type.ToLower())
            {
                case "admin":
                    {
                        return UserType.Admin;
                    }
                case "client":
                    {
                        return UserType.Client;
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}