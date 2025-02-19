namespace BlindBoxSystem.Common.Exceptions
{
    public static class CustomExceptions
    {
        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message) { }
        }

    }
}
