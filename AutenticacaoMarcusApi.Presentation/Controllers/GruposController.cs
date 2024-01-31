using Microsoft.AspNetCore.Mvc;

namespace AutenticacaoMarcusApi.Presentation.Controllers
{
    [Route("api/grupos")]
    public class GruposController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
