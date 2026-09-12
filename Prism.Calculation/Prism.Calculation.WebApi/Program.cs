using Prism.Calculation.WebApi.Extensions;
using Prism.Calculation.WebApi.Middlewares;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opt.JsonSerializerOptions.AllowTrailingCommas = true;
        opt.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

//builder.Services.AddPostgresDbContext(builder.Configuration.GetConnectionString(AppConstants.PostgresConnectionStringSectionName));
builder.Services
    .AddEndpointsApiExplorer()
    .AddLogging()
    .AddServices()
    .AddOptions(builder.Configuration);
//builder.Services.AddRepositories();
//builder.Services.AddMappers();

var app = builder.Build();

app.UseCors(options => options
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());
app.UseRouting();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
