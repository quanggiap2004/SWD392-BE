using Application.Services.Implementations;
using Application.Services.Interfaces;
using Data.Mapper;
using Data.Repository.Implementations;
using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities.ApplicationEntities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddCors(options =>
{
    //options.AddPolicy(name: MyAllowSpecificOrigins, // This is the policy name
    //                  policy =>
    //                  {
    //                      policy.WithOrigins("http://localhost:5173")
    //                          .AllowAnyHeader()
    //                          .AllowAnyMethod()
    //                          .AllowCredentials();
    //                  });

    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Allow any origin
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
    //options.AddPolicy("AllowFrontend", policy =>
    //{
    //    policy.WithOrigins("http://localhost:5173") // Replace with your frontend URL
    //          .AllowAnyHeader()
    //          .AllowAnyMethod();
    //});

});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlindBoxSystem API", Version = "v1" });

    // Add JWT Authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddDbContext<BlindBoxSystemDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("connection")).EnableSensitiveDataLogging());

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = false;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
})
    .AddEntityFrameworkStores<BlindBoxSystemDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    // this sets the lifespan for generated tokens like email and reset password
    options.TokenLifespan = TimeSpan.FromMinutes(5);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;


})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured.")))
        };
    })
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
    options.AddPolicy("StaffPolicy", policy => policy.RequireRole("Staff"));
});

#region Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IBoxService, BoxService>();
builder.Services.AddScoped<IBoxImageService, BoxImageService>();
builder.Services.AddScoped<IBoxItemService, BoxItemService>();
builder.Services.AddScoped<IBoxOptionService, BoxOptionService>();
builder.Services.AddScoped<IBoxService, BoxService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderStatusDetailService, OrderStatusDetailService>();
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IShippingService, ShippingService>();
builder.Services.AddScoped<IOnlineSerieBoxService, OnlineSerieBoxService>();
builder.Services.AddScoped<IUserRolledItemService, UserRolledItemService>();
builder.Services.AddScoped<ICurrentRolledItemService, CurrentRolledItemService>();
#endregion

#region Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IBoxRepository, BoxRepository>();
builder.Services.AddScoped<IBoxImageRepository, BoxImageRepository>();
builder.Services.AddScoped<IBoxItemRepository, BoxItemRepository>();
builder.Services.AddScoped<IBoxOptionRepository, BoxOptionRepository>();
builder.Services.AddScoped<IBoxRepository, BoxRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderStatusDetailRepository, OrderStatusDetailRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
builder.Services.AddScoped<IUserVotedBoxItemRepository, UserVotedBoxItemRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IOnlineSerieBoxRepository, OnlineSerieBoxRepository>();
builder.Services.AddScoped<IUserRolledItemRepository, UserRolledItemRepository>();
builder.Services.AddScoped<ICurrentRolledItemRepository, CurrentRolledItemRepository>();
#endregion

builder.Services.AddHttpClient<ShippingService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
