using IntegracijskiModul.Modeli;
using IntegracijskiModul.Models;

namespace IntegracijskiModul.Repozitoriji
{
    public class CountryRepo : ICountryRepo
    {
        private readonly DatabaseContext _dbc;

        public CountryRepo(DatabaseContext dbc)
        {
            _dbc = dbc;
        }
        IEnumerable<Country> ICountryRepo.GetAllCountries() => _dbc.Countries;
    }
}
