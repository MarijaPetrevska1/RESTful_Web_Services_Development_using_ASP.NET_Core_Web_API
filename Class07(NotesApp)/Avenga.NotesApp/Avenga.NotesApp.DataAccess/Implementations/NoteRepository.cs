using Avenga.NotesApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Avenga.NotesApp.DataAccess.Implementations
{
    // Repositorium za rabota so Notes
    // => Sekoj metod pravi EF Core operacija + .SaveChanges() za da se zapishi vo bazata.

    public class NoteRepository : IRepository<Note>
    {

        private NotesAppDbContext _notesAppDbContext;

        public NoteRepository(NotesAppDbContext notesAppDbContext) 
        {
            _notesAppDbContext = notesAppDbContext;        
        }

        public void Add(Note entity)
        {
            _notesAppDbContext.Notes.Add(entity);
            _notesAppDbContext.SaveChanges(); // call to db
        }

        public void Delete(Note entity)
        {
            _notesAppDbContext.Notes.Remove(entity);
            _notesAppDbContext.SaveChanges(); //call to db
        }

        // GetAll и GetById користат .Include(x => x.User) → значи кога ќе ги земеш белешките,
        // автоматски се вчитува и поврзаниот User.
        public List<Note> GetAll()
        {
            // Include will join note and user tables and include User data in the query result aswell
            return _notesAppDbContext.Notes.Include(x => x.User).ToList();
        }

        public Note GetById(int id)
        {
            return _notesAppDbContext.Notes.Include(x => x.User).FirstOrDefault(x => x.Id == id);
        }

        public void Update(Note entity)
        {
            _notesAppDbContext.Notes.Update(entity);
            _notesAppDbContext.SaveChanges(); // call to db
        }
    }
}
