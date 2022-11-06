using Belle.Database.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Belle.Services.ProductServices.Models
{
    public class CreateProductViewModel
    {
        public IFormFile Photo { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public ProductCategory Category { get; set; }
        public ProductSize Size { get; set; }
        public int Count { get; set; }
        public string Details { get; set; }
    }

    public class CreateProductViewModelValidator : AbstractValidator<CreateProductViewModel>
    {
        public CreateProductViewModelValidator()
        {
            RuleFor(x => x.Photo)
                .NotNull();

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .Length(3, 63);

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .Length(3, 255);

            RuleFor(x => x.Details)
                .NotEmpty()
                .NotNull()
                .Length(3, 511);
        }
    }
}