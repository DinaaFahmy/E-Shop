using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shop.Models;
using Shop.Models.Models;
using Shop.Persistence.Data;
using Shop.Persistence.Interfaces;
using Shop.Persistence.Services;
using System.Text;
using Shop.Core.Services;
using Shop.Core.Mapper;
using Microsoft.OpenApi.Models;
using Shop.Middlewares;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


// Add services to the container.

//Inject Repositories
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//Inject Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<BuyersService>();
builder.Services.AddScoped<OrderService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shop", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {{
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                }});
});

builder.Services.Configure<AppSettings>(configuration);
var appSettings = configuration.Get<AppSettings>();

builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
{
    optionsBuilder.UseMySql(appSettings.MySQLConnectionString, ServerVersion.AutoDetect(appSettings.MySQLConnectionString))
                  .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// Identity
builder.Services.AddIdentity<Profile, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddUserManager<UserManager<Profile>>()
    .AddRoleManager<RoleManager<Role>>()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IAuthenticationSchemeProvider, CustomAuthenticationSchemeProvider>();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.JWTSecretKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//Auto Mapper
var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseCors(x =>
    {
        x.WithOrigins(appSettings.FrontendOrigin)
        .AllowAnyMethod()
        .AllowCredentials()
        .AllowAnyHeader();
    });
}
else
{
    app.UseCors(x =>
    {
        x.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
}
app.UseWhen(context => context.Request.Path.Value.StartsWith("/api"), subBranch =>
{
    //subBranch.UseAuthenticationOverride(JwtBearerDefaults.AuthenticationScheme);
    app.UseMiddleware<AuthenticationOverrideMiddleware>(new AuthenticationOptions { DefaultScheme = JwtBearerDefaults.AuthenticationScheme });
});
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseMiddleware<UserDetailsMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "api/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapDefaultControllerRoute();
});
app.Run();


//// SeedRoles
//var existingRoles = (await _roleRepository.GetAll()).Select(r => r.Name);
//var roles = Roles.ALL_ROLES;
//foreach (var role in roles)
//{
//    if (!existingRoles.Contains(role))
//    {
//        var toBeAddedRole = new Role { Name = role };
//        await _roleRepository.Add(toBeAddedRole);
//    }
//}

