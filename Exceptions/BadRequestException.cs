namespace GoKinoGo.Exceptions;

public class BadRequestException(string message) : ApiException(message)
{
    public override int StatusCode => StatusCodes.Status400BadRequest;
}
