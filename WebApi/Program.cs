using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// autofac container configuration
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
builder.RegisterModule(new AutofacBusinessModule()));




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// cross origin
//builder.Services.AddCors(p =>
//{
//    // hepsine izin verir
//    p.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());


//    // spesifik izin - angular için 4200 port
//    // p.AddPolicy("AllowOrigin", options => options.WithOrigins("http://localhost:4200/"));
//});

// jwt
IConfiguration configuration = builder.Configuration;
var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false, // false yapmazsak token bittiðinde sistemden düþürüyor
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// cors config
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
