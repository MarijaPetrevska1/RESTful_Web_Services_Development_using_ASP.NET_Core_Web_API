using Homework2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // ==== GET method that returns all books ====
        // GET: api/books
        [HttpGet]
        public ActionResult<List<Book>> GetAllBooks()
        {
            try
            {
                return Ok(StaticDb.Books);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred! Contact the admin. {e.Message}");
            }
        }

        // ====  GET method that returns one book by sending index in the query string ====
        // GET: api/books/getByIndex?index=1
        [HttpGet("getByIndex")]
        public ActionResult<Book> GetBookByIndex(int? index)
        {
            try
            {
                if (index == null)
                {
                    return BadRequest("Index is required.");
                }

                if (index < 0)
                {
                    return BadRequest("Index cannot be negative.");
                }

                if (index >= StaticDb.Books.Count)
                {
                    return NotFound($"No book found at index {index}");
                }

                return Ok(StaticDb.Books[index.Value]);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred! Contact the admin. {e.Message}");
            }
        }

        // ==== GET method that returns one book by filtering by author and title (use query string parameters) ====
        // GET: api/books/filter?author=verne&title=sea
        [HttpGet("filter")]
        public ActionResult<Book> GetBookByAuthorAndTitle(string? author, string? title)
        {
            try
            {
                if (string.IsNullOrEmpty(author) || string.IsNullOrEmpty(title))
                {
                    return BadRequest("Both author and title are required.");
                }

                var book = StaticDb.Books.FirstOrDefault(b =>
                    b.Author.ToLower().Contains(author.ToLower()) &&
                    b.Title.ToLower().Contains(title.ToLower()));

                if (book == null)
                {
                    return NotFound("Book not found with the given author and title.");
                }

                return Ok(book);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred! Contact the admin. {e.Message}");
            }
        }

        // ==== POST method that adds new book to the list of books (use the FromBody attribute) ====
        // POST: api/books
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            try
            {
                if (string.IsNullOrEmpty(newBook.Author) || string.IsNullOrEmpty(newBook.Title))
                {
                    return BadRequest("Author and Title cannot be empty.");
                }

                StaticDb.Books.Add(newBook);
                return StatusCode(StatusCodes.Status201Created, "Book added successfully.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred! Contact the admin. {e.Message}");
            }
        }

        // Bonus Requirements: 
        // ==== Add POST method that accepts list of books from the body of the request and returns their titles as a list of strings. ====
        // BONUS: POST: api/books/titles
        [HttpPost("titles")]
        public ActionResult<List<string>> GetTitlesFromBooks([FromBody] List<Book> books)
        {
            try
            {
                if (books == null || books.Count == 0)
                {
                    return BadRequest("List of books is required.");
                }

                StaticDb.Books.AddRange(books);

                List<string> titles = books.Select(b => b.Title).ToList();
                return Ok(titles);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred! Contact the admin. {e.Message}");
            }
        }
    }
}
