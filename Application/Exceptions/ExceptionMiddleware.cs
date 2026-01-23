public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // proceed to next middleware
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = _env.IsDevelopment() ? ex.Message : "Internal Server Error"
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
