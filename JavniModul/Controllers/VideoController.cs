using AutoMapper;
using IntegracijskiModul.Repozitoriji;
using Microsoft.AspNetCore.Mvc;

namespace JavniModul.Controllers
{
    public class VideoController : Controller
    {
        private readonly ILogger<VideoController> _logger;
        private readonly IVideoRepo _ivr;
        private readonly IMapper _mapper;

        private readonly IGenreRepo _igr;
        private readonly IImageRepo _iir;

        public VideoController(ILogger<VideoController> logger, IVideoRepo ivr, IMapper mapper, IGenreRepo igr, IImageRepo iir)
        {
            _logger = logger;
            _ivr = ivr;
            _mapper = mapper;
            _igr = igr;
            _iir = iir;
        }

        public IActionResult Video(int id)
        {
            var videos = _ivr.GetAll();
            var video = videos.FirstOrDefault(x => x.Id == id);
            return View(video);
        }

        public IActionResult Videos()
        {
            var videos = _ivr.GetAll();
            return View(videos);
        }
    }
}
