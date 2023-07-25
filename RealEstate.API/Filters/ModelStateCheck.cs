﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Helpers;
using System.Net;

namespace RealEstate.API.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

    public class ModelStateCheck : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                List<string> errorMessage = ListModelError(context);

                HttpContext httpContext = context.HttpContext;


                httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                await httpContext.Response.WriteAsJsonAsync(new JsonMessage<string>()
                {
                    status = false,
                    result = errorMessage,
                    status_code = (int)HttpStatusCode.NotImplemented,
                    error_message = "Invalid validaation"
                });
                return;
            }
            await next();
        }

        private List<string> ListModelError(ActionContext context) => context.ModelState.SelectMany(x => x.Value.Errors).
                                                                Select(x => x.ErrorMessage).ToList();
    }
}
