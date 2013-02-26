using System;
using System.Web.Mvc;
using System.Web.Routing;
using TwitterSearchDemo.Framework;
using TwitterSearchDemo.Framework.Web;

namespace TwitterSearchDemo.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly IManageTwitterSearch _search;

        public HomeController(IManageTwitterSearch search)
        {
            _search = search;
        }

        [HttpGet]
        public virtual ActionResult Search(TwitterSearchQuery model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Results = _search.GetResultsFor(model);
            }
            else
            {
                model = new TwitterSearchQuery { ResultType = ResultType.Mixed, SearchType = SearchType.Hashtag, Page = 1 };    
            }

            ModelState.Clear();
            return View(model);
        }
        [HttpPost]
        public virtual ActionResult SearchFor(TwitterSearchQuery model)
        {
            if (!ModelState.IsValid)
                return View("Search", model);
            var route = model.ToRouteValues();
            route["Page"] = 1;
            return RedirectToAction("Search", route);
        }
    }
}