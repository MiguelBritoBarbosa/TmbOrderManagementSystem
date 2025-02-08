using TmbOrderManagementSystem.Api;
using TmbOrderManagementSystem.Api.Orders;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Env.Load();
builder.Services.AddScoped<appDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var applyMigration = builder.Configuration.GetValue<bool>("APPLY_MIGRATION");

if (applyMigration)
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<appDbContext>();
        dbContext.Database.Migrate();
    }
}

// Orders Routes
app.AddOrdersRoutes();

app.Run();
