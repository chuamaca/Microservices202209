using Microservices.Resilient.Hystrix.Client.MVC.Infrastructure.Agents.Orders.Commands;
using Microservices.Resilient.Hystrix.Client.MVC.Infrastructure.Agents.Orders;
using Microsoft.Extensions.Configuration;
using Steeltoe.CircuitBreaker.Hystrix;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IOrderClient, OrderClient>();
builder.Services.AddTransient<IOrdersAgent, OrdersAgent>();
builder.Services.AddHystrixCommand<ListCommand>("OrdersAgentGroup", "ListCommand", builder.Configuration);
builder.Services.AddHystrixCommand<GetCommand>("OrdersAgentGroup", "GetCommand", builder.Configuration);
builder.Services.AddHystrixMetricsStream(builder.Configuration);

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseHystrixMetricsStream();

app.Run();
