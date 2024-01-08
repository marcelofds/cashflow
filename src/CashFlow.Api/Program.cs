using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using CashFlow.Api.Middlewares;
using CashFlow.IoC.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Configuration.AddEnvironmentVariables();
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
builder.Configuration.AddUserSecrets(Assembly.GetAssembly(typeof(Program))!);
builder.Services.ConfigureAccessor();
builder.Services.AddControllers(opt => 
        opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>()
    )
    .AddControllersAsServices()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
    });
builder.Services.RegisterServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration((context.Configuration)));
var app = builder.Build();

app.ConfigureExceptionMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
        options.DisplayRequestDuration();
    });
}
app.ConfigureApplication();
app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();