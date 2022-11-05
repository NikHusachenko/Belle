namespace Belle.Database.Entities
{
    public class ProductEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public string PathToImage { get; set; }

        public long UserFK { get; set; }
        public UserEntity UserEntity { get; set; } // buyer
    }
}