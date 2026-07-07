namespace GoKinoGo.Exceptions;

public class UnauthorizedException(string message) : ApiException(message)
{
    public override int StatusCode => StatusCodes.Status401Unauthorized;
}
