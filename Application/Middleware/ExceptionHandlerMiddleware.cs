using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        public ExceptionHandlerMiddleware(RequestDelegate next) => this.next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {            
            string result = "";

            switch (exception)
            {              
                case ValidationException validationException:
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                    foreach(var error in validationException.Errors)
                    {
                        result += $"{error}\n";
                    }

                    break;

                case UnauthorizedException unauthorizedException:
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    result = exception.Message;
                    break;

                case OperationForbiddenException operationForbidden:
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    result = exception.Message;
                    break;

                case NotFoundException notFoundException:
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    result = exception.Message;
                    break;

                default:
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    result = exception.Message;
                    break;
            }
          
            return context.Response.WriteAsync(result);
        }
    }
}