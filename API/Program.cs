using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
builder.Services.AddCors();

// Register Services (The recommended for HTTP Reqs is AddScoped => for the lifetime of the HTTP Req is alive) 
builder.Services.AddScoped<ITokenService, TokenService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// Connection string to connect with Database 
string ConnectionStr = builder.Configuration.GetConnectionString("Default");

// Add DbContext
builder.Services.AddDbContext<DataContext>(options => options.UseMySql(ConnectionStr, ServerVersion.AutoDetect(ConnectionStr)));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

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

app.UseAuthorization();

app.MapControllers();

app.Run();
