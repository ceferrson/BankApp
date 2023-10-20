using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using BankApp.Web.Data.Contexts;
using BankApp.Web.Interfaces;
using BankApp.Web.Repositories;
using BankApp.Web.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BankContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUow, Uow>();

var app = builder.Build();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
    RequestPath = "/node_modules"
});

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
