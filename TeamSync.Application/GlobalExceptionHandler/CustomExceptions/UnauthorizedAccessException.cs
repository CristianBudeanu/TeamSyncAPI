namespace TeamSync.Application.GlobalExceptionHandler.CustomExceptions
{
    public class UnauthorizedAccessException : Exception
    {
        public UnauthorizedAccessException(string message) : base(message) { }
    }
}
