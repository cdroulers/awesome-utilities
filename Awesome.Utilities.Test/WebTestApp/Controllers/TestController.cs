using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Awesome.Utilities.Test.WebTestApp.Controllers
{
    // ReSharper disable Mvc.ViewNotResolved It's only a test controller.
    public class TestController : Controller
    {
        [Authorize]
        public ActionResult IndexNoRoles()
        {
            return this.View();
        }

        [Authorize(Roles = "Role1")]
        public ActionResult IndexRole1()
        {
            return this.View();
        }

        [Authorize(Roles = "Role2")]
        [HttpPost]
        [ActionName("IndexRole1")]
        public ActionResult IndexRole1Post()
        {
            return this.View();
        }

    }
}
