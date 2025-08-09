using Microsoft.AspNetCore.Server.Kestrel.Core; // за конфигурација на Kestrel серверот

var builder = WebApplication.CreateBuilder(args); // го стартува builder-от за апликацијата

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Конфигурација за Swagger (алатка за API документација)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Овозможува синхроно читање на Request.Body
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});
// Го гради финалниот app објект
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger(); // Овозможува генерирање Swagger JSON
    app.UseSwaggerUI();  // Овозможува Swagger UI за тестирање API
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
app.Run();
