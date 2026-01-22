using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lesson3_MiddlewareAndMvc.ActionResults
{
    public class JsonResult : IActionResult
    {
        private readonly object _data;
        private readonly int _statusCode;

        public JsonResult(object data, int statusCode = 200)
        {
            _data = data;
            _statusCode = statusCode;
        }

        public void ExecuteResult(HttpListenerContext context)
        {
            string json = JsonSerializer.Serialize(_data);

            byte[] buffer = Encoding.UTF8.GetBytes(json);

            context.Response.StatusCode = _statusCode;
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentLength64 = buffer.Length;

            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.OutputStream.Close();
        }
    }
}
