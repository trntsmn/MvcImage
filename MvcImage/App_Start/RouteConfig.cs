using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.Routing;
using System.Web.UI;

namespace MvcImage
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Uploads",
				url: "uploads/{brand}/{action}/{size}/{path}",
				defaults: new { controller = "Uploads", brand = "Saris", action = "Dynamic", size = "200x", path = string.Empty }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}



	public class ImageRouteHandler : IRouteHandler
	{
		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			string filename = requestContext.RouteData.Values["filename"] as string;

			if (string.IsNullOrEmpty(filename))
			{
				// return a 404 HttpHandler here
			}
			else
			{
				requestContext.HttpContext.Response.Clear();
				requestContext.HttpContext.Response.ContentType = GetContentType(requestContext.HttpContext.Request.Url.ToString());

				// find physical path to image here.  
				string filepath = requestContext.HttpContext.Server.MapPath("~/test.jpg");

				requestContext.HttpContext.Response.WriteFile(filepath);
				requestContext.HttpContext.Response.End();

			}
			return null;
		}

		private static string GetContentType(String path)
		{
			switch (Path.GetExtension(path))
			{
				case ".bmp": return "Image/bmp";
				case ".gif": return "Image/gif";
				case ".jpg": return "Image/jpeg";
				case ".png": return "Image/png";
				default: break;
			}
			return "";
		}
	}

}
