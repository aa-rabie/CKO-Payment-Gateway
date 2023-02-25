using CKO.PaymentGateway.Application.Contracts.Persistence;
using FluentValidation;

namespace CKO.PaymentGateway.Application.Features.Payments.Commands.CreatePayment;

/// <summary>
/// Responsible for validating 'CreatePaymentCommand' class
/// </summary>
public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    private readonly IMerchantRepository _merchantRepository;
    public CreatePaymentCommandValidator(IMerchantRepository merchantRepository)
    {
        _merchantRepository = merchantRepository;

        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(p => p.MerchantId)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required.");
            

        RuleFor(p => p.CardNumber)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(13)
            .Must((cn) =>
            {
                return !string.IsNullOrEmpty(cn) && cn.All(c => char.IsDigit(c) || c == '-');
            }).WithMessage("{PropertyName} format is invalid.");

        RuleFor(p => p.CardType)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required.");
            

        RuleFor(p => p.Cvv)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(3).WithMessage("{PropertyName} should be 3 characters.");

        RuleFor(x => x.ExpiryYear)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThanOrEqualTo(DateTime.Now.Year)
            .DependentRules(YearDependentRules);

       

        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(0);

        //TODO: SHOULD be validated against supported list of currencies
        RuleFor(p => p.Currency)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(e => e.MerchantId)
            .MustAsync(MerchantExistsAsync)
            .WithMessage("{PropertyValue} is not valid Merchant Id");
    }

    private async Task<bool> MerchantExistsAsync(Guid merchantId, CancellationToken token)
    {
        return await _merchantRepository.GetByIdAsync(merchantId) != null;
    }


    /// <summary>
    /// run only when ExpiryYear basic checks are successful
    /// validates that ExpiryMonth is between 1 , 12
    /// </summary>
    private void YearDependentRules()
    {
        RuleFor(x => x.ExpiryMonth)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .InclusiveBetween(1, 12)
            .DependentRules(MonthDependentRules);
    }

    /// <summary>
    /// validate that expiry date > today
    /// this rule should be checked only if ExpiryMonth is valid
    /// </summary>
    private void MonthDependentRules()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        RuleFor(m => m)
            .Must(x => 
                new DateOnly(x.ExpiryYear, x.ExpiryMonth, 1) > today)
            .WithMessage($" {nameof(CreatePaymentCommand.ExpiryMonth)} and {nameof(CreatePaymentCommand.ExpiryYear)} should represent a date in the future");
    }

}