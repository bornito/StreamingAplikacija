using IntegracijskiModul.BLModels;
using IntegracijskiModul.Modeli;

namespace IntegracijskiModul.Mapper
{
    public static class TagMapper
    {
        // Iz modela u BL model
        public static IEnumerable<BLTag> FromModelToBLModel(IEnumerable<Tag> mt)
            => (IEnumerable<BLTag>)mt.Select(t => FromModelToBLModel(t));

        private static object FromModelToBLModel(Tag t)
        {
            return new BLTag
            {
                Id = t.Id,
                Name = t.Name
            };
        }

        // obrnuto
        public static IEnumerable<Tag> FromBLModelToModel(IEnumerable<BLTag> blt)
            => (IEnumerable<Tag>)blt.Select(t => FromBLModelToModel(t));

        private static object FromBLModelToModel(BLTag t)
        {
            return new Tag
            {
                Id = t.Id,
                Name = t.Name
            };
        }
    }
}
