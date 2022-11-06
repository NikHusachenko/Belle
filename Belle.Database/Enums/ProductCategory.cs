using System.ComponentModel.DataAnnotations;

namespace Belle.Database.Enums
{
    public enum ProductCategory
    {
        [Display(Name = "Sportswear")]
        Sportswear = 1,

        [Display(Name = "Sport shoes")]
        SportShoes = 2,

        [Display(Name = "Sports food")]
        SportsFood = 3,

        [Display(Name = "Sport equipment")]
        SportEquipment = 4,
    }
}