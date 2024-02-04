using IntegracijskiModul.BLModels;
using IntegracijskiModul.Mapper;
using IntegracijskiModul.Models;
using IntegracijskiModul.Repozitoriji;
using Microsoft.AspNetCore.Mvc;

namespace AdministrativniModul.Controllers
{
    public class TagController : Controller
    {
        private readonly DatabaseContext _dbc;
        private readonly ITagRepo _itr;

        public TagController(DatabaseContext dbc, ITagRepo itr)
        {
            _dbc = dbc;
            _itr = itr;
        }

        // tagovi

        public IActionResult Tags()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                var dbt = _dbc.Tags;

                var blt = TagMapper.FromModelToBLModel(dbt);

                return View(blt);
            }

            return View("Index");
        }

        // dodaj tag bez + id

        public IActionResult Add()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                return View();
            }
            return View("Index");
        }

        [HttpPost]
        public IActionResult Add(BLTag blt)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                _itr.AddTag(blt);

                return RedirectToAction("Tags");
            }
            return View("Index");
        }

        // tag 1

        public IActionResult Tag(int idTag)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                var t = _dbc.Tags.FirstOrDefault(t => t.Id == idTag);

                if (t == null)
                {
                    ModelState.AddModelError("Id", $"Ne postoji tag id:{idTag}");
                    return RedirectToAction("Tags");
                }

                var blt = TagMapper.FromModelToBLModel((IEnumerable<IntegracijskiModul.Modeli.Tag>)t);

                return View(blt);
            }

            return View("Index");
        }

        // edit id + tag

        public IActionResult Edit(int idTag)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                var t = _dbc.Tags.FirstOrDefault(t => t.Id == idTag);

                if (t == null)
                {
                    ModelState.AddModelError("Id", $"Ne postoji tag id:{idTag}");
                    return RedirectToAction("Tags");
                }

                var blt = TagMapper.FromModelToBLModel((IEnumerable<IntegracijskiModul.Modeli.Tag>)t);

                return View(blt);
            }
            return View("Index");
        }

        [HttpPost]
        public IActionResult Edit(BLTag tag)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                _itr.EditTag(tag);

                return RedirectToAction("Tags");
            }
            return View("Index");
        }

        // delete id + tag

        public IActionResult Delete(int idTag)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                var t = _dbc.Tags.FirstOrDefault(t => t.Id == idTag);

                if (t == null)
                {
                    ModelState.AddModelError("Id", $"Ne postoji tag id:{idTag}");
                    return RedirectToAction("Tags");
                }

                var blt = TagMapper.FromModelToBLModel((IEnumerable<IntegracijskiModul.Modeli.Tag>)t);

                return View(blt);
            }
            return View("Index");
        }

        [HttpPost]
        public IActionResult Delete(BLTag tag)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                _itr.DeleteTag(tag);
                return RedirectToAction("Tags");
            }
            return View("Index");
        }

    }
}
