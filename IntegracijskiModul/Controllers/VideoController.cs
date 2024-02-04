using IntegracijskiModul.BLModels;
using IntegracijskiModul.Mapper;
using IntegracijskiModul.Modeli;
using IntegracijskiModul.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntegracijskiModul.Controllers
{
    [Route("api/videos")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly DatabaseContext _dbc;

        public VideoController(DatabaseContext dbc)
        {
            _dbc = dbc;
        }

        // GET: api/videos
        [HttpGet]
        public ActionResult<IEnumerable<BLVideo>> GetAll()
        {
            // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

            try
            {
                var dbVideos = _dbc.Videos
                    .Include("Genre")
                    .Include("Image")
                    .Include("VideoTags")
                    .Include("VideoTags.Tag");

                var videos = VideoMapper.FromModelToBLModel(dbVideos);

                return Ok(videos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška dohvacanja podatka!");
            }
        }

        // GET api/<VideoController>/5
        [HttpGet("{id}")]
        public ActionResult<BLVideo> GetVideo(int idVideo)
        {
            // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

            try
            {
                var dbVideos = _dbc.Videos
                    .Include("Genre")
                    .Include("Image")
                    .Include("VideoTags.Tag").Where(v => v.Id == idVideo);

                if (dbVideos == null)
                {
                    return NotFound($"Nema videa sa id:{idVideo}");
                }

                var videos = VideoMapper.FromModelToBLModel(dbVideos);

                return Ok(videos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška dohvacanja podatka!");
            }
        }
        // get 


        [HttpGet("[action]")]
        public ActionResult<IEnumerable<BLVideo>> Search(int page, int size, string nameFilter, string orderBy, string direction)
        {
            // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

            try
            {
                var searchName = HttpContext.Request.Cookies["search.name"];

                if (searchName == null)
                {
                    return NotFound($"Ne postoji!");
                }

                if (nameFilter != null)
                {
                    HttpContext.Response.Cookies.Append("search.name", nameFilter);
                }

                else if (!string.IsNullOrEmpty(searchName))
                {
                    nameFilter = searchName;
                }

                // filtriranje

                var dbVideos = _dbc.Videos.Where(v => v.Name.Contains(searchName));

                var videos = VideoMapper.FromModelToBLModel(dbVideos);

                // order by id

                if (string.Compare(orderBy, "id", true) == 0)
                {
                    videos = videos.OrderBy(v => v.Id);
                }
                // order by name


                else if (string.Compare(orderBy, "name", true) == 0)
                {
                    videos = videos.OrderBy(v => v.Name);
                }

                // order by trajanje

                else if (string.Compare(orderBy, "totaltime", true) == 0)
                {
                    videos = videos.OrderBy(v => v.TotalTime);
                }

                else // po id-u -> default
                {
                    videos = videos.OrderBy(v => v.Id);
                }

                // obrnuto

                if (string.Compare(direction, "desc", true) == 0)
                {
                    videos = videos.Reverse();
                }


                //paging

                //page i size predstavljaju broj stranice i broj elemenata po stranici
                videos = videos.Skip(page * size).Take(size);


                //session handling
                HttpContext.Session.SetString("videos.search.count", videos.Count().ToString());

                return Ok(videos);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška pretraživanja!");
            }
        }

        [HttpGet("[action]")]
        public ActionResult<int> GetLastVideosCount()
        {
            var count = HttpContext.Session.GetString("videos.search.count");
            return Ok(count);
        }


        // POST api/<VideoController>
        [HttpPost()]
        public ActionResult<BLVideo> Post(BLVideo bv)
        {
            {
                // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

                try
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    BLVideo dbv = (BLVideo)VideoMapper.FromModelToBLModel((IEnumerable<Modeli.Video>)bv);

                    var dbt = _dbc.Tags.Where(x => bv.Tags.Contains(x));

                    dbv.VideoTags = (ICollection<BLVideoTag>)dbt.Select(x => new VideoTag
                    { Tag = x }).ToList();

                    _dbc.Videos.Add((Video)(IEnumerable<Modeli.Video>)dbv);

                    _dbc.SaveChanges();

                    bv = (BLVideo)VideoMapper.FromModelToBLModel((IEnumerable<Video>)dbv);

                    return Ok(bv);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Greška dodavanja!");
                }
            }
        }

        // PUT api/<VideoController>/5
        [HttpPut("{id}")]
        public ActionResult<BLVideo> Put(int id, BLVideo video)
        {
            {
                // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

                try
                {   
                    // valid + nadji + null + set + staro/novo + spremi + vrati ok
                    // 
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var dbVideo = _dbc.Videos.FirstOrDefault(v => v.Id == id);

                    if (dbVideo == null)
                    {
                        return NotFound($"Nema videa za id:{id}");
                    }

                    dbVideo.Name = video.Name;
                    dbVideo.GenreId = video.GenreId;
                    dbVideo.TotalTime = video.TotalTime;
                    dbVideo.Description = video.Description;
                    dbVideo.ImageId = video.ImageId;
                    dbVideo.StreamingUrl = video.StreamingUrl;

                    // obrisi stare tagove

                    var tagToRemove = dbVideo.VideoTags.Where(v => !video.Tags.Contains(v.Tag));

                    foreach (var rmvTag in tagToRemove)
                    {
                        _dbc.VideoTags.Remove(rmvTag);
                    }

                    // dodaj nove tagove

                    var existingDbTags = dbVideo.VideoTags.Select(x => x.Tag);
                    var newTags = video.Tags.Except(existingDbTags);
                    foreach (var newTag in newTags)
                    {
                        var dbTag = _dbc.Tags.FirstOrDefault(x => newTag == x);

                        // ako nema tagova

                        if (dbTag == null)
                            continue;

                        dbVideo.VideoTags.Add(new VideoTag
                        {
                            Video = dbVideo,
                            Tag = dbTag
                        });
                    }

                    _dbc.SaveChanges();

                    return Ok(dbVideo);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Greška dodavanja!");
                }
            }
        }

        // DELETE api/<VideoController>/id
        [HttpDelete("{id}")]
        public ActionResult<BLVideo> Delete(int idVideo)
        {
            // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

            try // brisanje
            {
                var dbAudio = _dbc.Videos.FirstOrDefault(v => v.Id == idVideo);

                if (dbAudio == null)
                {
                    return NotFound($"Ne postoji za id:{idVideo}");
                }

                _dbc.Videos.Remove(dbAudio);

                _dbc.SaveChanges();

                return Ok(dbAudio);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška brisanja!");
            }
        }
    }
}
