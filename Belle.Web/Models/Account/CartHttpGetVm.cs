using Belle.Database.Entities;

namespace Belle.Web.Models.Account
{
    public class CartHttpGetVm
    {
        public List<ProductEntity> Products { get; set; }
    }
}