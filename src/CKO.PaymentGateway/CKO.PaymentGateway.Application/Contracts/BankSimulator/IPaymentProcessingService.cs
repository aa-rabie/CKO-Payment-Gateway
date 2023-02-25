using CKO.PaymentGateway.Application.Models.BankSimulator;

namespace CKO.PaymentGateway.Application.Contracts.BankSimulator;

public interface IPaymentProcessingService
{
    /// <summary>
    /// Handles payment request using specified card info and verifies card validity
    /// It will return error msg if card data is not valid
    /// </summary>
    /// <param name="request">store both requested payment info and card info</param>
    /// <returns>Successful response or response with error message</returns>
    Task<PaymentProcessingResponse> HandleAsync(PaymentProcessingRequest request);
}