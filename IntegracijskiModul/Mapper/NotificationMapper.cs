using IntegracijskiModul.BLModels;
using IntegracijskiModul.Models;

namespace IntegracijskiModul.Mapper
{
    public static class NotificationMapper
    {
        // Iz modela u BL model
        public static IEnumerable<BLNotification> FromModelToBLModel(IEnumerable<Notification> mn)
            => (IEnumerable<BLNotification>)mn.Select(n => FromModelToBLModel(n));

        private static object FromModelToBLModel(Notification n)
        {
            return new BLNotification
            {
                Id = n.Id,
                Reciever = n.Reciever,
                Subject = n.Subject,
                EmailBody = n.EmailBody,
                Created = n.Created,
                Sent = n.Sent
            };
        }

        // obrnuto
        public static IEnumerable<Notification> FromBLModelToModel(IEnumerable<BLNotification> bln)
            => (IEnumerable<Notification>)bln.Select(n => FromBLModelToModel(n));

        private static object FromBLModelToModel(BLNotification n)
        {
            return new Notification
            {
                Id = n.Id,
                Reciever = n.Reciever,
                Subject = n.Subject,
                EmailBody = n.EmailBody,
                Created = n.Created,
                Sent = n.Sent
            };
        }
    }
}
