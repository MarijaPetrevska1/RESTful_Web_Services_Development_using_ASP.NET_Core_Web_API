using Avenga.NotesApp.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Injection
DependencyInjectionHelper.InjectDbContext(builder.Services);
DependencyInjectionHelper.InjectRepositories(builder.Services); // registrira konkretni repozitoriumi za Note i User
DependencyInjectionHelper.InjectServices(builder.Services); // Registrira service sloj : INoteService -> NoteService

// AddTransient значи дека секој пат кога ќе се побара, DI креира нова инстанца.

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
