namespace TeamSync.Application.GlobalExceptionHandler.CustomExceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
