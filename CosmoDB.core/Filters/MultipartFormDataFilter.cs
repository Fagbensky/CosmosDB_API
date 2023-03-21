using CosmoDB.core.Models.viewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Filters
{
    public class MultipartFormDataFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.HasFormContentType)
            {
                context.Result = new JsonResult(new ReturnVM<string>
                {
                    ResponseCode = ResponseStatus.ResponseStatus.exception.code,
                    ResponseDescription = ResponseStatus.ResponseStatus.exception.message,
                    ResponseData = "Request is not a valid Form Data"
                })
                { StatusCode = StatusCodes.Status415UnsupportedMediaType };
            }
        }
    }
}
