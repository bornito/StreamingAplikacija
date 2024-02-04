using IntegracijskiModul.BLModels;

namespace IntegracijskiModul.Repozitoriji
{
    public interface IUserRepo
    {
        IEnumerable<BLUser> GetAll();
        BLUser CreateUser(string userName, string firstName, string lastName, string email, string password, int countryId);
        void SoftDeleteUser(int id);
        void ChangePassword(string username, string newPassword);
        bool CheckUsernameExists(string username);
        bool CheckEmailExists(string email);
        BLUser GetUser(int id);
        void ConfirmEmail(string email, string securityToken);
        BLUser GetConfirmedUser(string username, string password);
    }
}
