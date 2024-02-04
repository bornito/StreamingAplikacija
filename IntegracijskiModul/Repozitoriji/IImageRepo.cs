using IntegracijskiModul.BLModels;

namespace IntegracijskiModul.Repozitoriji
{
    public interface IImageRepo
    {
        IEnumerable<BLImage> GetAll();
        BLImage GetById(int id);
        BLImage Add(BLImage image);
        BLImage Update(int id, BLImage image);
        void Delete(int id);
    }
}
