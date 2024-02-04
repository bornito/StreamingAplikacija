using IntegracijskiModul.BLModels;
using IntegracijskiModul.Mapper;
using IntegracijskiModul.Modeli;
using IntegracijskiModul.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntegracijskiModul.Controllers
{
    public class GenreController : ControllerBase
    {
        [Route("api/genres")]
        [ApiController]
        public class CountryController : ControllerBase
        {
            private readonly DatabaseContext _dbc;

            public CountryController(DatabaseContext dbc)
            {
                _dbc = dbc;
            }

            [HttpGet("[action]")]
            public ActionResult<IEnumerable<BLGenre>> GetGenres()
            {
                // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

                try
                {
                    var g = _dbc.Genres.Include("Videos");
                    return Ok(GenreMapper.FromModelToBLModel(g));
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Greška dohvaćanja podatka!");
                }
            }

            [HttpGet("{id}")]
            public ActionResult<IEnumerable<BLGenre>> GetOneGenre(int idGenre)
            {
                // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

                try
                {
                    var dbg = _dbc.Genres.FirstOrDefault(gn => gn.Id == idGenre);

                    // provjera

                    if (dbg == null)
                    {
                        return NotFound($"Ne postoji žanr za id:{idGenre}");
                    }

                    return Ok(GenreMapper.FromModelToBLModel((IEnumerable<Genre>)dbg));
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Greška dohvaćanja podatka!");
                }
            }

            [HttpPost]
            public ActionResult<IEnumerable<BLGenre>> PostCountry(BLGenre g)
            {
                // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

                try
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var dbg = GenreMapper.FromBLModelToModel((IEnumerable<BLGenre>)g);

                    _dbc.Genres.Add((Genre)dbg);

                    _dbc.SaveChanges();

                    dbg = (IEnumerable<Genre>)GenreMapper.FromModelToBLModel(dbg);

                    return Ok(g);

                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Greška dodavanja podatka!");
                }

            }

            [HttpPut("{id}")]
            public ActionResult<IEnumerable<BLGenre>> PutGenre(int idGenre, BLGenre g)
            {
                // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

                try
                {
                    // valid
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var dbg = _dbc.Genres.FirstOrDefault(gn => gn.Id == idGenre);

                    // null
                    if (dbg == null)
                    {
                        return NotFound($"Ne postoji žanr za id:{idGenre}");
                    }

                    dbg.Name = g.Name;
                    dbg.Description = g.Description;

                    _dbc.SaveChanges();

                    return Ok(GenreMapper.FromModelToBLModel(g));
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Greška dodavanja podatka!");
                }

            }

            [HttpDelete("{id}")]
            public ActionResult<IEnumerable<BLGenre>> DeleteGenre(int idGenre)
            {
                // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

                try
                {
                    var dbg = _dbc.Genres.FirstOrDefault(gn => gn.Id == idGenre);

                    // null
                    if (dbg == null)
                    {
                        return NotFound($"Ne postoji žanr za id:{idGenre}");
                    }

                    var blg = GenreMapper.FromModelToBLModel((IEnumerable<Genre>)dbg);

                    // uloni + spremi

                    _dbc.Genres.Remove(dbg);

                    _dbc.SaveChanges();

                    return Ok(blg);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Greška brisanja podatka!");
                }

            }

        }
    }
}

