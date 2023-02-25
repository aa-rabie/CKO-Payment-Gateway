using CKO.PaymentGateway.Api.Middleware;
using CKO.PaymentGateway.Application;
using CKO.PaymentGateway.BankSimulator;
using CKO.PaymentGateway.Persistence;

namespace CKO.PaymentGateway.Api;

public static class StartupExtensions
{
    /// <summary>
    /// configure DI 
    /// </summary>
    public static WebApplication ConfigureServices(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddBankServices();
        builder.Services.AddPersistenceServices();
        builder.Services.AddApplicationServices();

        builder.Services.AddControllers();

        return builder.Build();
    }

    public static WebApplication ConfigureMiddlewarePipeline(this WebApplication app)
    {
        app.UseHttpsRedirection();

        // TODO: uncomment to enable authentication
        // app.UseAuthentication();

        // add ExceptionHandlerMiddleware middleware
        app.UseCustomExceptionHandler();

        // TODO: uncomment to enable Authorization
        // app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}