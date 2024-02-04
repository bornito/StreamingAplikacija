using AdministrativniModul.ViewModeli;
using AutoMapper;
using IntegracijskiModul.Repozitoriji;
using JavniModul.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace JavniModul.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepo _iur;
        private readonly ICountryRepo _icr;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, IUserRepo iur, ICountryRepo icr, IMapper mapper)
        {
            _logger = logger;
            _iur = iur;
            _icr = icr;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var blu = _iur.GetAll();
            var vmu = _mapper.Map<IEnumerable<VMUser>>(blu);

            return View(vmu);
        }

        public IActionResult Details(int idUser)
        {
            var blu = _iur.GetUser(idUser);

            if (blu == null)
            {
                return NotFound();
            }

            var vmu = _mapper.Map<VMUser>(blu);
            return View(vmu);
        }

        public IActionResult Register()
        {
            var blc = _icr.GetAllCountries();
            var vmc = _mapper.Map<IEnumerable<VMCountry>>(blc);
            ViewBag.Country = new SelectList(vmc, "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Register(VMRegistration register)
        {
            if (!ModelState.IsValid)
            {
                var blc = _icr.GetAllCountries();
                var vmc = _mapper.Map<IEnumerable<VMCountry>>(blc);
                ViewBag.Country = new SelectList(vmc, "Id", "Name");

                return View(register);
            }

            try
            {
                var user = _iur.CreateUser(
                register.UserName,
                register.FirstName,
                register.LastName,
                register.Email,
                register.Password,
                register.CountryId
                );
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                var blc= _icr.GetAllCountries();
                var vmc = _mapper.Map<IEnumerable<VMCountry>>(blc);

                ViewBag.Country = new SelectList(vmc, "Id", "Name");

                return View(register);
            }

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(VMLogin login, bool staySignedIn)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            try
            {
                var user = _iur.GetConfirmedUser(login.UserName, login.Password);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Neispravno korisničko ime ili lozinka!");
                    return View(login);
                }

                var claims = new List<Claim>
                {
                        new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),

                };
                var claimsIdentity = 
                    new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties 
                    = new AuthenticationProperties();

                if (staySignedIn)
                {
                    authenticationProperties.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30);
                }

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authenticationProperties).Wait();

                return RedirectToAction("Video", "Video");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(login);
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();

            return RedirectToAction("Home");
        }

        public IActionResult Home()
        {
            return View();
        }

    }
}
