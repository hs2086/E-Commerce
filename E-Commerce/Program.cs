using Contracts;
using E_Commerce.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Repository.Repos.Email;
using Service.DataShapping;
using Shared.DataTransferObject.Product;
using Shared.Validators.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    })
    .AddApplicationPart(typeof(E_Commerce.Presentation.AssemblyReference).Assembly);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.ConfigureLogging();
builder.Services.ConfigureLoggerService();

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.ConfigureJwtTokenProvider(builder.Configuration);
builder.Services.ConfigureTimeSpanTokenProvider();

builder.Services.AddScoped<IDataShaper<ProductDto>, DataShaper<ProductDto>>();
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<AuthRegisterDtoValidator>();

var app = builder.Build();
var logger = app.Services.GetRequiredService<Contracts.Logger.ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
