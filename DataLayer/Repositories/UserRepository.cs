using AutoMapper;
using DataLayer.BLModels;
using DataLayer.DALModels;
using DataLayer.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace DataLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RwaDatabaseContext _dbContext;
        private readonly IMapper _mapper;
        public UserRepository(RwaDatabaseContext dbContext, IMapper mapper)
        {

            _dbContext = dbContext;
            _mapper = mapper;

        }
        public IEnumerable<BLUser> GetAllUsers()
        {
            var dbUsers = _dbContext.Users
                .Include(x => x.Country)
                .Include(x => x.UserType);

            var blUsers = _mapper.Map<IEnumerable<BLUser>>(dbUsers);

            return blUsers;
        }

        public BLUser GetUser(int idUser)
        {
            var dalUser = _dbContext.Users.Include(x => x.Country).Include(x => x.UserType).FirstOrDefault(x => x.Iduser.Equals(idUser));
            var blUser = _mapper.Map<BLUser>(dalUser);

            return blUser;
        }
        public BLUser RegisterUser(string firstName, string lastName, string username, string email, string password, int idCountry)
        {
            HashManager hashManager = new();
            var userType = _dbContext.UserTypes.First(x => x.Type.Equals("Member"));
            var dalUser = new User
            {
                Created = DateTime.UtcNow,
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Email = email,
                Verified = false,
                PwdSalt = hashManager.SaltPwd,
                PwdHash = hashManager.GetHashPwd(password),
                SecurityToken = HashManager.GetSecurityToken(),
                CountryId = idCountry,
                Deactivated = false,
                UserTypeId = userType.IduserType

            };
            _dbContext.Users.Add(dalUser);
            _dbContext.SaveChanges();

            var blUser = _mapper.Map<BLUser>(dalUser);
            return blUser;
        }
        public void AdminCreateUser(string firstName, string lastName, string username, string email, bool verify, string password, int idCountry, bool deactivate, int userType)
        {
            HashManager hashManager = new();
            var dalUser = new User
            {
                Created = DateTime.UtcNow,
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Email = email,
                Verified = verify,
                PwdSalt = hashManager.SaltPwd,
                PwdHash = hashManager.GetHashPwd(password),
                SecurityToken = HashManager.GetSecurityToken(),
                CountryId = idCountry,
                Deactivated = deactivate,
                UserTypeId = userType

            };
            if (deactivate.Equals(true))
            {
                dalUser.DeactivatedAt = DateTime.UtcNow;
            }
            _dbContext.Users.Add(dalUser);
            _dbContext.SaveChanges();
        }

        public void UpdateUser(int idUser, string firstName, string lastName, string username, string email, bool verify, int idCountry, bool deactivate, int userType)
        {
            var user = _dbContext.Users.Find(idUser);

            if (user != null)
            {
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Username = username;
                user.Email = email;
                user.Verified = verify;
                user.CountryId = idCountry;
                user.Deactivated = deactivate;
                user.DeactivatedAt = null;
                user.UserTypeId = userType;

                if (deactivate.Equals(true))
                {
                    user.DeactivatedAt = DateTime.UtcNow;
                }

                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
        }

        public void DeleteUser(int idUser)
        {
            var user = new User
            {
                Iduser = idUser
            };
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }
        public bool Authenticate(string username, string password)
        {
            IEnumerable<User> dbUsers = _dbContext.Users;

            return HashManager.Authenticate(dbUsers, username, password);
        }
        public bool UserChangePassword(string username, string oldPassword, string newPassword)
        {
            HashManager hashManager = new();
            if (!Authenticate(username, oldPassword))
            {
                return false;
            }
            var users = _dbContext.Users;
            var user = users.First(u => u.Username.Equals(username));

            user.PwdSalt = hashManager.SaltPwd;
            user.PwdHash = hashManager.GetHashPwd(newPassword);

            _dbContext.Users.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return true;
        }
        public void AdminChangePassword(int userId, string newPassword)
        {
            HashManager hashManager = new();

            var users = _dbContext.Users;
            var user = users.First(u => u.Iduser.Equals(userId));

            user.PwdSalt = hashManager.SaltPwd;
            user.PwdHash = hashManager.GetHashPwd(newPassword);

            _dbContext.Users.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public IEnumerable<BLCountry> GetAllCountries()
        {
            var dbCountries = _dbContext.Countries;

            var blCountries = _mapper.Map<IEnumerable<BLCountry>>(dbCountries);

            return blCountries;
        }
        public IEnumerable<BLUserType> GetAllUserTypes()
        {
            var dbUserTypes = _dbContext.UserTypes;

            var blUserTypes = _mapper.Map<IEnumerable<BLUserType>>(dbUserTypes);

            return blUserTypes;
        }
        public IEnumerable<BLCountry> GetPartialCountries(int page, int size, string orderBy, string direction)
        {
            var dbCountries = _dbContext.Countries;

            var countryPage = dbCountries.Skip(page * size).Take(size);

            var blCountries = _mapper.Map<IEnumerable<BLCountry>>(countryPage);

            return blCountries;
        }

        public IEnumerable<BLUser> GetPartialUsers(int page, int size, string orderBy, string direction)
        {
            IEnumerable<User> dbUsers = _dbContext.Users
                .Include(x => x.Country)
                .Include(x => x.UserType);

            if (string.Compare(orderBy, nameof(User.FirstName), true).Equals(0))
            {
                dbUsers = dbUsers.OrderBy(x => x.FirstName);
            }
            else if (string.Compare(orderBy, nameof(User.LastName), true).Equals(0))
            {
                dbUsers = dbUsers.OrderBy(x => x.LastName);
            }
            else if (string.Compare(orderBy, nameof(User.Username), true).Equals(0))
            {
                dbUsers = dbUsers.OrderBy(x => x.Username);
            }
            else if (string.Compare(orderBy, nameof(User.Country), true).Equals(0))
            {
                dbUsers = dbUsers.Where(x => x.Country.Name.Equals(direction));
            }
            else
            {
                dbUsers = dbUsers.OrderBy(x => x.Iduser);
            }

            if (string.Compare(direction, "descending", true).Equals(0))
            {
                dbUsers = dbUsers.Reverse();
            }

            var usersPage = dbUsers.Skip(page * size).Take(size);

            var blUsers = _mapper.Map<IEnumerable<BLUser>>(usersPage);

            return blUsers;
        }

        public void ValidateEmail(string email, string securityToken)
        {
            var validUser = _dbContext.Users.FirstOrDefault(x => x.Email.Equals(email) && x.SecurityToken.Equals(securityToken));
            if (validUser == null)
            {
                return;
            }

            validUser.Verified = true;

            _dbContext.Users.Entry(validUser).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public IEnumerable<BLNotification> GetAllNotifications()
        {
            var dbNotifications = _dbContext.Notifications;

            var blNotifications = _mapper.Map<IEnumerable<BLNotification>>(dbNotifications);

            return blNotifications;
        }

        public BLNotification GetNotification(int id)
        {
            var dalNotification = _dbContext.Notifications.FirstOrDefault(x => x.Idnotification.Equals(id));
            var blNotification = _mapper.Map<BLNotification>(dalNotification);

            return blNotification;
        }
        public void CreateNotification(string receiver, string? subject, string? body)
        {
            var notification = new Notification
            {
                CreatedAt = DateTime.Now,
                Guid = Guid.NewGuid(),
                Sender = receiver,
                Receiver = receiver,
                Subject = subject,
                Body = body,
                SentAt = null
            };

            _dbContext.Notifications.Add(notification);
            _dbContext.SaveChanges();
        }

        public void UpdateNotification(int id, string receiver, string? subject, string? body)
        {
            var notification = _dbContext.Notifications.FirstOrDefault(x => x.Idnotification.Equals(id));
            if (notification != null)
            {
                notification.Sender = receiver;
                notification.Receiver = receiver;
                notification.Subject = subject;
                notification.Body = body;
            }

            _dbContext.Entry(notification).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public void DeleteNotification(int id)
        {
            var notification = new Notification
            {
                Idnotification = id,
            };
            _dbContext.Notifications.Remove(notification);
            _dbContext.SaveChanges();
        }

        public void SendNotifications()
        {
            var notifications = _dbContext.Notifications;
            var notSentNotifications = notifications.Where(x => x.SentAt == null);

            notSentNotifications.ToList().ForEach(x => x.SentAt = DateTime.UtcNow);

           // _dbContext.Entry(notSentNotifications).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
