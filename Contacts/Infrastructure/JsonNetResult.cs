using System;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Contacts.Infrastructure
{
    public class JsonNetResult<T> : JsonResult
        where T: class
    {
        
        public JsonNetResult(T data)
        {
            this.Data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var response = context.HttpContext.Response;

            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data == null)
                return;

            JsonSerializerSettings settings = new JsonSerializerSettings { Formatting = Formatting.None };
            var serializedObject = JsonConvert.SerializeObject(Data, settings);
            response.Write(serializedObject);
        }
    }
}