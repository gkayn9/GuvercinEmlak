using System.Text;
using FluentValidation;
using Guvercin.Application.Dtos.AdvertItemsDtos;
using Guvercin.Application.Dtos.CategoryDtos;
using Guvercin.Application.Dtos.UserDtos;
using Guvercin.Application.Helpers;
using Guvercin.Application.Interfaces;
using Guvercin.Application.Mapping;
using Guvercin.Application.Services.Abstract;
using Guvercin.Application.Services.Concrete;
using Guvercin.Application.Validators.Users;
using Guvercin.Persistance.Context;
using Guvercin.Persistance.Context.Identity;
using Guvercin.Persistance.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

//Jwt Authentication

#region Jwt Token 

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

#endregion

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("GuvercinDbConnectionString")));

builder.Services.AddDbContext<AppIdentityDbContext>(options=>
    options.UseNpgsql(builder.Configuration.GetConnectionString("GuvercinDbConnectionString")));

builder.Services.AddIdentity<AppIdentityUser, AppIdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAdvertItemServices, AdvertItemServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IAdvertItemRepository, AdvertItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserServices, UserService>();
builder.Services.AddScoped<IAuthServices, AuthService>();
builder.Services.AddScoped<TokenHelpers>();




builder.Services.AddAutoMapper(typeof(GeneralMapping));
builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAdvertItemDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateAdvertItemDto>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();

    



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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
