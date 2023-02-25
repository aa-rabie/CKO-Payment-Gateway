using CKO.PaymentGateway.Api;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{

    var builder = WebApplication.CreateBuilder(args);
    
    // Here logs are written to files 
    // In real application
    // It is better to write logs to cloud service like Azure Application Insights or AWS CloudWatch
    // or Cloud Storage like AWS S3 
    // or Database 
    builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
        .WriteTo.Console()
        .ReadFrom.Configuration(context.Configuration));

    var app = builder
        .ConfigureServices()
        .ConfigureMiddlewarePipeline();

    app.UseSerilogRequestLogging();

    app.Run();
}
catch (Exception ex)
{
    // Any unhandled exception during start-up will be caught and flushed to
    // our log file or centralized log server
    Log.Fatal(ex, "An unhandled exception during bootstrapping");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }
