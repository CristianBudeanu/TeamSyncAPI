namespace TeamSync.Application.GlobalExceptionHandler.CustomExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
