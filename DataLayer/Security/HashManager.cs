using DataLayer.DALModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace DataLayer.Security
{
    public class HashManager
    {
        public string SaltPwd { get; set; }
        //public string HashPwd { get; set; }
        public HashManager() => SaltPwd = GetSaltPwd();
        private static string GetSaltPwd()
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            string b64Salt = Convert.ToBase64String(salt);
            return b64Salt;
        }
        public string GetHashPwd(string password)
        {
            byte[] salt = Convert.FromBase64String(SaltPwd);
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
        public static string GetSecurityToken()
        {
            byte[] securityToken = RandomNumberGenerator.GetBytes(256 / 8);
            string b64SecToken = Convert.ToBase64String(securityToken);
            return b64SecToken;
        }
        public static bool Authenticate(IEnumerable<User> users, string usrInput, string pwdInput)
        {
            var user = users.Single(u => u.Username.Equals(usrInput));
            //staviti ! u if dole kada napravim verifikaciju emailom
            if (user.Deactivated /*&& user.Verified*/)
            {
                return false;
            }

            byte[] salt = Convert.FromBase64String(user.PwdSalt);
            byte[] hash = Convert.FromBase64String(user.PwdHash);
            byte[] calcHash =
            KeyDerivation.Pbkdf2(
                password: pwdInput,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8);

            return hash.SequenceEqual(calcHash);

        }
    }
}
