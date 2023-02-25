using AutoFixture;
using CKO.PaymentGateway.Api.IntegrationTests.Factories;
using CKO.PaymentGateway.Application.Features.Payments.Queries.GetPayment;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq.AutoMock;
using FluentAssertions;
using System.Text.Json;

namespace CKO.PaymentGateway.Api.IntegrationTests.Controllers;

public class PaymentsControllerTests
{
    private readonly AutoMocker _mocker = new AutoMocker();
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public async Task GetById_Should_Return_Successful_Response_With_Valid_Input()
    {
        //setup
        var paymentDto = _fixture.Create<PaymentDto>();

        await using var appFactory = new CustomWebApplicationFactory(services =>
        {
            var mediatorDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(IMediator));

            if (mediatorDescriptor == null)
            {
                throw new Exception($"'{typeof(IMediator)}' was not registered in DI Container");
            }

            services.Remove(mediatorDescriptor);

            _mocker.GetMock<IMediator>()
                .Setup(m => m.Send(It.IsAny<GetPaymentQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(paymentDto);

            services.Add(new ServiceDescriptor(typeof(IMediator),
                _ => _mocker.GetMock<IMediator>().Object, mediatorDescriptor.Lifetime));
        });
        var client = appFactory.CreateClient();

        //act
        var response = await client.GetAsync($"/api/payments/{paymentDto.Id}");

        //assert
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        var actualDto = System.Text.Json.JsonSerializer.Deserialize<PaymentDto>(responseString, serializeOptions);
        actualDto.Should().BeEquivalentTo(paymentDto);
    }
}