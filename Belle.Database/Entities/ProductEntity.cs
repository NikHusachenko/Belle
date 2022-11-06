using Belle.Database.Enums;

namespace Belle.Database.Entities
{
    public class ProductEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public ProductCategory Category { get; set; }
        public ProductSize Size { get; set; }
        public string? PathToImage { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        public long? UserFK { get; set; }
        public UserEntity UserEntity { get; set; } // buyer
    }
}