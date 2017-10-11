using System;
using Nancy;

namespace NancyService.helpers
{
   
    public static class MiddlewareWrapper
    {
        public static Func<object, object> intercept(dynamic module, Func<dynamic, Response> func)
        {
            return (dynamic args) =>
            {
                var startTime = DateTime.Now;
                var returnValue = func(args);
                Console.WriteLine("{0} {1} [{2}] ", module.Request.Method, module.Request.Url, (DateTime.Now - startTime).TotalMilliseconds.ToString());
                return returnValue;
            };
        }
    }
}