using AutoMapper;
using CKO.PaymentGateway.Application.Features.Payments.Commands.CreatePayment;
using CKO.PaymentGateway.Application.Features.Payments.Queries.GetPayment;
using CKO.PaymentGateway.Application.Models.BankSimulator;
using CKO.PaymentGateway.Domain.Entities;

namespace CKO.PaymentGateway.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Payment, CreatePaymentCommand>().ReverseMap();
        CreateMap<CreatePaymentCommand,PaymentProcessingRequest>();
        CreateMap<Payment, PaymentDto>();
    }
}