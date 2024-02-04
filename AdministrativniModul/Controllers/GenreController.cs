using IntegracijskiModul.BLModels;
using IntegracijskiModul.Mapper;
using IntegracijskiModul.Models;
using IntegracijskiModul.Repozitoriji;
using Microsoft.AspNetCore.Mvc;

namespace AdministrativniModul.Controllers
{
    public class GenreController : Controller
    {
        private readonly DatabaseContext _dbc;
        private readonly IGenreRepo _igr;

        public GenreController(DatabaseContext dbc, IGenreRepo igr)
        {
            _dbc = dbc;
            _igr = igr;
        }

        // zanrovi

        public IActionResult Genres()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                var dbg = _dbc.Genres;

                var blg = GenreMapper.FromModelToBLModel(dbg);

                return View(blg);
            }

            return View("Index");
        }

        // add bez + sa

        public IActionResult Add()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                return View();
            }

            return View("Index");
        }

        [HttpPost]
        public IActionResult Add(BLGenre g)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                _igr.AddGenre(g);
                return RedirectToAction("Genres");
            }

            return View("Index");
        }

        // zanr 1

        public IActionResult Genre(int idGenre)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                var target = _dbc.Genres.FirstOrDefault(x => x.Id == idGenre);

                if (target == null)
                {
                    ModelState.AddModelError("Id", "Žanr ne postoji!");
                    return RedirectToAction("Genres");
                }

                var blGenres = GenreMapper.FromModelToBLModel((IEnumerable<IntegracijskiModul.Modeli.Genre>)target);

                return View(blGenres);
            }

            return View("Index");
        }

        // edit id + zanr

        public IActionResult Edit(int idGenre)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                var target = _dbc.Genres.FirstOrDefault(x => x.Id == idGenre);

                if (target == null)
                {
                    ModelState.AddModelError("Id", "Žanr ne postoji!");
                    return RedirectToAction("Genres");
                }

                var blGenres = GenreMapper.FromModelToBLModel((IEnumerable<IntegracijskiModul.Modeli.Genre>)target);

                return View(blGenres);
            }

            return View("Index");
        }


        [HttpPost]
        public IActionResult Edit(BLGenre g)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                _igr.EditGenre(g);
                return RedirectToAction("Genres");
            }

            return View("Index");
        }

        // delete id + zanr

        public IActionResult Delete(int idGenre)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                var target = _dbc.Genres.FirstOrDefault(x => x.Id == idGenre);

                if (target == null)
                {
                    ModelState.AddModelError("Id", "Žanr ne postoji!");
                    return RedirectToAction("Genres");
                }

                var blGenres = GenreMapper.FromModelToBLModel((IEnumerable<IntegracijskiModul.Modeli.Genre>)target);

                return View(blGenres);
            }

            return View("Index");
        }

        [HttpPost]
        public IActionResult Delete(BLGenre g)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                _igr.DeleteGenre(g);
                return RedirectToAction("Genres");
            }

            return View("Index");
        }
    }
}
