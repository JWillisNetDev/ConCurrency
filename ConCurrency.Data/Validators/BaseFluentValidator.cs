using FluentValidation;

namespace ConCurrency.Data.Validators;

public class BaseFluentValidator<TModel> : AbstractValidator<TModel>
{
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<TModel>.CreateWithOptions((TModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid) { return []; }
        return result.Errors.Select(x => x.ErrorMessage);
    };
}
