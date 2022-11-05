using Belle.Database.Enums;

namespace Belle.Services.CurrentUser
{
    public interface ICurrentUserContext
    {
        public long? Id { get; }
        public string? Email { get; }
        public string? Login { get; }
        public UserType? Type { get; }
    }
}