using TmbOrderManagementSystem.Api;
using TmbOrderManagementSystem.Api.Orders;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(option => option.AddDefaultPolicy(policy =>
{
    policy.AllowAnyOrigin();
    policy.AllowAnyMethod();
    policy.AllowAnyHeader();
}));

Env.Load();
builder.Services.AddScoped<appDbContext>();
builder.Services.AddSingleton<ServiceBusHelper>();
builder.Services.AddScoped<OrderServiceBusConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

var applyMigration = builder.Configuration.GetValue<bool>("APPLY_MIGRATION");
if (applyMigration)
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<appDbContext>();
        dbContext.Database.Migrate();
    }
}

app.AddOrdersRoutes();

using (var scope = app.Services.CreateScope())
{
    var serviceBusConsumer = scope.ServiceProvider.GetRequiredService<OrderServiceBusConsumer>();
    var cancellationToken = app.Lifetime.ApplicationStopping;
    await serviceBusConsumer.StartAsync(cancellationToken);
}

app.Run();
