using API;
using API.Data;
using API.Interfaces;
using API.Middleware;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin().AllowAnyHeader());
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
    };

    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// -----------------------------------------------------------------------------------------------------

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IHeatProgramRepository, HeatJSONRegister>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<HeatProgramServices>();

IConfigurationBuilder secretBuilder = new ConfigurationBuilder().AddJsonFile("secrets.json", optional: false, reloadOnChange: true);
IConfigurationRoot secretRoot = secretBuilder.Build();
var jwtSettings = secretRoot.GetSection("JwtSettings");
var secret = jwtSettings.GetValue<string>("Secret");

byte[] key = [];
if (secret != null)
{
    key = Encoding.ASCII.GetBytes(secret);
}
else
{
    throw new Exception("M� configura��o do secret.json");
}

Settings.GetInstance().SetJWTSecretKey(key);


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
// -----------------------------------------------------------------------------------------------------

var app = builder.Build();

app.UseMiddleware<CustomUnauthorizedResponseMiddleware>();
app.UseMiddleware<CustomBadRequestMiddleware>();

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.UseRequestLocalization();

app.MapControllers();

app.Run();