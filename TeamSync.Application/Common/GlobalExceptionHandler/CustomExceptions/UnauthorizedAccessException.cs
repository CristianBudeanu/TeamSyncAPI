namespace TeamSync.Application.Common.GlobalExceptionHandler.CustomExceptions
{
    public class UnauthorizedAccessException : Exception
    {
        public UnauthorizedAccessException(string message) : base(message) { }
    }
}
