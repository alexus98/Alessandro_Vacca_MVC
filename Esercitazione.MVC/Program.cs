using Esercitazione.Core.BusinessLayer;
using Esercitazione.Core.InterfaceRepositories;
using Esercitazione.Core.RepositoryEF;
using Esercitazione.Core.RepositoryEF.RepositoryEF;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IBusinessLayer, MainBusinessLayer>();

builder.Services.AddScoped<IPiattoRepository, RepositoryPiattiEF>();
builder.Services.AddScoped<IMenuRepository, RepositoryMenuEF>();
builder.Services.AddScoped<IUtenteRepository, RepositoryUtentiEF>();

builder.Services.AddDbContext<MasterContext>(options =>
{
    options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RistoranteDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    option =>
    {
        option.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Utenti/Login");
        option.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Utenti/Forbidden");
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Ristoratore", policy => policy.RequireRole("Ristoratore"));
    options.AddPolicy("Cliente", policy => policy.RequireRole("Cliente"));
});

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
