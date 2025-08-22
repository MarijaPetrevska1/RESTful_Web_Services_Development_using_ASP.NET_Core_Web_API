using Avenga.NotesApp.Domain.Models;
using Avenga.NotesApp.Dtos.NoteDtos;

// Mapper class => која служи за конверзија помеѓу Domain модели и DTOs.
namespace Avenga.NotesApp.Mappers
{
    public static class NoteMapper
    {
        //Extension method за Note → NoteDto.

        //Кога земаш Note од база, го претвораш во DTO за API да врати „кориснички“ информации.

        //UserFullName комбинира FirstName + LastName.
        public static NoteDto ToNoteDto(this Note note)
        {
            return new NoteDto
            {
                Tag = note.Tag,
                Priority = note.Priority,
                Text = note.Text,
                UserFullName = $"{note.User.FirstName} {note.User.LastName}",
            };
        }

        //Extension method за AddNoteDto → Note.
        
        //Служи за запишување на нова белешка во база.

        //Мапира само полиња што се добиваат од клиентот.
        public static Note ToNote(this AddNoteDto addNoteDto) 
        {
            return new Note()
            {
                Text = addNoteDto.Text,
                Priority = addNoteDto.Priority,
                Tag = addNoteDto.Tag,
                UserId = addNoteDto.UserId,
            };
        }

        //Extension method за UpdateNoteDto + постоечка Note → Note.

        //Се користи кога сакаме да апдејтираме белешка: земаме Note од база(noteDb) и го пополнуваме со новите вредности.

        //Се избегнува креирање на нов објект, за да EF Core препознае дека се работи за постоечка ентитет.
        public static Note ToNote(this UpdateNoteDto updateNoteDto, Note noteDb)
        {
            noteDb.Text = updateNoteDto.Text;
            //....
            return noteDb;
        }
    }
}
