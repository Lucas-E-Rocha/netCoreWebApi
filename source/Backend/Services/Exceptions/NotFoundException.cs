namespace netCoreWebApi.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(String message) : base(message)
        {

        }
    }
}
