using IntegracijskiModul.BLModels;

namespace IntegracijskiModul.Repozitoriji
{
    public interface ITagRepo
    {
        BLTag AddTag(BLTag tag);
        BLTag EditTag(BLTag tag);
        void DeleteTag(BLTag tag);
    }
}
