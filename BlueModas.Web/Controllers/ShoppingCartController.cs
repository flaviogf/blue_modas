using Microsoft.AspNetCore.Mvc;

namespace BlueModas.Web.Controllers
{
    [Route("[controller]")]
    public class ShoppingCartController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Show()
        {
            return View();
        }
    }
}
