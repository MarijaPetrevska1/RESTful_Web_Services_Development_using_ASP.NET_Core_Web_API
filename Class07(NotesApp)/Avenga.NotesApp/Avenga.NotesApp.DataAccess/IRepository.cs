namespace Avenga.NotesApp.DataAccess
{
    // Generic interface za CRUD operacii 
    // Dogovor za CRUD
    // Ja definira strukturata za bilo koj repositorium (NoteRepository, UserRepository, ... itn)
    // Зошто? => Separation of Concerns → контролерите/сервисите ќе работат преку интерфејс, без да знаат што има внатре.
    public interface IRepository<T>
    {
        //CRUD 
        List<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
