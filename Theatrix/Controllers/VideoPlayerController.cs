using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Theatrix.Controllers
{
    public class VideoPlayerController : Controller
    {
        //
        // GET: /VideoPlayer/

        public ActionResult Index(string videoId)
        {
            var client = new Theatrix.Services.VideoServiceClient();
            client.GetVideoById(videoId);
            return View();
        }

    }
}
