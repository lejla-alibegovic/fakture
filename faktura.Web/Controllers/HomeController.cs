using faktura.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace faktura.Web.Controllers
{
    public class HomeController : Controller
    {
        private IFakturaService _fakturaService;
        public HomeController()
        {

        }
        public HomeController(IFakturaService fakturaService)
        {
            _fakturaService = fakturaService;
        }
        public async Task<ActionResult> Index()
        {
            var result = await _fakturaService.GetAll();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}