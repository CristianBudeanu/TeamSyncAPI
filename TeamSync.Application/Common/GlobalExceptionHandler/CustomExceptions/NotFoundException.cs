namespace TeamSync.Application.Common.GlobalExceptionHandler.CustomExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
