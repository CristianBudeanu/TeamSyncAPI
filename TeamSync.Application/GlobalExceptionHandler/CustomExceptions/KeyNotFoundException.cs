namespace TeamSync.Application.GlobalExceptionHandler.CustomExceptions
{
    public class KeyNotFoundException : Exception
    {
        public KeyNotFoundException(string message) : base(message) { }
    }
}
