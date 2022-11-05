using Belle.Common;
using Belle.EntityFramework;
using Belle.Services.Authentication;
using Belle.Services.Authentication.Models;
using Belle.Services.CurrentUser;
using Belle.Services.GenericRepository;
using Belle.Services.UserService;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var services = builder.Services;

services.AddMvcCore();

services.AddRouting(options =>
{
    options.LowercaseQueryStrings = true;
    options.LowercaseUrls = true;
});

services.AddFluentValidationAutoValidation();
services.AddValidatorsFromAssemblyContaining<Program>();

services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Constants.Configuration.CONNECTION_STRING));

services.AddAuthentication(options =>
{
    options.RequireAuthenticatedSignIn = false;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; 
})
.AddCookie(options =>
{
    options.AccessDeniedPath = "/Authentication/Login";
    options.LogoutPath = "/Authentication/Login";
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.LoginPath = "/Home/Index";
});

services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddScoped<ICurrentUserContext, CurrentUserContext>();
services.AddTransient<IAuthenticationService, AuthenticationService>();
services.AddTransient<IUserService, UserService>();

services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

services.AddScoped<IValidator<LoginViewModel>, LoginViewModelValidator>();
services.AddScoped<IValidator<RegistrationViewModel>, RegistrationViewModelValidator>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
