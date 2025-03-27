namespace TeamSync.Application.Common.GlobalExceptionHandler.CustomExceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
