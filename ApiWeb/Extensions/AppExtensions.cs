using ApiWeb.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWeb.Extensions
{
    public static class AppExtensions
    {
        public static void UseErrorHandlingMiddlerware(this IApplicationBuilder application)
        {
            application.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
