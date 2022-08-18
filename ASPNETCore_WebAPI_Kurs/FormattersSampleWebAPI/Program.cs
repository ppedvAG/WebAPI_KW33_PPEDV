using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FormattersSampleWebAPI.Data;
using WebApiContrib.Core.Formatter.Csv;
using FormattersSampleWebAPI.Formatters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FootballClubDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FootballClubDb") ?? throw new InvalidOperationException("Connection string 'FootballClubDb' not found.")));

// Add services to the container.

builder.Services.AddResponseCaching();

builder.Services.AddControllers(options=>
{
    options.OutputFormatters.Insert(0, new VCardOutputFormatter());
    options.InputFormatters.Insert(0, new VCardInputFormatter());
})
    .AddXmlSerializerFormatters() //Wir können jetzt XML
    .AddCsvSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

app.Run();
