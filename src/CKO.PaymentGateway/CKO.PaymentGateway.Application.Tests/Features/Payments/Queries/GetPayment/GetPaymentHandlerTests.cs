using AutoFixture;
using AutoMapper;
using CKO.PaymentGateway.Application.Contracts;
using CKO.PaymentGateway.Application.Contracts.Persistence;
using CKO.PaymentGateway.Application.Exceptions;
using CKO.PaymentGateway.Application.Features.Payments.Queries.GetPayment;
using CKO.PaymentGateway.Application.Profiles;
using CKO.PaymentGateway.Application.Services;
using CKO.PaymentGateway.Domain.Entities;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace CKO.PaymentGateway.Application.Tests.Features.Payments.Queries.GetPayment
{
    public class GetPaymentHandlerTests
    {
        readonly AutoMocker _mocker;
        readonly Fixture _fixture;

        public GetPaymentHandlerTests()
        {
            _fixture = new Fixture();
            _mocker = new AutoMocker();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            
            _mocker.Use<IMapper>(configurationProvider.CreateMapper());
            _mocker.Use<IDataMaskingService>(new DataMaskingService());
        }

        [Fact]
        public async Task Handle_Should_Returns_Masked_Dto_WhenPaymentId_Found()
        {
            // setup
            var paymentEntity = _fixture.Create<Payment>();
            paymentEntity.CardNumber = "6771-8994-9024-5742";
           
            _mocker.GetMock<IRepository<Payment>>()
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(paymentEntity);

            var expectedDto = new PaymentDto()
            {
                CardNumber = "***************5742",
                Amount = paymentEntity.Amount,
                CardType = paymentEntity.CardType,
                CreatedAt = paymentEntity.CreatedAt,
                Currency = paymentEntity.Currency,
                Cvv = new string('*', paymentEntity.Cvv.Length),
                ExpiryMonth = paymentEntity.ExpiryMonth,
                ExpiryYear = paymentEntity.ExpiryYear,
                Id = paymentEntity.Id,
                MerchantId = paymentEntity.MerchantId,
                Succeeded = paymentEntity.Succeeded,
            };

            // act
            var sut = _mocker.CreateInstance<GetPaymentHandler>();
            var actual = await sut.Handle(new GetPaymentQuery() { Id = paymentEntity.Id }, CancellationToken.None);

            //assert
            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expectedDto);
        }

        [Fact]
        public async Task Handle_Should_ThrowsNotFoundException_WhenPaymentId_NotFound()
        {
            // setup
            _mocker.GetMock<IRepository<Payment>>()
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Payment) null);

            var expectedMsg = new NotFoundException(nameof(Payment), Guid.Empty).Message;
            
            // act
            var sut = _mocker.CreateInstance<GetPaymentHandler>();
            Func<Task> act = () => sut.Handle(new GetPaymentQuery() { Id = Guid.Empty }, CancellationToken.None);
            
            //assert
            await act.Should().ThrowAsync<NotFoundException>()
                .Where(e => e.Message == expectedMsg);
        }
    }
}