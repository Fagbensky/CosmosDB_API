using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using CosmoDB.core.Models.DTOs;
using CosmoDB.core.Models.viewModels;
using Microsoft.AspNetCore.Http;
using CosmoDB.core.Exceptions;
using CosmoDB.core.Logger;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace CosmoDB.core.Filters
{
    public class ExceptionFilter: IExceptionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly LogWriter _logger;
        public ExceptionFilter(IHostEnvironment hostEnvironment, LogWriter logger)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }
        public void OnException(ExceptionContext context)
        {

            _logger.WriteLog($"An error occured: {context.Exception.Message}. \n Details: {JsonConvert.SerializeObject(context.Exception)}", LogType.LOG_INFORMATION);
            context.Result = new JsonResult(new ReturnVM<string>
            {
                ResponseCode = ResponseStatus.ResponseStatus.exception.code,
                ResponseDescription = ResponseStatus.ResponseStatus.exception.message,
                ResponseData = "A system error occured, please check back later"
            })
            { StatusCode = StatusCodes.Status500InternalServerError };

            if (context.Exception.GetType() == typeof(ErrorOccuredShortCircuitException))
            {
                context.Result = new JsonResult(new ReturnVM<string>
                {
                    ResponseCode = ResponseStatus.ResponseStatus.badRequest.code,
                    ResponseDescription = ResponseStatus.ResponseStatus.badRequest.message,
                    ResponseData = context.Exception.Message.ToString()
                })
                { StatusCode = StatusCodes.Status400BadRequest };
            }
        }
    }
}
