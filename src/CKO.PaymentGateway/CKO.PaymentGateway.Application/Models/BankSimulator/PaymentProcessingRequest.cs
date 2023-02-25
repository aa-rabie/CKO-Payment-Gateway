namespace CKO.PaymentGateway.Application.Models.BankSimulator;

/// <summary>
/// stores data about Payment operation to be processed by "Acquiring Bank Simulator"  
/// </summary>
public class PaymentProcessingRequest
{
    public string CardNumber { get; set; }
    public string CardType { get; set; }
    public string Cvv { get; set; }
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public double Amount { get; set; }
    public string Currency { get; set; }
}