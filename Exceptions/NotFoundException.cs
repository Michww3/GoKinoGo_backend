namespace GoKinoGo.Exceptions;

public class NotFoundException(string message) : ApiException(message)
{
    public override int StatusCode => StatusCodes.Status404NotFound;
}
