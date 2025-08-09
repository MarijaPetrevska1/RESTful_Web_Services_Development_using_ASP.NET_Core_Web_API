using Microsoft.AspNetCore.Server.Kestrel.Core; // �� ������������ �� Kestrel ��������

var builder = WebApplication.CreateBuilder(args); // �� �������� builder-�� �� �����������

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// ������������ �� Swagger (������ �� API ������������)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// ���������� �������� ������ �� Request.Body
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});
// �� ����� ��������� app �����
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger(); // ���������� ���������� Swagger JSON
    app.UseSwaggerUI();  // ���������� Swagger UI �� ��������� API
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
app.Run();
