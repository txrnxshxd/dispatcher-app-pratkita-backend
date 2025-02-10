using DispatcherApp.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoConnection"));

builder.Services.AddScoped<RadarService>();
builder.Services.AddScoped<CountriesService>();
builder.Services.AddScoped<CitiesService>();
builder.Services.AddScoped<FlightsHistoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
    await next.Invoke();
});

app.UseRouting();

app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();


app.MapControllers();

app.Run();
