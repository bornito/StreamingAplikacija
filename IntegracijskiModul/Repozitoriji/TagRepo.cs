using IntegracijskiModul.BLModels;
using IntegracijskiModul.Mapper;
using IntegracijskiModul.Models;

namespace IntegracijskiModul.Repozitoriji
{

    public class TagRepo : ITagRepo
    {
        private readonly DatabaseContext _dbc;

        public TagRepo(DatabaseContext dbc)
        {
            _dbc = dbc;
        }

        public BLTag AddTag(BLTag tag)
        {
            var dbt= TagMapper.FromBLModelToModel((IEnumerable<BLTag>)tag);

            _dbc.Tags.Add((Modeli.Tag)dbt);

            _dbc.SaveChanges();

            return tag;
        }

        public void DeleteTag(BLTag tag)
        {
            var t = _dbc.Tags.FirstOrDefault(x => x.Id == tag.Id);
            _dbc.Tags.Remove(t);
            _dbc.SaveChanges();
        }

        public BLTag EditTag(BLTag tag)
        {
            var t = _dbc.Tags.FirstOrDefault(x => x.Id == tag.Id);

            t.Name = tag.Name;

            _dbc.SaveChanges();

            return tag;
        }
    }
}
