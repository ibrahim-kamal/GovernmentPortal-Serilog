using GovernmentPortal.Services;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Services.AddSerilog();
builder.Services.AddSwaggerGen();
// Add services
builder.Services.AddControllers();
builder.Services.AddSingleton<ILicenseService, LicenseService>();

var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseRouting();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
