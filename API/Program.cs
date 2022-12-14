using Api.Services;
using API.Data;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using API.Repository;
using API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<TokenService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
builder.Services.AddSingleton<EmailSender>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

builder.Services.AddAccountServices(builder.Configuration);

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(
      policy =>
      {
        policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000", "https://habits-r8zcfxooi-leandrobueno.vercel.app", "https://habits-eight.vercel.app");
      });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();
app.UseForwardedHeaders();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("index.html");

try
{
  using var serviceScope = app.Services.CreateScope();
  var context = serviceScope.ServiceProvider.GetService<DataContext>()!;
  await context.Database.MigrateAsync();
  var logger = serviceScope.ServiceProvider.GetService<ILogger<Program>>()!;
  logger.LogInformation("Database seeding complete!");

}
catch (Exception ex)
{

  var logger = app.Services.GetRequiredService<ILogger<Program>>();
  logger.LogError(ex, "An error occured during migration");
}

app.UseSpa(spa =>
{
  if (builder.Environment.IsDevelopment())
  {
    spa.Options.SourcePath = "../Client/dist";
    // spa.UseProxyToSpaDevelopmentServer("http://127.0.0.1:5173/");
  }
});

app.Run();
