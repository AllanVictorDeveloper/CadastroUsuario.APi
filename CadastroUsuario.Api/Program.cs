
using CadastroUsuario.Api.Configuration;
using CadastroUsuario.Api.Middleware;
using CadastroUsuario.Domain.Interfaces;
using CadastroUsuario.Infra.IoC;
using Microsoft.IdentityModel.Logging;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Habilitar logs de PII para diagnosticar o problema
IdentityModelEventSource.ShowPII = true;

builder.Services.AddLogging();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapperConfiguration();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllOrginsAllow", options =>
    {
        options.AllowAnyOrigin();
        options.AllowAnyMethod();
        options.AllowAnyHeader();
    });
});



builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddInfrastructureJWT(builder.Configuration);
builder.Services.AddInfrastructureSwagger();

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllOrginsAllow");

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var seedUserRoleInitial = services.GetRequiredService<ISeedUserRoleInitial>();

    seedUserRoleInitial.SeedRoles();
}

app.UseMiddleware<CustomAuthenticationMiddleware>();


app.UseAuthentication();
app.UseAuthorization();





app.UseHttpsRedirection();

app.MapControllers();

app.Run();
