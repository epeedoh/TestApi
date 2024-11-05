//using EPE.Auth;
using EPE_AuthLibrary.Config;
using EPE_AuthLibrary.Interfaces;
using EPE_AuthLibrary.Services;
using Microsoft.AspNetCore.Builder;
using TestApi.Services;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


// Charger la configuration depuis appsettings.json
var jwtConfigSection = builder.Configuration.GetSection("JwtConfig");
var jwtConfig = jwtConfigSection.Get<JwtConfig>();

// Vérifier si jwtConfig est null
if (jwtConfig == null)
{
    throw new InvalidOperationException("Configuration JwtConfig non trouvée dans appsettings.json");
}



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUserRepository, UserRepository>();  
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITokenService, TokenService>();

builder.Configuration.GetSection("JwtConfig");

//// Ajouter l'authentification avec EPE.Auth
//builder.Services.AddEPEAuthentication(options =>
//{
//    options.JwtSecret = "VOTRE_CLE_SECRETE"; // Remplacez par une clé secrète appropriée
//    options.TokenValidityInMinutes = 60; // Définir la durée de validité du token en minutes
//});

var app = builder.Build();

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
