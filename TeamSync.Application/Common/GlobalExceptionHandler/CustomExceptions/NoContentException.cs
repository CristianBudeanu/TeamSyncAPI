namespace TeamSync.Application.Common.GlobalExceptionHandler.CustomExceptions
{
    public class NoContentException : Exception
    {
        public NoContentException(string message) : base(message) { }
    }
}
