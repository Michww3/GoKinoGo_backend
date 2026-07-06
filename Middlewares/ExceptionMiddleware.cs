using GoKinoGo.Exceptions;

namespace GoKinoGo.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await Handle(context, ex);
        }
    }

    private static async Task Handle(
        HttpContext context,
        Exception ex)
    {
        context.Response.ContentType = "application/json";

        switch (ex)
        {
            case ApiException api:
                context.Response.StatusCode = api.StatusCode;

                await context.Response.WriteAsJsonAsync(new
                {
                    message = api.Message
                });

                break;

            default:

                context.Response.StatusCode = 500;

                await context.Response.WriteAsJsonAsync(new
                {
                    message = "Internal server error."
                });

                break;
        }
    }
}
