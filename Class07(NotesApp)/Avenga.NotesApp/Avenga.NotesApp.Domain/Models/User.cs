using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// DOMAIN PROEKTOT E OSNOVATA NA APLIKACIJATA. Tuka se definiraat entitetite i enumeraciite, bez nikakva logika za baza i web api.
// Domain se koristi od site drugi sloevi (DataAccess, Services, Web).
namespace Avenga.NotesApp.Domain.Models
{
    // Ova e tabela Users vo bazata, so One-To-Many vrska kon Notes
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // primaren kluc -> Id
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Username { get; set; }
        public List<Note> Notes { get; set; } // Navigation property, eden korisnik moze da ima povekje beleseki (Notes)
    }
}
