using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.OData.Builder;
using Library;
using System.Web.OData.Extensions;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            ODataModelBuilder builder = new ODataConventionModelBuilder();

            builder.EntitySet<Game>("Games");
            builder.EntitySet<Store>("Stores");
            builder.EntitySet<CardShirt>("CardShirts");

            builder.Function("GetAvailableCardShirts").ReturnsCollection<CardShirt>();

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel());
        }
    }
}
