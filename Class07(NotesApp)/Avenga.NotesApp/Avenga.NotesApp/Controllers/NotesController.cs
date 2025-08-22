using Avenga.NotesApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Avenga.NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        // Контролерот добива INoteService преку конструктор.

        // DI(што го конфигуриравме во DependencyInjectionHelper) ќе даде NoteService, кој пак работи со репозиториуми и DbContext.
        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }
    }
}
