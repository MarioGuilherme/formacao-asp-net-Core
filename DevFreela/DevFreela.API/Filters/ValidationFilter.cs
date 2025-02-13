﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DevFreela.API.Filters;

public class ValidationFilter : IActionFilter {
    public void OnActionExecuted(ActionExecutedContext context) {}

    public void OnActionExecuting(ActionExecutingContext context) {
        if (!context.ModelState.IsValid) {
            IEnumerable<string> messages = context.ModelState
                   .SelectMany(ms => ms.Value.Errors)
                   .Select(e => e.ErrorMessage);

            context.Result = new BadRequestObjectResult(messages);
        }
    }
}