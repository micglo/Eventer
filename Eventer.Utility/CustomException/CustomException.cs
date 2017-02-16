using System;

namespace Eventer.Utility.CustomException
{
    public class CustomException : ICustomException
    {
        public void ThrowNotFoundException(string message)
        {
            throw new NotFoundException($"{message}");
        }

        public void ThrowBadRequestException(string message)
        {
            throw new BadRequestException($"{message}");
        }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception ex) : base(message, ex) { }
    }

    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
        public BadRequestException(string message, Exception ex) : base(message, ex) { }
    }
}