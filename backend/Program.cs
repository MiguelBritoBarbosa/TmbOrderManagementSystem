using TmbOrderManagementSystem.Api;
using TmbOrderManagementSystem.Api.Orders;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(option => option.AddDefaultPolicy(policy =>
{
    policy.SetIsOriginAllowed(_ => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
}));

Env.Load();
builder.Services.AddScoped<appDbContext>();
builder.Services.AddSingleton<ServiceBusHelper>();
builder.Services.AddScoped<OrderServiceBusConsumer>();
builder.Services.AddSignalR();

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
app.MapHub<OrderHub>("/orderHub");

using (var scope = app.Services.CreateScope())
{
    var serviceBusConsumer = scope.ServiceProvider.GetRequiredService<OrderServiceBusConsumer>();
    var cancellationToken = app.Lifetime.ApplicationStopping;
    await serviceBusConsumer.StartAsync(cancellationToken);
}

app.Run();
