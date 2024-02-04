using AdministrativniModul.ViewModeli;
using AutoMapper;
using IntegracijskiModul.BLModels;
using IntegracijskiModul.Repozitoriji;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace AdministrativniModul.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepo _ur;
        private readonly IMapper _mapper;

        public UserController(IUserRepo ur, IMapper mapper)
        {
            _ur = ur;
            _mapper = mapper;
        }

        // korisnici

        public IActionResult Users()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                var blu = _ur.GetAll();

                return View(blu);
            }
            return View("Login");
        }

        // 1 korisnik

        public IActionResult UserOne(int idUser)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                var blu= _ur.GetUser(idUser);
                return View(blu);
            }
            return View("Login");
        }

        // login

        public IActionResult Login()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Users");
            }
            return View();
        }

        // logout

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();

            return View("Login");
        }

        // dodaj prazno + registracija

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(VMRegistration reg)
        {
            if (!ModelState.IsValid)
            {
                return View(reg);
            }

            if (_ur.CheckUsernameExists(reg.UserName))
            {
                ModelState.AddModelError("UserName", $"Username {reg.UserName} već postoji!");
                return View(reg);
            }

            if (_ur.CheckEmailExists(reg.Email))
            {
                ModelState.AddModelError("Email", $"E-mail {reg.Email} već postoji!");
                return View(reg);
            }

            var user = _ur.CreateUser(
                reg.UserName,
                reg.FirstName,
                reg.LastName,
                reg.Email,
                reg.Password,
                reg.CountryId
                );

            return RedirectToAction("UserRegistered");
        }

        // registriran

        public IActionResult UserRegistered()
        {
            return View();
        }

        // brisanje id + user

        public IActionResult Delete(int idUser)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                var blu = _ur.GetUser(idUser);

                if (blu == null)
                {
                    ModelState.AddModelError("Id", $"Korisnik id:{idUser} ne postoji!");
                    return RedirectToAction("Users");
                }

                return View(blu);
            }
            return View("Login");
        }


        [HttpPost]
        public IActionResult Delete(VMUser u)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                _ur.SoftDeleteUser(u.Id);

                return RedirectToAction("Users");
            }
            return View("Login");
        }

        // detalji

        public IActionResult Details(int id)
        {
            var blUser = _ur.GetUser(id);
            if (blUser == null)
            {
                return NotFound();
            }
            var vmUser = _mapper.Map<VMUser>(blUser);

            return View(vmUser);
        }

        // promjena lozinke + mail

    }
}
