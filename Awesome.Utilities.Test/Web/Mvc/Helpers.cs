using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using System.IO;
using System.Diagnostics;

namespace Awesome.Utilities.Test.Web.Mvc
{
    public static class Helpers
    {
        public static HtmlHelper CreateHtmlHelper(ViewDataDictionary vd = null, TextWriter textWriter = null)
        {
            vd = vd ?? new ViewDataDictionary();
            textWriter = textWriter ?? Console.Out;
            var mockViewContext = new Mock<ViewContext>(
              new ControllerContext(
                new Mock<HttpContextBase>().Object,
                new RouteData(),
                new Mock<ControllerBase>().Object),
              new Mock<IView>().Object,
              vd,
              new TempDataDictionary(),
              textWriter);

            var mockViewDataContainer = new Mock<IViewDataContainer>();
            mockViewDataContainer.Setup(v => v.ViewData).Returns(vd);

            return new HtmlHelper(mockViewContext.Object, mockViewDataContainer.Object);
        }
    }
}
