using AutoMapper;
using JWTAppBackOffice.Core.Application.Interfaces;
using JWTAppBackOffice.Core.Application.Mappings;
using JWTAppBackOffice.Persistance.Context;
using JWTAppBackOffice.Persistance.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder aslında servisleri yazacağımız alan.
// startup artık program.cs içerisine taşındı
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//burada eskiden hatırladığımız şekilde di'lar yapılabilir.
builder.Services.AddDbContext<JWTContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("conStr"));
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddMediatR(Assembly.GetExecutingAssembly()); //Assembly çalıştığında eklensin diye bu şekilde kayıt ediyorum.

builder.Services.AddAutoMapper(opt =>
{
    opt.AddProfiles(new List<Profile>()
    {
        new ProductProfile() //buraya eklenerek kayıt edilecek
    });
});


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
