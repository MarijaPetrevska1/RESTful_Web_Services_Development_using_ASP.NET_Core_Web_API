using Avenga.NotesApp.Domain.Enums;
using Avenga.NotesApp.Dtos.NoteDtos;

namespace Avenga.NotesApp.Dtos.NoteDtos
{
    public class AddNoteDto
    {
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public Tag Tag { get; set; }
        public int UserId { get; set; }
    }
}


// AddNoteDto → влезни податоци за POST /notes.