using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.MicrosoftExtensions;
using MoviesApplication.Dtos;
using MoviesApplication.Models;

namespace MoviesApplication.Controllers
{
    [Route("api/[controller]")] // URL ke e api/movies
    [ApiController] // => api controller и има автоматска валидација на [FromBody], [FromQuery] и слично
    public class MoviesController : ControllerBase
    {
        [HttpGet]   //api/movies
        public ActionResult<List<MovieDto>> Get()
        {
            try
            {
                // go zemame spisokot na filmovi od StaticDb
                var moviesDb = StaticDb.Movies;
                // gi vrakjame kako lista od MovieDto (DTO = Data Transfer Object) 
                return Ok(moviesDb.Select(x => new MovieDto
                { 
                    Description = x.Description,
                    Genre = x.Genre,
                    Title = x.Title,
                    Year = x.Year
                }));
            }
            catch (Exception e)
            {
                // dokolku ima greska vo serverot vrakjame 500
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        // ================== GET BY ID (route param) ==================

        [HttpGet("{id}")]   // GET api/movies/2
        public ActionResult<MovieDto> Get(int id)
        {
            try
            {
                // proverka dali Id e validno (ne smee da e 0 ili negativno)
                if(id<=0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad request, the id can not be negative!");
                }
                // baranje na filmot spored Id
                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                // ako nema film so toa Id vrakjame 404 Not Found
                if(movieDb==null)
                {
                    return NotFound("Movie was not found!");
                }
                // Ako ima, vrakjame DTO
                return Ok(new MovieDto
                {
                    Description = movieDb.Description,
                    Genre = movieDb.Genre,
                    Title = movieDb.Title,
                    Year = movieDb.Year
                });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
        // ================== GET BY ID (query string) ==================
        // Пример: api/movies/queryString?id=1

        [HttpGet("queryString")] //api/movies/queryString?index=1
        public ActionResult<MovieDto> GetById(int id)
        {
            try
            {
                if(id<=0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad request, the id can not be negative!");
                }
                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if(movieDb==null)
                {
                    return NotFound("Movie was not found!");
                }
                return Ok(new MovieDto
                {
                    Description=movieDb.Description,
                    Title=movieDb.Title,
                    Genre=movieDb.Genre,
                    Year=movieDb.Year
                });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        // ================== FILTER (genre/year) ==================
        // Пример: api/movies/filter?genre=1&year=2022

        [HttpGet("filter")]  //api/movies/filter?genre=1&year=2022  
        public ActionResult<List<MovieDto>> FilterNotesFromQuery(int? genre, int? year)
        {
            try
            {
                // ako ne pratime nieden parametar => 400 BadRequest
                if(genre==null && year==null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You have to send at least one filter parameter!");
                }

                //  Ако има genre, проверуваме дали е валидна вредност од enum (Action/Comedy)
                if (genre.HasValue)
                {
                    //validate if the value for genre is valid
                    var enumValues = Enum.GetValues(typeof(GenreEnum))
                                            .Cast<GenreEnum>()
                                            .Select(v => (int)v)
                                            .ToList();

                    if (!enumValues.Contains(genre.Value))
                    {
                        return BadRequest("Invalid genre value");
                    }
                }
                // Филтрирање само по жанр
                if(year==null)
                {
                    List<Movie> moviesDb = StaticDb.Movies.Where(x => x.Genre == (GenreEnum)genre).ToList();
                    return Ok(moviesDb.Select(x => new MovieDto
                    {
                        Description = x.Description,
                        Genre = x.Genre,
                        Title = x.Title,
                        Year = x.Year,
                    }));
                }
                // Филтрирање само по година
                if(genre==null)
                {
                    List<Movie> moviesDb = StaticDb.Movies.Where(x => x.Year == year).ToList();
                    return Ok(moviesDb.Select(x => new MovieDto
                    {
                        Description = x.Description,
                        Genre = x.Genre,
                        Title = x.Title,
                        Year = x.Year
                    }));
                }
                // Филтрирање и по година и по жанр
                List<Movie> movies = StaticDb.Movies.Where(x => x.Year == year && x.Genre == (GenreEnum)genre).ToList();
                return Ok(movies.Select(x => new MovieDto
                {
                    Description= x.Description,
                    Genre = x.Genre,
                    Title = x.Title,
                    Year = x.Year
                }));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
        // ================== UPDATE MOVIE ==================

        [HttpPut] // PUT api/movies
        public IActionResult UpdateMovie([FromBody] UpdateMovieDto movie)
        {
            try
            {
                // Го бараме филмот што сакаме да го апдејтираме

                Movie movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == movie.Id);
                if(movieDb==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Resource not found!");
                }
                // Валидација на полиња

                if (string.IsNullOrEmpty(movie.Title))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Text must not be empty");
                }
                if(movie.Year<=0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Year must not be negative");
                }
                if(!string.IsNullOrEmpty(movie.Description) && movie.Description.Length>250)
                {
                    return BadRequest("Description can not be longer than 250 characters");
                }

                // Доколку е во ред → апдејтираме тука

                movieDb.Year = movie.Year;
                movieDb.Title=movie.Title;
                movieDb.Description=movie.Description;
                movieDb.Genre=movie.Genre;
                return StatusCode(StatusCodes.Status204NoContent, "Note updated!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        // ================== DELETE MOVIE (id from body) ==================

        [HttpDelete]  // DELETE api/movies (id in body)
        public IActionResult DeleteMovie([FromBody] int id)
        {
            try
            {
                if(id<0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad request, the id can not be negative!");
                }
                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if(movieDb==null)
                {
                    return NotFound("Movie was not found!");
                }
                StaticDb.Movies.Remove(movieDb);

                return StatusCode(StatusCodes.Status204NoContent, "Deleted resource");

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        // ================== DELETE MOVIE (id from route) ==================
        [HttpDelete("{id}")]  // DELETE api/movies/2
        public IActionResult Delete(int id)
        {
            try
            {
                if (id < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad request, the id can not be negative!");
                }
                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if (movieDb == null)
                {
                    return NotFound("Movie was not found");
                }
                StaticDb.Movies.Remove(movieDb);

                return StatusCode(StatusCodes.Status204NoContent, "Deleted resource");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        // ================== ADD MOVIE ==================

        [HttpPost("addMovie")] // POST api/movies/addMovie
        public IActionResult AddMovie([FromBody] AddMovieDto movieDto)
        {
            try
            {
                // Валидација на полињата

                if (string.IsNullOrEmpty(movieDto.Title))
                {
                    return BadRequest("Title must not be empty");
                }
                if (!string.IsNullOrEmpty(movieDto.Description) && movieDto.Description.Length > 250)
                {
                    return BadRequest("Description can not be longer than 250 characters");
                }
                if (movieDto.Year <= 0)
                {
                    return BadRequest("Year can not have negative value");
                }
                
                // КРЕИРАЊЕ НА НОВ ФИЛМ
                Movie movie = new Movie()
                {
                    Year = movieDto.Year,
                    Title = movieDto.Title,
                    Genre = movieDto.Genre,
                    Description = movieDto.Description

                };

                movie.Id = ++StaticDb.MovieId;  // Автоматски доделуваме ново Id
                StaticDb.Movies.Add(movie); // Додаваме во StaticDb
                return StatusCode(StatusCodes.Status201Created); // Враќаме 201 Created
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
    }
}
