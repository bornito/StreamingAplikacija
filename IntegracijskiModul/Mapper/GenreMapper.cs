using IntegracijskiModul.BLModels;
using IntegracijskiModul.Modeli;

namespace IntegracijskiModul.Mapper
{
    public static class GenreMapper
    {
        // Iz modela u BL model
        public static IEnumerable<BLGenre> FromModelToBLModel(IEnumerable<Genre> mGenre)
            => (IEnumerable<BLGenre>)mGenre.Select(g => FromModelToBLModel(g));

        private static object FromModelToBLModel(Genre g)
        {
            return new BLGenre
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description
            };
        }

        // obrnuto
        public static IEnumerable<Genre> FromBLModelToModel(IEnumerable<BLGenre> blGenre)
            => (IEnumerable<Genre>)blGenre.Select(g => FromBLModelToModel(g));

        private static object FromBLModelToModel(BLGenre g)
        {
            return new Genre
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description
            };
        }
    }
}
