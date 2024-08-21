using AutoMapper;
using LocalViagem.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using RotaViagem.API.ViewModels;
using RotaViagem.Entidades.Entities;
using RotaViagem.Infra.Context;
using RotaViagem.Infra.Interfaces;
using RotaViagem.Infra.Repositories;
using RotaViagem.Service.Interfaces;
using RotaViagem.Service.Services;

var builder = WebApplication.CreateBuilder(args);

#region AutoMapper
var automapperConfig = new MapperConfiguration(cfg =>
{

    cfg.CreateMap<CreateRotaViewModel, Rota>().ReverseMap();
    cfg.CreateMap<UpdateRotaViewModel, Rota>().ReverseMap();


});

builder.Services.AddSingleton(automapperConfig.CreateMapper());
#endregion


builder.Services.AddDbContext<ManagerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ManagerAPISqlServer"));
});

#region InjecaoDependencia
//Injeção de Dependencias
builder.Services.AddScoped<IRotaService, RotaService>();
builder.Services.AddScoped<IRotaRepository, RotaRepository>();

builder.Services.AddScoped<ILocalService, LocalService>();
builder.Services.AddScoped<ILocalRepository, LocalRepository>();
#endregion




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
