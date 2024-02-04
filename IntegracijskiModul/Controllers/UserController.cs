using IntegracijskiModul.BLModels;
using IntegracijskiModul.Mapper;
using IntegracijskiModul.Modeli;
using IntegracijskiModul.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntegracijskiModul.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _dbc;

        public UserController(DatabaseContext dbc)
        {
            _dbc = dbc;
        }

        // get korisnici 

        [HttpGet]
        public ActionResult<IEnumerable<BLUser>> GetUsers()
        {
            // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

            try
            {
                var dbu = _dbc.Users.Include("Country");
                var u = UserMapper.FromModelToBLModel(dbu);

                return Ok(u);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška dohvaćanja podatka!");
            }
        }

        // post korisnika

        [HttpPost()]
        public ActionResult<BLUser> Post(BLUser u)
        {
            // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

            try
            {
                // provjera + mapiranje + potvrda + dodaj + spremi + vrati ok -> cast obavezno

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                User dbu = (User)UserMapper.FromBLModelToModel((IEnumerable<BLUser>)u);

                dbu.IsConfirmed = true;

                _dbc.Users.Add(dbu);

                _dbc.SaveChanges();

                return Ok(u);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška dodavanja podatka!");
            }
        }

    }
}
