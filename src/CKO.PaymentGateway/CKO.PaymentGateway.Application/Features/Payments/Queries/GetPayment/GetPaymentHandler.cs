using AutoMapper;
using CKO.PaymentGateway.Application.Contracts;
using CKO.PaymentGateway.Application.Contracts.Persistence;
using CKO.PaymentGateway.Application.Exceptions;
using CKO.PaymentGateway.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CKO.PaymentGateway.Application.Features.Payments.Queries.GetPayment;
/// <summary>
/// Responsible for processing Incoming Request to retrieve specific Payment operation from data store.
/// Returns 'PaymentDto' object after masking card information if Payment exists in data store
/// Or throws 'NotFoundException' exception if 'Id' in incoming request does not exist in data store 
/// </summary>
public class GetPaymentHandler : IRequestHandler<GetPaymentQuery,PaymentDto>
{
    private readonly IMapper _mapper;
    private readonly IDataMaskingService _dataMaskingService;
    private readonly IRepository<Payment> _paymentRepository;
    private readonly ILogger<GetPaymentHandler> _logger;

    public GetPaymentHandler(IMapper mapper,
        IDataMaskingService dataMaskingService,
        IRepository<Payment> paymentRepository,
        ILogger<GetPaymentHandler> logger)
    {
        _mapper = mapper;
        _dataMaskingService = dataMaskingService;
        _paymentRepository = paymentRepository;
        _logger = logger;
    }
    public async Task<PaymentDto> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var dbRecord = await _paymentRepository.GetByIdAsync(request.Id);
            if (dbRecord == null)
            {
                throw new NotFoundException(nameof(Payment), request.Id);
            }
            var dto = _mapper.Map<PaymentDto>(dbRecord);
            MaskData(dto);
            return dto;
        }
        catch (Exception ex) when(ex is not NotFoundException)
        {
            var traceId = Guid.NewGuid();
            var errMsg = $"TraceId: {traceId}, exception thrown while processing {nameof(GetPaymentQuery)} request.";
            _logger.LogError(ex, errMsg);
            throw new InternalServerErrorException($"Runtime Error - Please Contact Support - TraceId {traceId}");
        }
    }

    private void MaskData(PaymentDto dto)
    {
        dto.CardNumber = _dataMaskingService.MaskCardNumber(dto.CardNumber);
        dto.Cvv = _dataMaskingService.MaskCardCvv(dto.Cvv);
    }
}