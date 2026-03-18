using OrderService.Clients;
using OrderService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<OrderServiceHandler>();

builder.Services.AddHttpClient<ICartServiceClient, CartServiceClient>(client => {
    client.BaseAddress = new Uri("https://localhost:7168");
});

builder.Services.AddHttpClient<IProductServiceClient, ProductServiceClient>(client => { 

    client.BaseAddress = new Uri("https://localhost:7097");
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
