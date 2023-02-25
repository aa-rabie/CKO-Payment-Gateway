using CKO.PaymentGateway.Application.Contracts.BankSimulator;
using CKO.PaymentGateway.Application.Models.BankSimulator;
using CKO.PaymentGateway.BankSimulator.Models;
using Microsoft.Extensions.Logging;

namespace CKO.PaymentGateway.BankSimulator;

/// <summary>
/// Responsible for processing incoming payment requests
/// Normally this service will initiate a request to external system; for example API call
/// Service can used to configure retries, use caching internally if needed
/// </summary>
internal class FakePaymentProcessingService : IPaymentProcessingService
{
    private readonly ILogger<FakePaymentProcessingService> _logger;
    private readonly List<CardEntry> _acceptedCardEntries;
    /// <summary>
    /// internal constructor for initializing fake data
    /// </summary>
    /// <param name="acceptedCardEntries"></param>
    /// <param name="logger"></param>
    public FakePaymentProcessingService(List<CardEntry> acceptedCardEntries, ILogger<FakePaymentProcessingService> logger)
    {
        _acceptedCardEntries = acceptedCardEntries ?? new List<CardEntry>();
        _logger = logger;
    }

    public Task<PaymentProcessingResponse> HandleAsync(PaymentProcessingRequest request)
    {
        try
        {
            request.CardType = NormalizeCardType(request.CardType);
            var entry = new CardEntry(request.CardNumber, request.Cvv, request.ExpiryMonth, request.ExpiryYear,
                request.CardType);

            var isValid = _acceptedCardEntries.Contains(entry);
            return Task.FromResult(!isValid ? InvalidCardResponse() : SuccessResponse());
        }
        catch (Exception ex)
        {
            var traceId = Guid.NewGuid();
            var errMsg = $"TraceId: {traceId}, exception thrown while processing payment request via Bank.";
            _logger.LogError(ex, errMsg);
            return Task.FromResult(RunTimeErrorResponse(traceId));
        }
    }

    private string NormalizeCardType(string requestCardType)
    {
        return requestCardType.Trim().ToUpper();
    }

    private static PaymentProcessingResponse InvalidCardResponse()
    {
        return new PaymentProcessingResponse( "Payment Rejected - Invalid Card Data" );
    }

    private static PaymentProcessingResponse RunTimeErrorResponse(Guid traceId)
    {
        return new PaymentProcessingResponse($"Payment Error - Please Contact Support - TraceId {traceId}" );
    }

    private static PaymentProcessingResponse SuccessResponse()
    {
        return new PaymentProcessingResponse();
    }
}