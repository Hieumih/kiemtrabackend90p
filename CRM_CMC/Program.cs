
using CRM_CMC.Contexts;
using CRM_CMC.Models;
using CRM_CMC.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"

    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
//builder.Services.AddDbContext<BACKEND_CRMContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("CRM_SQLite")));
builder.Services.AddDbContext<BACKEND_CRMContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("CRM")));

builder.Services
    .AddIdentity<UserInfo, IdentityRole>()
    .AddEntityFrameworkStores<BACKEND_CRMContext>()
    .AddDefaultTokenProviders();

//builder.Services.AddDefaultIdentity<userInfo>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<BACKEND_CRMContext>();

builder.Services.AddScoped<IAccountRepositories, AccountRepositories>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});
//.AddCookie(options =>
//{
//    options.ExpireTimeSpan = TimeSpan.FromHours(3);
//    options.SlidingExpiration = true;
//    //options.Cookie.HttpOnly = true;
//    options.AccessDeniedPath = "/Forbidden/";
//});

builder.Services.AddMemoryCache();



builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = builder.Configuration.GetValue<bool>("Validation:Password:RequireDigit");
    options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Validation:Password:RequireLowercase");
    options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Validation:Password:RequireUppercase");
    options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Validation:Password:RequireNonAlphanumeric");
    options.Password.RequiredLength = builder.Configuration.GetValue<int>("Validation:Password:RequiredLength");
    //options.Lockout.MaxFailedAccessAttempts = 8;
    //options.Lockout.AllowedForNewUsers = true;
    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
});

builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "dist";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.UseCookiePolicy(new CookiePolicyOptions
//{
//    MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
//});

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.MapControllers();

app.UseSpaStaticFiles();

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "frontend";

    if (app.Environment.IsDevelopment())
    {
        spa.UseProxyToSpaDevelopmentServer("http://localhost:5173");
    }
});

app.Run();
