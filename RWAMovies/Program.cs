using AutoMapper;
using DataLayer.DALModels;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<RwaDatabaseContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("RWAConnStr"));
    options.UseSqlServer("name=ConnectionStrings:RWAConnStr");
});

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
/*
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
*/
builder.Services
.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.HttpOnly = true;
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdministratorOnly", policy => policy.RequireRole("Administrator"));
    options.AddPolicy("MemberOnly", policy => policy.RequireRole("Member"));
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddAutoMapper(
    typeof(RWAMovies.Mapping.AutomapperVideo),
    typeof(DataLayer.Mapping.AutomapperVideo),
    typeof(RWAMovies.Mapping.AutomapperGenre),
    typeof(DataLayer.Mapping.AutomapperGenre),
    typeof(RWAMovies.Mapping.AutomapperTag),
    typeof(DataLayer.Mapping.AutomapperTag),
    typeof(RWAMovies.Mapping.AutomapperNotification),
    typeof(DataLayer.Mapping.AutomapperNotification),
    typeof(RWAMovies.Mapping.AutomapperUser),
    typeof(DataLayer.Mapping.AutomapperUser)
    );
builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserView}/{action=LogIn}/{id?}");

app.Run();
