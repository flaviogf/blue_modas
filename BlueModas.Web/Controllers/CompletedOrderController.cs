using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace BlueModas.Web.Controllers
{
    [Route("PedidoFinalizado")]
    public class CompletedOrderController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Show()
        {
            if (!HttpContext.Session.TryGetValue("@order-number", out var value))
            {
                return RedirectToAction("Index", "Product");
            }

            HttpContext.Session.Remove("@order-number");

            HttpContext.Session.Remove("@order-items-count");

            return View();
        }
    }
}
