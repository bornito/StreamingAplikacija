using IntegracijskiModul.BLModels;
using IntegracijskiModul.Mapper;
using IntegracijskiModul.Modeli;
using IntegracijskiModul.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace IntegracijskiModul.Repozitoriji
{
    public class UserRepo : IUserRepo
    {
        private readonly DatabaseContext _dbc;

        public UserRepo(DatabaseContext dbc)
        {
            _dbc = dbc;
        }

        public void ChangePassword(string username, string newPassword)
        {
            var userToChangePassword 
                = _dbc.Users.FirstOrDefault(x => x.UserName == username && x.Deleted == null);

            (var salt, var b64Salt) = GenerateSalt();

            var b64Hash = CreateHash(newPassword, salt);

            userToChangePassword.PasswordHash = b64Hash;
            userToChangePassword.PasswordSalt = b64Salt;

            _dbc.SaveChanges();
        }

        public bool CheckEmailExists(string email)
            => _dbc.Users.Any(x => x.Email == email && x.Deleted == null);

        public bool CheckUsernameExists(string username)
            => _dbc.Users.Any(x => x.UserName == username && x.Deleted == null);

        public void ConfirmEmail(string email, string securityToken)
        {
            var userToConfirm = _dbc.Users.FirstOrDefault(x =>
                                x.Email == email &&
                                x.Token == securityToken &&
                                x.Deleted == null);

            userToConfirm.IsConfirmed = true;

            _dbc.SaveChanges();
        }

        public BLUser CreateUser(string userName, string firstName, string lastName, string email, string password, int countryId)
        {
            (var salt, var b64Salt) = GenerateSalt();
            var b64Hash = CreateHash(password, salt);
            var b64SecToken = GenerateSecurityToken();

            var c = _dbc.Countries.FirstOrDefault(x => x.Id == countryId);

            if (c == null)
            {
                throw new NotImplementedException();
            }

            var dbUser = new User
            {
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = b64Hash,
                PasswordSalt = b64Salt,
                Created = DateTime.UtcNow,
                Token = b64SecToken,
                Country = c,
                CountryId = countryId
            };

            _dbc.Users.Add(dbUser);

            _dbc.SaveChanges();

            var bLUser = UserMapper.FromModelToBLModel((IEnumerable<User>)dbUser);

            return (BLUser)bLUser;
        }
        private static string GenerateSecurityToken()
        {
            byte[] securityToken = RandomNumberGenerator.GetBytes(256 / 8);
            string b64SecToken = Convert.ToBase64String(securityToken);

            return b64SecToken;
        }

        private static string CreateHash(string password, byte[] salt)
        {

            byte[] hash =
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8);
            string b64Hash = Convert.ToBase64String(hash);

            return b64Hash;
        }

        private static (byte[], string) GenerateSalt()
        {

            var salt = RandomNumberGenerator.GetBytes(128 / 8);
            var b64Salt = Convert.ToBase64String(salt);

            return (salt, b64Salt);
        }

        public IEnumerable<BLUser> GetAll()
        {
            var dbUsers = _dbc.Users;

            var blUsers = UserMapper.FromModelToBLModel(dbUsers);

            return blUsers;
        }

        public BLUser GetConfirmedUser(string username, string password)
        {
            var dbUser = _dbc.Users.FirstOrDefault(x => x.UserName == username && x.IsConfirmed == true);

            if (dbUser == null)
            {
                throw new InvalidOperationException("Korisnik nije pronadjen!");
            }

            var salt = Convert.FromBase64String(dbUser.PasswordSalt);

            var b64hash = CreateHash(password, salt);

            if (dbUser.PasswordHash != b64hash)
            {
                throw new InvalidOperationException("Lozinka neispravna!");
            }

            var blUser = UserMapper.FromModelToBLModel((IEnumerable<User>)dbUser);

            return (BLUser)blUser;
        }

        public BLUser GetUser(int idUser)
        {
            var t = _dbc.Users.FirstOrDefault(x => x.Id == idUser);

            if (t == null)
            {
                throw new InvalidOperationException("Korisnik nije pronadjen!");
            }

            var blUser = UserMapper.FromModelToBLModel((IEnumerable<User>)t);

            return (BLUser)blUser;
        }

        public void SoftDeleteUser(int idUser)
        {
            var dbUser = _dbc.Users.FirstOrDefault(x => x.Id == idUser);

            if (dbUser == null)
            {
                throw new InvalidOperationException("Korisnik nije pronadjen!");
            }

            dbUser.Deleted = DateTime.UtcNow;

            _dbc.SaveChanges();
        }
    }
}
