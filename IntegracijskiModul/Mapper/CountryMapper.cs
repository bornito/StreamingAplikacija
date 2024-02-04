using IntegracijskiModul.BLModels;
using IntegracijskiModul.Modeli;

namespace IntegracijskiModul.Mapper
{
    public static class CountryMapper
    {
        // Iz modela u BL model
        public static IEnumerable<BLCountry> FromModelToBLModel(IEnumerable<Country> mCountry)
            => (IEnumerable<BLCountry>)mCountry.Select(m => FromModelToBLModel(m));

        private static object FromModelToBLModel(Country m)
        {
            return new BLCountry
            {
                Id = m.Id,
                Name = m.Name,
                CountryCode = m.CountryCode
            };
        }

        // obrnuto
        public static IEnumerable<Country> FromBLModelToModel(IEnumerable<BLCountry> blCountry)
            => (IEnumerable<Country>)blCountry.Select(c => FromBLModelToModel(c));

        private static object FromBLModelToModel(BLCountry c)
        {
            return new Country
            {
                Id = c.Id,
                Name = c.Name,
                CountryCode = c.CountryCode      
            };
        }
    }
}
