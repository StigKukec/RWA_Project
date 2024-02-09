using DataLayer.DALModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Security
{
    public interface ITokenRepository
    {
        Token AssignToken(string username, string password);
    }
}
