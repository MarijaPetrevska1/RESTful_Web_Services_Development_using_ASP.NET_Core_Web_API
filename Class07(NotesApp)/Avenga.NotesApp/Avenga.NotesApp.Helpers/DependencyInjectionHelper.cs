using Avenga.NotesApp.DataAccess;
using Avenga.NotesApp.DataAccess.Implementations;
using Avenga.NotesApp.Domain.Models;
using Avenga.NotesApp.Services.Implementations;
using Avenga.NotesApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
// vo Helpers se pravi Dependency Injection (DI) konfiguracijata
// vo ASP.NET Core se' funkcionira preku DI
// toa e zicata sto gi spojuva site sloevi
// go olesnuva Program.cs, namesto da pisuvame tamu, imame uredna klasa so metodi za injection
namespace Avenga.NotesApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        // === InjectDbContext
        // Регистрира EF Core DbContext (NotesAppDbContext) во DI контейнерот.
        // Користи UseSqlServer → што значи дека базата е MS SQL.
        public static void InjectDbContext(IServiceCollection services) 
        {
            services.AddDbContext<NotesAppDbContext>(x =>
            x.UseSqlServer("Server=.;Database=NotesAppDatabase;Trusted_Connection=True;TrustServerCertificate=True"));
        }
        // === InjectRepositories
        // Викаме на IRepository<Note> → секогаш кога ќе се побара, DI ќе даде NoteRepository.
        // Истото важи и за UserRepository.
        // AddTransient значи дека секој пат кога ќе се побара нова инстанца, ќе се креира нова.
        public static void InjectRepositories(IServiceCollection services) 
        {
            services.AddTransient<IRepository<Note>, NoteRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();
        }

        // === InjectServices
        // Регистрира INoteService со имплементацијата NoteService.
        // Исто како кај репозиториумите → со AddTransient.
        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<INoteService, NoteService>();
        }
    }
}
