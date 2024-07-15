using BusinessLayer.Implementations;
using BusinessLayer.Interfaces;
using DataLayer.Implementations;
using DataLayer.Interfaces;
using Mapper.Implementations;
using Mapper.Interfaces;
using OfficeOpenXml;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDataLayerMapper, DataLayerMapper>();
builder.Services.AddScoped<IBusinessLayerMapper, BusinessLayerMapper>();
builder.Services.AddScoped<IConnection, Connection>();
builder.Services.AddScoped<IExceRepo, ExcelRepo>();
builder.Services.AddScoped<IExcelService, ExcelService>();
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("ReactCORS", options =>
    {
        options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ReactCORS");

app.UseAuthorization();

app.MapControllers();

app.Run();
