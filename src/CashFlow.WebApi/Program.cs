using CashFlow.IoC.Extensions;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Configuration.AddEnvironmentVariables();
builder.Services.ConfigureAccessor();
builder.Services.AddCors();
builder.Services.AddControllers(opt =>
        opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>()
    )
    .AddControllersAsServices()
    .AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentations();
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x =>
{
    x.AllowAnyOrigin();
    x.AllowAnyMethod();
    x.AllowAnyHeader();
});

app.ConfigureApplication();
app.UseAuthorization();

app.MapControllers();

app.Run();