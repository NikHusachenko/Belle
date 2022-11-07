using System.ComponentModel.DataAnnotations;

namespace Belle.Database.Enums
{
    public enum UserType
    {
        [Display(Name = "Admin")]
        Admin = 1,

        [Display(Name = "Client")]
        Client = 2,
    }
}