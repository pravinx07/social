using System.Net;
using System.Text.Json;

namespace backend.Middlewares

{
    public class GlobalExceptionMiddleware
    {
        // requestDelegate is the "next layer" in the funnel.
        // Our manager needs to know where to send the customer 

        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;

        }
        // Invoke Async is the method that automatically runs for Every Http request

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // 1. Manager Says: "Okay, pass the request to the controller 
                await _next(context);


            }
            catch (Exception ex)
            {
                // 1. Manager says : "Oh no! A contoller crashed I will catch it 
                // set the status code to 500 (Internal Server error)


                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                context.Response.ContentType = "application/json";

                // create a clean , safe error message for the user 
                var response = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Internal Server Error. Please try again",

                    // in a real app , your might only show ex.Message is develoement mode not productuon!
                    DetailedError = ex.Message

                };

                // Convert our safe message to Json and send it back to the user

                var jsonResponse = JsonSerializer.Serialize(response);
await context.Response.WriteAsync(jsonResponse);
            }
        }

    }

}