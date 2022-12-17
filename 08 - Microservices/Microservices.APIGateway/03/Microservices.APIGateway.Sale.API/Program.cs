var builder = WebApplication.CreateBuilder(args);

string allowAllOrigins = "_allowAllOrigins";
builder.Services.AddCors(options => options.AddPolicy(allowAllOrigins, builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
}));
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors(allowAllOrigins);
app.UseAuthorization();
app.MapControllers();

app.Run();
