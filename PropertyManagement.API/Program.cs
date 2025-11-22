using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using PropertyManagement.API.Data;
using PropertyManagement.API.Mappings;
using PropertyManagement.API.Models.Cloudinary;
using PropertyManagement.API.Repositories;
using PropertyManagement.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext class
builder.Services.AddDbContext<PMDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PMConnectionString"));
});

// Add Repository
builder.Services.AddScoped<IPropertyRepository, SQLPropertyRepository>();
builder.Services.AddScoped<IUserRepository, SQLUserRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();

// Add Service
builder.Services.AddScoped<IReportService, ReportService>();

// Add Automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Add Cloudinary
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));

builder.Services.AddSingleton(provider =>
{
    var config = provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;

    return new Cloudinary(new Account(
        config.CloudName,
        config.ApiKey,
        config.ApiSecret
    ));
});

builder.Services.AddScoped<ICloudStorageService, CloudStorageService>();

var app = builder.Build();

ExcelPackage.License.SetNonCommercialPersonal("Rajat Jadhav");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
