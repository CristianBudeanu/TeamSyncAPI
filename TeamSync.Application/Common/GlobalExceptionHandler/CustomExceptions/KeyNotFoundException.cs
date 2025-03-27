namespace TeamSync.Application.Common.GlobalExceptionHandler.CustomExceptions
{
    public class KeyNotFoundException : Exception
    {
        public KeyNotFoundException(string message) : base(message) { }
    }
}
