using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Awesome.Utilities.Test.WebTestApp.Controllers
{
    public class TestController : Controller
    {
        [Authorize()]
        public ActionResult IndexNoRoles()
        {
            return View();
        }

        [Authorize(Roles = "Role1")]
        public ActionResult IndexRole1()
        {
            return View();
        }

        [Authorize(Roles = "Role2")]
        [HttpPost]
        [ActionName("IndexRole1")]
        public ActionResult IndexRole1Post()
        {
            return View();
        }

    }
}
