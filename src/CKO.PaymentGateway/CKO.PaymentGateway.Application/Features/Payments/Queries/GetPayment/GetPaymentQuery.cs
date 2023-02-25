using MediatR;

namespace CKO.PaymentGateway.Application.Features.Payments.Queries.GetPayment;

/// <summary>
/// represents request to retrieve payment operation from data store
/// </summary>
public class GetPaymentQuery : IRequest<PaymentDto>
{
    /// <summary>
    /// Payment Id to be retrieved
    /// </summary>
    public Guid Id { get; set; }
}