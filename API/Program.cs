using API.Data;
using API.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.


using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
   var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("https://localhost:4200"));


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
