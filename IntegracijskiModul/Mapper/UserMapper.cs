using IntegracijskiModul.BLModels;
using IntegracijskiModul.Modeli;

namespace IntegracijskiModul.Mapper
{
    public static class UserMapper
    {
        // Iz modela u BL model
        public static IEnumerable<BLUser> FromModelToBLModel(IEnumerable<User> mu)
            => (IEnumerable<BLUser>)mu.Select(u => FromModelToBLModel(u));

        private static object FromModelToBLModel(User u)
        {
            return new BLUser
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                Email = u.Email,
                TelephoneNumber = u.TelephoneNumber,
                PasswordHash = u.PasswordHash,
                PasswordSalt = u.PasswordSalt,
                Token = u.Token,
                IsConfirmed = u.IsConfirmed,
                Created = u.Created,
                Deleted = u.Deleted,
                CountryId = u.CountryId
            };
        }

        // obrnuto
        public static IEnumerable<User> FromBLModelToModel(IEnumerable<BLUser> blu)
            => (IEnumerable<User>)blu.Select(u => FromBLModelToModel(u));

        private static object FromBLModelToModel(BLUser u)
        {
            return new User
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                Email = u.Email,
                TelephoneNumber = u.TelephoneNumber,
                PasswordHash = u.PasswordHash,
                PasswordSalt = u.PasswordSalt,
                Token = u.Token,
                IsConfirmed = u.IsConfirmed,
                Created = u.Created,
                Deleted = u.Deleted,
                CountryId = u.CountryId
            };
        }
    }
}
