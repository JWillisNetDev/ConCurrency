using ConCurrency.Data.Dtos.Customers;

using FluentValidation;

namespace ConCurrency.Data.Validators;

public class CreateCustomerDtoValidator : BaseFluentValidator<CreateCustomerDto>
{
    public CreateCustomerDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(1, 200);
    }
}

public class UpdateCustomerDtoValidator : BaseFluentValidator<UpdateCustomerDto>
{
    public UpdateCustomerDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(1, 200);
    }
}