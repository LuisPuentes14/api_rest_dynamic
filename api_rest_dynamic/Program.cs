
using api_rest_dynamic;
using api_rest_dynamic.IOC;
using Asp.Versioning;
using midelware.Middlewares;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Star log");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.InyectarDependencia(builder.Configuration);

    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0); // Versión por defecto
        options.AssumeDefaultVersionWhenUnspecified = true; // Usar versión por defecto si no se especifica
        options.ReportApiVersions = true; // Informar de las versiones disponibles en los encabezados de respuesta
    });    

    var app = builder.Build();

    LoadObjectsDataBase.GetObjectsDataBase();


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<LogMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{

    logger.Error(e, "Programa detenido causado por una exception");
}
