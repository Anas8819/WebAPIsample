using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using WebApiContrib.Formatting.Jsonp;
using System.Web.Http.Cors;

namespace EmployeeService
{
    //sdvsv
    //public class CustomJsonFormeter : JsonMediaTypeFormatter
    //{
    //    public CustomJsonFormeter()
    //    {
    //        this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
    //    }
    //    public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
    //    {
    //        base.SetDefaultContentHeaders(type, headers, mediaType);
    //        headers.ContentType = new MediaTypeHeaderValue("application/json");
    //    }
    //}
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Formatters.Add(new CustomJsonFormeter());
                
            //config.Formatters.Remove(config.Formatters.XmlFormatter);
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            //config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //var jsonpFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            //config.Formatters.Insert(0, jsonpFormatter);

            //this will enable access to whole application(all controllers etc)
            //EnableCorsAttribute cors = new EnableCorsAttribute
            //("Origins that we want to allow(which clint can use and not)", "headers", "meathord that can be issued");
            //EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors); for every controller
            config.EnableCors();//[EnableCorsAttribute ("*","*","*")] in controller we want to allow [DisableCors] to disable on a single function
            config.Filters.Add(new RequireHttpsAttribute());

        }
    }
}
