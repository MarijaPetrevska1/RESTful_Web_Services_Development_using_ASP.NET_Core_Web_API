using Homework2.Models;

namespace Homework2
{
    public static class StaticDb
    {
        public static List<Book> Books = new List<Book>
        {
            new Book { Author = "Fyodor Dostoevsky", Title = "Demons" },
            new Book { Author = "Ernest Hemingway", Title = "The Old Man and the Sea" },
            new Book { Author = "Jules Verne", Title = "From the Earth to the Moon" }
        };
    }
}

