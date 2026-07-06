namespace GoKinoGo.Exceptions;

public class ConflictException(string message) : ApiException(message)
{
    public override int StatusCode => StatusCodes.Status409Conflict;
}
