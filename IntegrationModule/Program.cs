using DataLayer.DALModels;
using DataLayer.Repositories;
using IntegrationModule.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RwaDatabaseContext>(options =>
{
    options.UseSqlServer("name=ConnectionStrings:RWAConnStr");
});

builder.Services.Configure<MFakeSMTP>(
    builder.Configuration.GetSection(MFakeSMTP.FakeSMTP));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        var jwtKey = builder.Configuration["JWT:Key"];
        var jwtKeyBytes = Encoding.UTF8.GetBytes(jwtKey!);
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(jwtKeyBytes),
            ValidateLifetime = true,
        };
    });

builder.Services.AddAutoMapper(
    typeof(IntegrationModule.Mapping.AutomapperVideo),
    typeof(DataLayer.Mapping.AutomapperVideo));
builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
