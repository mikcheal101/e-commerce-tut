using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// add redis to the project
builder.Services.AddStackExchangeRedisCache(options => 
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title="Basket.Api", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket Api V1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();

