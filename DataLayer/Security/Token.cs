using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Security
{
    public class Token : JwtSecurityToken
    {
        private static readonly IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        public string SerializedToken { get; set; }

        public static IConfiguration Configuration => configuration;

        public static string GetKey()
        {
            try
            {
                return Configuration["JWT:Key"]!;
            }
            catch (Exception)
            {

                throw new Exception("[Worng JWT key]");
            }
        }
        public static string GetIssuer()
        {
            try
            {
                return Configuration["JWT:Issuer"]!;

            }
            catch (Exception)
            {

                throw new Exception("[Worng issuer]");
            }
        }
        public static string GetAudience()
        {
            try
            {
                return Configuration["JWT:Audience"]!;

            }
            catch (Exception)
            {

                throw new Exception("[Worng audience]");
            }
        }
    }
}
