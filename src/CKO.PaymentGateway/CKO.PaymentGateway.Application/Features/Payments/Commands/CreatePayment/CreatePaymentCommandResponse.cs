using CKO.PaymentGateway.Application.Responses;

namespace CKO.PaymentGateway.Application.Features.Payments.Commands.CreatePayment;

/// <summary>
/// stores the result of 'CreatePaymentCommand' processing operation.
/// It store Payment-Id in data store
/// It provides validation errors if any
/// </summary>
public class CreatePaymentCommandResponse : BaseResponse
{
    public CreatePaymentCommandResponse() {}
    public CreatePaymentCommandResponse(List<string> validationErrors) : base(validationErrors)
    {
    }
    public CreatePaymentCommandResponse(string error): base(error)
    {
    }

    public string PaymentId { get; set; } = string.Empty;
}