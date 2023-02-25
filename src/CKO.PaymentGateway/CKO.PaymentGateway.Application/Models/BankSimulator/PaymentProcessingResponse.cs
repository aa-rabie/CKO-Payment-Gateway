namespace CKO.PaymentGateway.Application.Models.BankSimulator;

/// <summary>
/// stores the result of processing payment request by "Acquiring Bank Simulator"
/// </summary>
public class PaymentProcessingResponse
{
    /// <summary>
    /// True if payment request is processed successfully otherwise False
    /// </summary>
    public bool Success { get; init; }
    /// <summary>
    /// stores error message returned by "Acquiring Bank Simulator" if any
    /// </summary>
    public string Error { get; init; }

    public PaymentProcessingResponse()
    {
        Success = true;
    }

    public PaymentProcessingResponse(string error)
    {
        Success = false;
        Error = error;
    }
}