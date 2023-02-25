using AutoMapper;
using CKO.PaymentGateway.Application.Contracts;
using CKO.PaymentGateway.Application.Contracts.BankSimulator;
using CKO.PaymentGateway.Application.Contracts.Persistence;
using CKO.PaymentGateway.Application.Exceptions;
using CKO.PaymentGateway.Application.Models.BankSimulator;
using CKO.PaymentGateway.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CKO.PaymentGateway.Application.Features.Payments.Commands.CreatePayment;

/// <summary>
/// Responsible for processing Incoming Payment Request.
/// It stores Payment Request and its processing result in data store
/// Returns Successful Response with {Payment-Id}
/// OR unsuccessful Response with validation errors and {Payment-Id}
/// </summary>
public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentCommandResponse>
{
    private readonly IRepository<Payment> _paymentRepository;
    private readonly IMerchantRepository _merchantRepository;
    private readonly IPaymentProcessingService _paymentProcessingService;
    private readonly IMapper _mapper;
    private readonly ILogger<CreatePaymentCommandHandler> _logger;


    public CreatePaymentCommandHandler(IMapper mapper,
        IRepository<Payment> paymentRepository,
        IMerchantRepository merchantRepository,
        IPaymentProcessingService paymentProcessingService,
        IDataMaskingService dataMaskingService,
        ILogger<CreatePaymentCommandHandler> logger)
    {
        _mapper = mapper;
        _paymentRepository = paymentRepository;
        _merchantRepository = merchantRepository;
        _paymentProcessingService = paymentProcessingService;
        _logger = logger;
    }

    public async Task<CreatePaymentCommandResponse> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            CreatePaymentCommandResponse response = new();

            var validator = new CreatePaymentCommandValidator(_merchantRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Count > 0)
            {
                return new CreatePaymentCommandResponse(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            var paymentProcessingRequest = _mapper.Map<PaymentProcessingRequest>(request);

            var processingResponse = await _paymentProcessingService.HandleAsync(paymentProcessingRequest);

            var paymentEntry = _mapper.Map<Payment>(request);

            paymentEntry.Id = Guid.NewGuid();
            paymentEntry.Succeeded = processingResponse.Success;
            // store payment & operation result in data-store
            paymentEntry = await _paymentRepository.AddAsync(paymentEntry);

            if (!processingResponse.Success)
            {
                response = HandleProcessingError(paymentEntry.Id, processingResponse);
            }
            else
            {
                response.PaymentId = paymentEntry.Id.ToString();
            }

            return response;
        }
        catch (Exception ex)
        {
            var traceId = Guid.NewGuid();
            var errMsg = $"TraceId: {traceId}, exception thrown while processing {nameof(CreatePaymentCommand)} request.";
            _logger.LogError(ex, errMsg);
            throw new InternalServerErrorException($"Runtime Error - Please Contact Support - TraceId {traceId}");
        }
    }

    private static CreatePaymentCommandResponse HandleProcessingError(Guid paymentId,PaymentProcessingResponse processingResponse)
    {
        return new CreatePaymentCommandResponse(processingResponse.Error)
        {
            PaymentId = paymentId.ToString()
        };
    }
}