namespace GoKinoGo.Exceptions;

public class ForbiddenException(string message) : ApiException(message)
{
    public override int StatusCode => StatusCodes.Status403Forbidden;
}
