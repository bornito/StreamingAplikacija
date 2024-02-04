using AdministrativniModul.ViewModeli;
using AutoMapper;
using IntegracijskiModul.BLModels;
using IntegracijskiModul.Repozitoriji;
using Microsoft.AspNetCore.Mvc;

namespace AdministrativniModul.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideoRepo _ivr;
        private readonly ILogger<VideoController> _lgr;
        private readonly IMapper _mapper;

        private readonly IGenreRepo _igr;
        private readonly IImageRepo _iir;
        private readonly ITagRepo _itr;

        public VideoController(IVideoRepo ivr, ILogger<VideoController> lgr, IMapper mapper, IGenreRepo igr, IImageRepo iir, ITagRepo itr)
        {
            _ivr = ivr;
            _lgr = lgr;
            _mapper = mapper;
            _igr = igr;
            _iir = iir;
            _itr = itr;
        }

        // svi + 1

        public ActionResult Videos()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                var blVideos = _ivr.GetAll();

                return View(blVideos);
            }
            return View("Index");
        }

        public ActionResult Video(int idVideo)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                var blv= _ivr.GetVideo(idVideo);

                if (blv == null)
                {
                    ModelState.AddModelError("Id", $"Nije pronađen video id:{idVideo}!");
                    return RedirectToAction("Videos");
                }
                return View(blv);
            }
            return View("Index");
        }

        public ActionResult Add()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                return View();
            }
            return View("Index");
        }

        // edit

        [HttpPost]
        public ActionResult Edit(BLVideo video)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                _ivr.EditVideo(video);
                return RedirectToAction("Videos");
            }
            return View("Index");
        }

        // delete id + video

        public ActionResult Delete(int idVideo)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                var blv = _ivr.GetVideo(idVideo);
                if (blv == null)
                {
                    ModelState.AddModelError("Id", $"Nije pronađen video id:{idVideo}!");
                    return RedirectToAction("Videos");
                }
                return View(blv);
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Delete(VMVideo video)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                _ivr.DeleteVideo((int)video.Id);

                return RedirectToAction("Videos");
            }
            return View("Index");
        }
    }
}
