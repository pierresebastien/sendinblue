using System;
namespace Sendinblue.Api
{
    public class IllegalArgumentException : Exception
    {
        public IllegalArgumentException(string message):base(message)
        {

        }
        public IllegalArgumentException()
        {
        }
    }
}
