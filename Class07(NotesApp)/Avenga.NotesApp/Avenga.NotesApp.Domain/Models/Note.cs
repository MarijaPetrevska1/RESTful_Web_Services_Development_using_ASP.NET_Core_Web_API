using Avenga.NotesApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avenga.NotesApp.Domain.Models
{
    // Ova e entitet sto pretstavuva eden Note(beleska)
    // MODEL ZA BAZA (Ke se mapira vo Tabela Notes)
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // primaren kluc, se generira avtomatski
        public string Text { get; set; }
        public Priority Priority { get; set; } // vrska so Priority enum
        public Tag Tag { get; set; } // vrska so Tag enum
        public int UserId { get; set; } // Foreign KEY kon korisnikot koj ja kreiral Note
        public User User { get; set; } // Navigation property za EF Core(relacija Note -> User)
    }
}
