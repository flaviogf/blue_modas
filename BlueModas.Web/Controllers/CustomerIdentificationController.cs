using Microsoft.AspNetCore.Mvc;

namespace BlueModas.Web.Controllers
{
    [Route("MeusDados")]
    public class CustomerIdentificationController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Store()
        {
            return View();
        }
    }
}
