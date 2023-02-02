using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testes.TryException.WebApi.Handlers
{
    internal static class ErrorHandlerExtensions
    {
        public static IApplicationBuilder UseCustomErrorHandler(this IApplicationBuilder app) =>
            app.UseMiddleware<ErrorHandlerMiddleware>();
    }
}
