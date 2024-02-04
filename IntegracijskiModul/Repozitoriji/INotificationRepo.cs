using IntegracijskiModul.BLModels;

namespace IntegracijskiModul.Repozitoriji
{
    public interface INotificationRepo
    {
        IEnumerable<BLNotification> GetAll();
        BLNotification GetById(int id);
        BLNotification Add(BLNotification notification);
        BLNotification Update(int id, BLNotification notification);
        void Delete(int id);
    }
}
