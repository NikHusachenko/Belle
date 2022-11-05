using Belle.Database.Enums;

namespace Belle.Database.Entities
{
    public class UserEntity
    {
        public UserEntity()
        {
            Products = new List<ProductEntity>();
        }

        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public double WalletBalance { get; set; }
        public UserType Type { get; set; }

        public ICollection<ProductEntity> Products { get; set; } // Cart
    }
}