using Microservice.Persistence.EFCore.Data.Contexts;
using Microservice.Persistence.EFCore.Data.Repositories;
using Microservice.Persistence.NoSQL.Cosmo.EFCore.Data.Contexts;
using Microservice.Persistence.NoSQL.Cosmo.EFCore.Data.Init;
using Microservice.Persistence.NoSQL.Cosmo.EFCore.Data.Repositories;
using Microservice.Persistence.NoSQL.Mongo.Data.Settings;
using Microservice.Persistence.NoSQL.Mongo.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

////////////////////////////////// EF CORE ////////////////////////////////////////////////////
string conn = builder.Configuration.GetConnectionString("Microservice.Persistence.EFCoreDB");
//string conn = Configuration.GetConnectionString("Microservice.Persistence.EFCoreDB.azure");
builder.Services.AddDbContext<MicroservicePersistenceEFcoreContext>(opt => { opt.UseSqlServer(conn).LogTo(Console.WriteLine); });
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

////////////////////////////////// MONGODB ////////////////////////////////////////////////////
builder.Services.Configure<BookstoreDatabaseSettings>(builder.Configuration.GetSection(nameof(BookstoreDatabaseSettings)));
builder.Services.AddSingleton<IBookstoreDatabaseSettings>(sp =>sp.GetRequiredService<IOptions<BookstoreDatabaseSettings>>().Value);
builder.Services.AddSingleton<IBookService, BookService>();

////////////////////////////////// COSMODB ////////////////////////////////////////////////////
builder.Services.AddDbContext<OrderNoSqlContext>(opt =>
{
    opt.UseCosmos("https://usrsqlcosmopersistence.documents.azure.com:443/",
        "mCM9XMHczEkagcKR6KZZi7fuu8dRHHRigo3MZFmDVty2nrPUCyPq8ToF675GX8CNOfDpSD8ScX2AtlBwyBgJHQ==",
        databaseName: "OrdersDB");
});
builder.Services.AddScoped<IOrderNoSqlRepository, OrderNoSqlRepository>();
///////////////////////////////////////////////////////////////////////////////////////////////

builder.Services.AddControllers();

/********************************************************************************************************************************/
var app = builder.Build();
/********************************************************************************************************************************/

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

SeedDatabase();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<OrderNoSqlContext>();
        DataSeeder.SeedOrders(context);
    }
}