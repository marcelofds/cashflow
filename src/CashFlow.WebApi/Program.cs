using CashFlow.IoC.Extensions;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;

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
     .AddNewtonsoftJson(options =>
     {
         options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
         options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
     });
builder.Services.RegisterServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentations();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
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