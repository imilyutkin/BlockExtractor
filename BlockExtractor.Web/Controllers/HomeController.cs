using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;

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
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(siteUrlModel.Url);
            var textNodes =
                doc.DocumentNode.Descendants("section")
                    .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("b-main-page-news-2__main-news"));
//            foreach (var htmlNode in textNodes)
//            {
//                WriteText(doc, htmlNode, "lol");
//            }

            foreach (var node in textNodes)
            {
                if (node.Attributes.Contains("class"))
                {
                    var classes = node.Attributes["class"].Value;
                    classes += " green-bg";
                    node.Attributes["class"].Value = classes;
                }
                else
                {
                    node.Attributes.Add("class", "green-bg");
                }
            }

    
            var html = doc.DocumentNode.OuterHtml;
            return View(new HtmlString(html));
        }

        private void WriteText(HtmlDocument htmlDocument, HtmlNode node, string text)
        {
            if (node.ChildNodes.Count > 0)
            {
                node.ReplaceChild(htmlDocument.CreateTextNode(text), node.ChildNodes.First());
            }
            else
            {
                node.AppendChild(htmlDocument.CreateTextNode(text));
            }
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
