namespace pja_apbd_cwic11.Exceptions;

public class KeyExistsException : Exception
{
    public KeyExistsException(string entityName) : base(entityName + " with such id already exists")
    {
    }
}