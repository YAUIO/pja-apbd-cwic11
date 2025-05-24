namespace pja_apbd_cwic11.Exceptions;

public class MaximumSizeExceededException : Exception
{
    public MaximumSizeExceededException(string entityName, int limit) : base("Maximum size for " + entityName +
                                                                             " exceeded. Limit is " + limit)
    {
    }
}