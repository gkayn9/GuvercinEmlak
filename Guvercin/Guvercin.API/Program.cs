using FluentValidation;
using Guvercin.Application.Dtos.AdvertItemsDtos;
using Guvercin.Application.Dtos.CategoryDtos;
using Guvercin.Application.Interfaces;
using Guvercin.Application.Mapping;
using Guvercin.Application.Services.Abstract;
using Guvercin.Application.Services.Concrete;
using Guvercin.Persistance.Context;
using Guvercin.Persistance.Repository;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("GuvercinDbConnectionString")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAdvertItemServices, AdvertItemServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IAdvertItemRepository, AdvertItemRepository>();
builder.Services.AddAutoMapper(typeof(GeneralMapping));
builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAdvertItemDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateAdvertItemDto>();

    



var app = builder.Build();

builder.Services.AddEndpointsApiExplorer();
app.MapScalarApiReference(opt =>
    {
        opt.Title = "Guvercin API V1";
        opt.Theme = ScalarTheme.BluePlanet;
        opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
    }
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
