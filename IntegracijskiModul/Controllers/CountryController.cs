using IntegracijskiModul.BLModels;
using IntegracijskiModul.Mapper;
using IntegracijskiModul.Modeli;
using IntegracijskiModul.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegracijskiModul.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly DatabaseContext _dbc;

        public CountryController(DatabaseContext dbc)
        {
            _dbc = dbc;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BLCountry>> GetCountries()
        {
            // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

            try
            {
                var dbc = _dbc.Countries;
                return Ok(CountryMapper.FromModelToBLModel(dbc));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška dohvaćanja podatka!");
            }
        }

        [HttpPost]
        public ActionResult<IEnumerable<BLCountry>> PostCountry(BLCountry c)
        {
            // Osigurajte dobro korisničko iskustvo bez prikaza neulovljenih iznimki.

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                else
                {
                    Country country = new()
                    {
                        Name = c.Name,
                        CountryCode = c.CountryCode
                    };

                    _dbc.Countries.Add(country);
                    _dbc.SaveChanges();

                    return Ok(c);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška dodavanja podatka!");
            }
        }
    }
}
