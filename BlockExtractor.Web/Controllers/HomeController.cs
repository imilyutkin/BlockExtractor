using System;
using System.Web.Mvc;

namespace BlockExtractor.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        
        //
        // POST: /Home/
        [HttpPost]
        public ActionResult Index(SiteUrlViewModel siteUrlModel)
        {
            return RedirectToAction("Extract", "Home", siteUrlModel);
        }

        [HttpGet]
        public ActionResult Extract(SiteUrlViewModel siteUrlModel)
        {
            return View(siteUrlModel);
        }

    }

    public class SiteUrlViewModel
    {
        public string Url
        {
            get;
            set;
        }
    }
}
