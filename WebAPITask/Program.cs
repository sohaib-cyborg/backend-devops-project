using BusinessAccessLayer.Services.Addresses;
using BusinessAccessLayer.Services.Auth;
using BusinessAccessLayer.Services.Orders;
using BusinessAccessLayer.Services.PaymentMethods;
using BusinessAccessLayer.Services.Products;
using DataAccessLayer.Context;
using DataAccessLayer.DAL.AddressRepo;
using DataAccessLayer.DAL.AuthRepo;
using DataAccessLayer.DAL.OrderRepo;
using DataAccessLayer.DAL.PaymentMethodRepo;
using DataAccessLayer.DAL.ProductRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPITask;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//DataAccessLayer.DALStartup.ConfigureServices(builder.Services);
//BusinessAccessLayer.BALStartup.ConfigureServices(builder.Services);
builder.Services.AddDbContext<AuthAPIDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("localConnection"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 5;
})
.AddEntityFrameworkStores<AuthAPIDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:7299",
        ValidAudience = "https://localhost:7299",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("C5F29726-F162-4CB4-95CB-059EB70CDD52"))
    };
});
builder.Services.AddScoped<IAuthRepo, AuthRepository>();
builder.Services.AddScoped<IAddressRepo, AddressRepository>();
builder.Services.AddScoped<IOrderRepo, OrderRepository>();
builder.Services.AddScoped<IPaymentMethodRepo, PaymentMethodRepository>();
builder.Services.AddScoped<IProductRepo, ProductRepository>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IAddressServices, AddressServices>();
builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IOrderService, OrderService>();
var app = builder.Build();

app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var Context = scope.ServiceProvider.GetRequiredService<AuthAPIDbContext>();
    Context.Database.EnsureCreated();
    await DbSeeder.SeedRolesAndAdminAsync(scope.ServiceProvider);
}

app.Run();
