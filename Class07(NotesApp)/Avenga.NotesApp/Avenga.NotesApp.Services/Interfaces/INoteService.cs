using Avenga.NotesApp.Dtos.NoteDtos;

// Service sloj => Biznis Logika (validacii, pravila, mapiranje DTO <-> DTO models)

namespace Avenga.NotesApp.Services.Interfaces
{
    // Ova e kontrakt za NoteService
    // Kontrolerite ke zavisat od interfejs, a ne od konkretna klasa ->
    // ova e vazen princip (Dependency Inversion Principle - DIP)
    public interface INoteService
    {
        List<NoteDto> GetAllNotes();
        NoteDto GetById(int id);
        void AddNote(AddNoteDto note);
        void UpdateNote(UpdateNoteDto note);
        void DeleteNote(int id);
    }
}
