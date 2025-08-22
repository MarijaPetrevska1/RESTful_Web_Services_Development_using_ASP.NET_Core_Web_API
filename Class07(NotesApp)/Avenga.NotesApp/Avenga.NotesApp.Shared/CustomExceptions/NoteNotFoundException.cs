namespace Avenga.NotesApp.Shared.CustomExceptions
{
    public class NoteNotFoundException : Exception
    {
        public NoteNotFoundException(string message) :base(message) { }
    }
}


//Ги одвојуваат различните типови грешки → validation vs not found.

//Појасна и чиста логика во сервис слојот → сервисот само фрла exceptions, контролерот решава како да одговори на клиентот.

//Лесно се тестираат (unit testing).