using IntegracijskiModul.Modeli;

namespace IntegracijskiModul.Repozitoriji
{
    public interface ICountryRepo
    {
        IEnumerable<Country> GetAllCountries();
    }
}
