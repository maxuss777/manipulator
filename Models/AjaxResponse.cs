using Microsoft.AspNetCore.Mvc;

namespace Manipulator.Models
{
    public class AjaxResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }


        public static JsonResult GetErrorResponse(string message)
        {
            var model = new AjaxResponse { IsSuccess = false, Message = message };

            return new JsonResult(model);
        }

        public static JsonResult GetSuccessResponse(string message)
        {
            var model = new AjaxResponse { IsSuccess = true, Message = message };

            return new JsonResult(model);
        }
    }
}
