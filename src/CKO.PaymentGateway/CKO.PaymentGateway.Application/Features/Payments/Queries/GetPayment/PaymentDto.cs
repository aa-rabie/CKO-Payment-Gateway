namespace CKO.PaymentGateway.Application.Features.Payments.Queries.GetPayment;

/// <summary>
/// represents Payment operation (sensitive card info is masked)
/// </summary>
public class PaymentDto
{
    /// <summary>
    /// Payment PK in data store
    /// </summary>
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public Guid MerchantId { get; set; }
    /// <summary>
    /// should be masked except last 4 digits
    /// </summary>
    public string CardNumber { get; set; }
    public string CardType { get; set; }
    /// <summary>
    /// should be masked
    /// </summary>
    public string Cvv { get; set; }
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public double Amount { get; set; }
    public string Currency { get; set; }
    /// <summary>
    /// true if payment operation is successful otherwise false
    /// </summary>
    public bool Succeeded { get; set; }
}