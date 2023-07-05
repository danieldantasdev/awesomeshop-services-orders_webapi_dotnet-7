using AwesomeShop.Services.Orders.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatRExtension();
builder.Services.AddInfrastructureExtension();
builder.Services.AddMongoDbExtension();
builder.Services.AddRabbitMqExtension();
builder.Services.AddHttpClient();
builder.Services.AddConsulExtension(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.InjectStylesheet("/Styles/SwaggerUi/Dark.css");
    });
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseConsul();

app.MapControllers();

app.Run();