using IntegracijskiModul.BLModels;
using IntegracijskiModul.Mapper;
using IntegracijskiModul.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntegracijskiModul.Controllers
{
    [Route("api/tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly DatabaseContext _dbc;

        public TagController(DatabaseContext dbc)
        {
            _dbc = dbc;
        }

        // get

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<BLTag>> GetAllTags()
        {
            // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

            try
            {
                var dbt = _dbc.Tags.Include("VideoTags.Video");

                if (dbt == null)
                {
                    return NotFound($"Nema oznake!");
                }

                return Ok(TagMapper.FromModelToBLModel(dbt));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška dohvaćanja podatka!");
            }
        }

        // get + id

        [HttpGet("{id}")]
        public ActionResult<BLTag> GetOne(int idTag)
        {
            // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

            try
            {
                var dbt = _dbc.Tags.Include("VideoTags.Video").FirstOrDefault(x => x.Id == idTag);

                if (dbt == null)
                {
                    return NotFound($"Ne postoji oznaka id:{idTag}");
                }

                return Ok(TagMapper.FromModelToBLModel((IEnumerable<Modeli.Tag>)dbt));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška dohvaćanja podatka!");
            }
        }

        [HttpPost]
        public ActionResult<BLTag> PostOne(BLTag t)
        {
            // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var dbt = TagMapper.FromBLModelToModel((IEnumerable<BLTag>)t);

                _dbc.Tags.Add((Modeli.Tag)dbt);

                _dbc.SaveChanges();

                t = (BLTag)TagMapper.FromModelToBLModel(dbt);

                return Ok(t);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška dodavanja podatka!");
            }
        }
        [HttpPut("{id}")]
        public ActionResult<BLTag> PutOne(int idTag, BLTag blt)
        {
            // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

            try
            {
                // provjere prvo

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var dbt = _dbc.Tags.FirstOrDefault(x => x.Id == idTag);

                if (dbt == null)
                {
                    return NotFound($"Ne postoji oznaka id:{idTag}");
                }

                dbt.Name = blt.Name;

                _dbc.SaveChanges();

                return Ok(TagMapper.FromModelToBLModel((IEnumerable<Modeli.Tag>)dbt));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška dodavanja podatka!");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<BLTag> Delete(int idTag)
        {
            // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

            try
            {
                // provjera + brisi + spremi + vrati ok

                var dbt = _dbc.Tags.FirstOrDefault(x => x.Id == idTag);

                if (dbt == null)
                {
                    return NotFound($"Ne postoji oznaka id:{idTag}");
                }

                _dbc.Tags.Remove(dbt);

                _dbc.SaveChanges();

                return Ok(TagMapper.FromModelToBLModel((IEnumerable<Modeli.Tag>)dbt));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška brisanja podatka!");
            }
        }
    }
}
