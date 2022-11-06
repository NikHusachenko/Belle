using Belle.Database.Enums;

namespace Belle.Web.Models.Product
{
    public class ProductDetailsHttpGetVm
    {
        public string PathToPhoto { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        public ProductCategory Category { get; set; }
        public ProductSize Size { get; set; }
        public int Count { get; set; }
    }
}