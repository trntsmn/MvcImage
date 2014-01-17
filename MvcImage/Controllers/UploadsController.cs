using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcImage.Controllers
{
	public class UploadsController : Controller
	{
		//
		// GET: /Uploads/
		public ActionResult Index()
		{
			return Content("Wrong one.");
		}

		public ActionResult Dynamic()
		{
			// Controller names are case insensitive.
			var controller = RouteData.Values["controller"];
			var action = RouteData.Values["action"];
			var id = RouteData.Values["path"];

			var message = String.Format("<h1>{0}::{1}  {2}</h1>", controller, action, id);
			return Content(message);
		}
	}
}