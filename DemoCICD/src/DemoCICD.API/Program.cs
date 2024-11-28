using DemoCICD.API.DependencyInjection.Extensions;
using DemoCICD.API.Middleware;
using DemoCICD.Application.DependenceInjection.Extensions;
using DemoCICD.Infrastructure.Dapper.DependenceInjection.Extensions;
using DemoCICD.Persistence.DependenceInjection.Extensions;
using DemoCICD.Persistence.DependenceInjection.Options;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Cấu hình Serilog - mục đích để ghi log ra file trong folder logs
Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();
builder.Logging
    .ClearProviders()
    .AddSerilog();
builder.Host.UseSerilog();

// ===== Add Config - DependeceInjection =====
builder.Services.AddConfigureMediatR(); // Application

// Api - Thực hiện tham chiếu đến các đầu api trong Presentation
builder.Services
    .AddControllers()
    .AddApplicationPart(DemoCICD.Presentation.AssemblyReference.Assembly);

// DI - Xử lí ngoại lệ.
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// Configure Options and SQL
// -- Thiết lập của Persistence - giá trị khởi tạo trong appsettings.json (DemoCICD.API)
builder.Services.ConfigureSqlServerRetryOptions(builder.Configuration.GetSection(nameof(SqlServerRetryOptions)));
builder.Services.AddSqlConfiguration(); // => Persistence
builder.Services.AddRepositoryBaseConfiguration(); // Persistence
builder.Services.AddConfigurationAutoMapper(); // Application

// Configure Dapper
builder.Services.AddInfrastructureDapper();

// Cấu hình Swagger
builder.Services
        .AddSwaggerGenNewtonsoftSupport()
        .AddFluentValidationRulesToSwagger()
        .AddEndpointsApiExplorer()
        .AddSwagger();

// Cấu hình API Versioning
builder.Services
    .AddApiVersioning(options => options.ReportApiVersions = true)
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

var app = builder.Build();

// Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
    app.ConfigureSwagger();

try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}

public partial class Program { }
