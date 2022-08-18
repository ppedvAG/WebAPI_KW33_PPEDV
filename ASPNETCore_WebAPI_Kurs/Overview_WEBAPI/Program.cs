using Serilog;
using Serilog.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Overview_WEBAPI.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


//EF Core 
builder.Services.AddDbContext<MovieDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDbContext") ?? throw new InvalidOperationException("Connection string 'MovieDbContext' not found."));
});
    
#region Initialiserung Phase von WebAPI APP
// Add services to the IOC - Container.

//Hinzuf�gen unserer WebAPI 
//Via AddController werden auch weitere Dienste dem IOC Container (Default-Dienste) hinzugef�gt
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            //.WriteTo.Seq("http://localhost:5341")
            .WriteTo.File("Log\\log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

#endregion

//Build beendet die Initialisierungphase

//Initialisierung von Serilog
builder.Host.UseSerilog();

var app = builder.Build();


#region Konfigure meine Dienste, da wo Sie laufen sollen (Ebene->WebServer)

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
#endregion


//Der Endpunkt f�r WebAPI-Anfragen -> URL findet richtigen Controller und Action-Methode
app.MapControllers();

app.Run();
