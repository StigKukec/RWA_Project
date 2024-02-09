using DataLayer.BLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<BLUser> GetAllUsers();
        BLUser GetUser(int idUser);
        BLUser RegisterUser(string firstName, string lastName, string username, string email, string password, int idCountry);
        void AdminCreateUser(string firstName, string lastName, string username, string email, bool verify, string password, int idCountry, bool deactivate, int userType);
        void UpdateUser(int idUser, string firstName, string lastName, string username, string email, bool verify, int idCountry, bool deactivate, int userType);
        void DeleteUser(int idUser);
        bool Authenticate(string username, string password);
        IEnumerable<BLCountry> GetAllCountries();
        IEnumerable<BLUserType> GetAllUserTypes();
        bool UserChangePassword(string username, string oldPassword, string newPassword);
        void AdminChangePassword(int userId, string newPassword);
        IEnumerable<BLCountry> GetPartialCountries(int page, int size, string orderBy, string direction);
        IEnumerable<BLUser> GetPartialUsers(int page, int size, string orderBy, string direction);
        void ValidateEmail(string email, string securityToken);
        IEnumerable<BLNotification> GetAllNotifications();
        BLNotification GetNotification(int id);
        void DeleteNotification(int id);
        void CreateNotification(string receiver, string? subject, string? body);
        void UpdateNotification(int id, string receiver, string? subject, string? body);
        void SendNotifications();
    }
}
