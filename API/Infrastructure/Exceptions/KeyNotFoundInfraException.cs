namespace API.Infrastructure.Exceptions
{
    public class KeyNotFoundInfraException : Exception
    {
        public KeyNotFoundInfraException(string message) : base(message)
        {
        }
    }
}