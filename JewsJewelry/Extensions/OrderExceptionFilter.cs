using JewsJewelry.API.Exceptions;
using JewsJewelry.Services.Contracts.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JewsJewelry.API.Extensions
{
    public class OrderExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception as DefaultException;
            if (exception == null)
            {
                return;
            }

            switch (exception)
            {
                case ValidsException ex:
                    SetDataToContext(
                        new ConflictObjectResult(new APIExcValidation
                        {
                            Errors = ex.Errors,
                        }),
                        context);
                    break;

                case InvalidOpsException ex:
                    SetDataToContext(
                        new BadRequestObjectResult(new APIExc { Msg = ex.Message, })
                        {
                            StatusCode = StatusCodes.Status406NotAcceptable,
                        },
                        context);
                    break;

                case NtFoundException ex:
                    SetDataToContext(new NotFoundObjectResult(new APIExc
                    {
                        Msg = ex.Message,
                    }), context);
                    break;

                default:
                    SetDataToContext(new BadRequestObjectResult(new APIExc
                    {
                        Msg = $"Ошибка записи в БД (Проверьте индексы). {exception.Message}",
                    }), context);
                    break;
            }
        }

        /// <summary>
        /// Определяет контекст ответа
        /// </summary>
        static protected void SetDataToContext(ObjectResult data, ExceptionContext context)
        {
            context.ExceptionHandled = true;
            var response = context.HttpContext.Response;
            response.StatusCode = data.StatusCode ?? StatusCodes.Status400BadRequest;
            context.Result = data;
        }
    }
}

