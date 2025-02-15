namespace API.Exceptions
{
    public class HeatException : Exception
    {
        public HeatException() { }

        public HeatException(string message)
            : base(message) { }

        public HeatException(string message, Exception inner)
            : base(message, inner) { }
    }
}
