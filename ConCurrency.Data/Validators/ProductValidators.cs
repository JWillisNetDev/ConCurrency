using ConCurrency.Data.Dtos.Products;

using FluentValidation;

namespace ConCurrency.Data.Validators;

public class CreateProductDtoValidator : BaseFluentValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(1, 200);
        RuleFor(x => x.Description).NotEmpty().Length(1, 2000);
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0.0);
    }
}

public class UpdateProductDtoValidator : BaseFluentValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(1, 200);
        RuleFor(x => x.Description).NotEmpty().Length(1, 2000);
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0.0);
    }
}