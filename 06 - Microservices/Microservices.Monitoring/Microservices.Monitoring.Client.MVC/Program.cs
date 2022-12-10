using Microservices.Monitoring.Client.MVC.Infrastructure.Agents.Products.Commands;
using Microservices.Monitoring.Client.MVC.Infrastructure.Agents.Products;
using Microsoft.Extensions.Configuration;
using Steeltoe.Discovery.Client;
using Steeltoe.Extensions.Configuration;
using Microservices.Monitoring.Client.MVC.Application;
using Elastic.Apm.NetCoreAll;
using Steeltoe.Extensions.Configuration.ConfigServer;

var builder = WebApplication.CreateBuilder(args)
.AddConfigServer();

// Add services to the container.
builder.Services.AddDiscoveryClient(builder.Configuration);
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ListCommand>();
builder.Services.AddTransient<IProductsClient, ProductsClient>();
builder.Services.AddTransient<IProductsAgent, ProductsAgent>();
builder.Services.AddTransient<IProductApplicationService, ProductApplicationService>();

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAllElasticApm(builder.Configuration);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
