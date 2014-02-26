using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
			String controller = RouteData.Values["controller"].ToString();
            String directory = RouteData.Values["directory"].ToString();
			String action = RouteData.Values["action"].ToString();
            String size = RouteData.Values["size"].ToString();
            String path = RouteData.Values["path"].ToString();

            // Create string with {controller}/{directory}/{path}
            String origPath = "~/" + controller + "/" + directory + "/" + path; 

            Int32 newHeight = 0;
            Int32 newWidth = 0;
            Boolean valid = false;
            
            // Size starts with 'x', user entered height, otherwise user entered an int32 width 
            // Ternary op to check for valid size leading 'x' means integer height, no 'x' means width
            valid = size.StartsWith("x", true, null) ? Int32.TryParse(size.TrimStart('x'), out newHeight) : Int32.TryParse(size, out newWidth);

            String message = String.Empty;
            // If size check out lets keep going
            if (valid)
            {
                // Ok, we've checked the size lets see if {controller}/{directory}/{path} exists
                valid = Regex.Match(origPath, @"([A-Za-z0-9~_\/\-]+\.[A-Za-z]+)$", RegexOptions.IgnoreCase).Success && System.IO.File.Exists(Server.MapPath(origPath));
                if (valid)
                {
                    try
                    {
                        // Get the original image
                        Image img = Image.FromFile(Server.MapPath(origPath));
                        if (img != null)
                        {
                            // Lets make sure this image hasn't already been resized to the specified dimensions
                            String newPath = String.Format("~/{0}/{1}/{2}/{3}/{4}", controller, directory, action, size, path);
                            if (!System.IO.File.Exists(Server.MapPath(newPath)))
                            {
                                // Get original image size
                                Int32 origHeight = img.Height;
                                Int32 origWidth = img.Width;

                                // Get dat ratio to keep things in proportion
                                Double ratio = (Double)origWidth / (Double)origHeight;

                                // Get new size
                                newHeight = newHeight == 0 ? newHeight = Convert.ToInt32((Double)newWidth / ratio) : newHeight;
                                newWidth = newWidth == 0 ? newWidth = Convert.ToInt32((Double)newHeight * ratio) : newWidth;

                                // Validate new size and resize the image
                                if (valid = newHeight > 0 && newWidth > 0)
                                {
                                    // Create new image object
                                    Image newImg = (Image)(new Bitmap(img, new Size(newWidth, newHeight)));

                                    // Per Trent: don't save images this way, but keeping it here for reference just in case
                                    // Create new image name path with the format {image}-{height}x{width}.ext string
                                    // newImage = Regex.Replace(path, @"^([A-Za-z0-9~_\/\-]+)", String.Format("$1-{0:0}x{1:0}", newWidth, newHeight));
                                    // newPath = String.Format("~/{0}/{1}/{2}/{3}", controller, directory, action, newImage);

                                    // Save the image with new path do dynamic/{path} folder
                                    System.IO.Directory.CreateDirectory(Server.MapPath(Regex.Replace(newPath, @"(\/[A-Za-z]+\.[A-Za-z]+)$", String.Empty)));

                                    // Get content type of image
                                    String sMimeType = GetMimeType(img);

                                    newImg.Save(Server.MapPath(newPath));
                                    newImg.Dispose();
                                    img.Dispose();

                                    return base.File(newPath, sMimeType);
                                }
                                else
                                    message = "Your size don't look so hot. Let's make sure we're not entering any 0's or anything!";
                            }
                        }
                    }
                    catch (Exception ex)
                    { }
                }
                else
                    message = "Your image does not exist on our system so you better double check your path and image routes";
            }
            else
                message = "Your size are whack so better double check them! Enter an 'x' folowed by an int6 for a height, or without the leading 'x' for width";

            return Content(message);
		}

        public static string GetMimeType(Image i)
        {
            var guid = i.RawFormat.Guid;
            foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders())
            {
                if (codec.FormatID == guid)
                    return codec.MimeType;
            }
            return "image/unknown";
        }
	}
}