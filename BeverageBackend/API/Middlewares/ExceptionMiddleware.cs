using BeverageBackend.Application.Exceptions;

namespace BeverageBackend.API.Middlewares
{
    public class ExceptionMiddleware
    {
        public RequestDelegate _next { get; set; }
        public ILogger<ExceptionMiddleware> _logger { get; set; }

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            int statusCode = StatusCodes.Status500InternalServerError;
            string message = "Something went wrong";
            string errorCode = "InternalServerError";

            if(ex is AppException appEx)
            {
                statusCode = appEx.StatusCode;
                message = appEx.Message;
                errorCode = appEx.ErrorCode;
            }

            context.Response.StatusCode = statusCode;

            var response = new
            {
                success = false,
                statusCode,
                errorCode,
                message
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
