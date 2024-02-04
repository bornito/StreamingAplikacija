using IntegracijskiModul.BLModels;
using IntegracijskiModul.Mapper;
using IntegracijskiModul.Models;

namespace IntegracijskiModul.Repozitoriji
{

    public class GenreRepo : IGenreRepo
    {
        private readonly DatabaseContext _dbc;

        public GenreRepo(DatabaseContext dbc)
        {
            _dbc = dbc;
        }

        public BLGenre AddGenre(BLGenre g)
        {
            var dbg = GenreMapper.FromBLModelToModel((IEnumerable<BLGenre>)g);

            _dbc.Genres.Add((Modeli.Genre)dbg);

            _dbc.SaveChanges();

            return g;
        }

        public void DeleteGenre(BLGenre bg)
        {
            var d = _dbc.Genres.FirstOrDefault(g => g.Id == bg.Id);

            if (d != null)
            {
                try
                {
                    _dbc.Genres.Remove(d);
                    _dbc.SaveChanges();
                }
                catch
                {
                    throw new ArgumentNullException("Taj žanr ne postoji!");
                }
            }
            else
            {
                throw new ArgumentNullException("Taj žanr ne postoji!");
            }
        }

        public BLGenre EditGenre(BLGenre bg)
        {
            var d = _dbc.Genres.FirstOrDefault(g => g.Id == bg.Id);

            try
            {
                d.Name = bg.Name;
                d.Description = bg.Description;

                _dbc.SaveChanges();

                return bg;
            }
            catch
            {
                throw new ArgumentNullException("Taj žanr ne postoji!");
            }
        }
    }
}
