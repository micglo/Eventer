namespace Eventer.Utility.CustomException
{
    public interface ICustomException
    {
        void ThrowNotFoundException(string message);
        void ThrowBadRequestException(string message);
    }
}