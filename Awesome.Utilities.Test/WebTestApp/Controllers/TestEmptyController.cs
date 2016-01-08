using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Awesome.Utilities.Test.WebTestApp.Controllers
{
    // ReSharper disable Mvc.ViewNotResolved It's only a test controller.
    public class TestEmptyController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
