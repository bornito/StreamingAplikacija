using IntegracijskiModul.BLModels;
using IntegracijskiModul.Mapper;
using IntegracijskiModul.Models;

namespace IntegracijskiModul.Repozitoriji
{
    public class NotificationRepo : INotificationRepo
    {
        private readonly DatabaseContext _dbc;

        public NotificationRepo(DatabaseContext dbc)
        {
            _dbc = dbc;
        }

        public BLNotification Add(BLNotification n)
        {
            var dbn = NotificationMapper.FromBLModelToModel((IEnumerable<BLNotification>)n);

            _dbc.Notifications.Add((Models.Notification)dbn);

            _dbc.SaveChanges();

            return n;
        }

        public void Delete(int idN)
        {
            var t = _dbc.Notifications.FirstOrDefault(n => n.Id == idN);
            if (t != null)
            {
                _dbc.Remove(t);
                _dbc.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Notifikacija nije pronadjena!");
            }
        }

        public IEnumerable<BLNotification> GetAll()
        {
            var dbn = _dbc.Notifications;

            var bln = NotificationMapper.FromModelToBLModel(dbn);

            return bln;
        }

        public BLNotification GetById(int id)
        {
            var dbn = _dbc.Notifications.FirstOrDefault(n => n.Id == id);

            if (dbn == null)
            {
                throw new InvalidOperationException("Notifikacija nije pronadjena!");
            }

            var bln = NotificationMapper.FromModelToBLModel((IEnumerable<Notification>)dbn);

            return (BLNotification)bln;
        }

        public BLNotification Update(int id, BLNotification notification)
        {
            var dbn = _dbc.Notifications.FirstOrDefault(n => n.Id == id);

            if (dbn == null)
            {
                throw new InvalidOperationException("Notifikacija nije pronadjena!");
            }

            dbn.Reciever = notification.Reciever;
            dbn.Subject = notification.Subject;
            dbn.Created = notification.Created;
            dbn.Sent = notification.Sent;
            dbn.EmailBody = notification.EmailBody;

            var bln = NotificationMapper.FromModelToBLModel((IEnumerable<Notification>)dbn);

            return (BLNotification)bln;
        }
    }
}
