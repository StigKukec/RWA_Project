using DataLayer.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IRepositoryFactory
    {
        Lazy<IVideoRepository> VideoRepository { get; }
        Lazy<ITokenRepository> TokenRepository { get; }
        Lazy<IUserRepository> UserRepository { get; }
    }
}
