using IntegracijskiModul.BLModels;

namespace IntegracijskiModul.Repozitoriji
{
    public interface IGenreRepo
    {
        BLGenre AddGenre(BLGenre g);
        BLGenre EditGenre(BLGenre g);
        void DeleteGenre(BLGenre g);
    }
}
