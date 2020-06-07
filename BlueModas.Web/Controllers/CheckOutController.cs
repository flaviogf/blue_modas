using Microsoft.AspNetCore.Mvc;

namespace BlueModas.Web.Controllers
{
    [Route("")]
    public class CheckoutController : Controller
    {
        [HttpPost]
        [Route("")]
        [ValidateAntiForgeryToken]
        public IActionResult Store()
        {
            return RedirectToAction("Show", "CompletedOrder");
        }
    }
}
