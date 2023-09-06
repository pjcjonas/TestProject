using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Helpers;
using settings;
using api_gaming_global.Models.Crud;
using api_gaming_global.Helpers;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var oAuthClientId = Environment.GetEnvironmentVariable("GoogleOAuth_ClientId");
var oAuthClientSecret = Environment.GetEnvironmentVariable("GoogleOAuth_ClientSecret");
var jwtSecretToken = Environment.GetEnvironmentVariable("JWTSecretToken");
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretToken));
var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                        builder.WithOrigins("http://localhost:3000",
                                              "http://localhost:3000/login");
                    });
});

builder.Services.AddAuthentication(options => {
    // options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //options.DefaultScheme = GoogleDefaults.AuthenticationScheme;
    //options.DefaultAuthenticateScheme = "Google";

    options.DefaultScheme = "Application";
    options.DefaultSignInScheme = "External";
})
.AddCookie("Application")
.AddCookie("External")
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "localhost:3000",
        ValidAudience = "localhost:3000",
        IssuerSigningKey = creds.Key
    };
})
.AddGoogle(options =>
{
    options.ClientId = oAuthClientId;
    options.ClientSecret = oAuthClientSecret;
});

//DEP INJ

var dbConnectionString = builder.Configuration.GetValue<string>("DbConnectionString");
Settings settings = new Settings
{
    DbConnectionString = dbConnectionString,
};

builder.Services.AddSingleton(settings);
builder.Services.AddSingleton<SqlHelper>();
builder.Services.AddSingleton<UserCrud>();
builder.Services.AddSingleton<JwtHelper>();
builder.Services.AddSingleton<ProductCrud>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowAllOrigins");

app.Run();
