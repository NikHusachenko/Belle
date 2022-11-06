using System.ComponentModel.DataAnnotations;

namespace Belle.Database.Enums
{
    public enum ProductSize
    {
        [Display(Name = "XXS")]
        XXS = 1,

        [Display(Name = "XS")]
        XS = 2,

        [Display(Name = "S")]
        S = 3,

        [Display(Name = "M")]
        M = 4,

        [Display(Name = "L")]
        L = 5,

        [Display(Name = "XL")]
        XL = 6,

        [Display(Name = "XXL")]
        XXL = 7,

        [Display(Name = "XXXL")]
        XXXL,
    }
}