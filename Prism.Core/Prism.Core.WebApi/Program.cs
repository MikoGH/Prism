using Prism.Core.WebApi.Constants;
using Prism.Core.WebApi.Extensions;
using Prism.Core.WebApi.Middlewares;
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
    });

builder.Services.AddPostgresDbContext(builder.Configuration.GetConnectionString(AppConstants.PostgresConnectionStringSectionName));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLogging();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddMappers();
builder.Services.AddPrismAuthentication(builder.Configuration.GetValue<string>(AppConstants.JwtSecretKeySectionName));

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

await app.MigrateDatabaseAsync();

app.Run();
