using IntegracijskiModul.Modeli;
using IntegracijskiModul.Repozitoriji;
using Microsoft.AspNetCore.Mvc;

namespace AdministrativniModul.Controllers
{
    public class CountryController : Controller
    {

        private readonly ILogger<CountryController> _lgr;
        private readonly ICountryRepo _icr;

        public CountryController(ILogger<CountryController> lgr, ICountryRepo icr)
        {
            _lgr = lgr;
            _icr = icr;
        }

        // drzave

        public IActionResult Country()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                var c = _icr.GetAllCountries();
                return View(c);
            }

            return View("index");
        }
    }
}
