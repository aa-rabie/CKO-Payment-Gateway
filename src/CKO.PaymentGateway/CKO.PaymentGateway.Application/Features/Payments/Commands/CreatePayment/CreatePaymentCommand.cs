using MediatR;

namespace CKO.PaymentGateway.Application.Features.Payments.Commands.CreatePayment;

/// <summary>
/// Represents incoming request to complete a payment from a merchant
/// using specific card data
/// </summary>
public class CreatePaymentCommand : IRequest<CreatePaymentCommandResponse>
{
    /// <summary>
    /// Valid Merchant Id in the system - Required
    /// </summary>
    public Guid MerchantId { get; set; }
    /// <summary>
    /// Card Number to be used to complete payment process - Required
    /// </summary>
    public string CardNumber { get; set; }
    /// <summary>
    /// Card Type which should be supported by system- for example "VISA" or "MASTERCARD" - Required 
    /// </summary>
    public string CardType { get; set; }
    /// <summary>
    /// Card Cvv - Required
    /// </summary>
    public string Cvv { get; set; }
    /// <summary>
    /// Card Expiry Month - Required
    /// </summary>
    public int ExpiryMonth { get; set; }
    /// <summary>
    /// Card Expiry year - Required
    /// </summary>
    public int ExpiryYear { get; set; }
    /// <summary>
    /// Amount to Pay - should be non-negative number - Required
    /// </summary>
    public double Amount { get; set; }
    /// <summary>
    /// Currency - Example "GBP" or "EURO" - Required
    /// </summary>
    public string Currency { get; set; }
}