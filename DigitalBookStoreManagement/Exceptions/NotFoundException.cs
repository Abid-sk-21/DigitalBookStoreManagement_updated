using System.Security.Cryptography.X509Certificates;

namespace DigitalBookStoreManagement.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
       
    }
}
