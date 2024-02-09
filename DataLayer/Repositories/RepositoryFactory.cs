using AutoMapper;
using DataLayer.DALModels;
using DataLayer.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly RwaDatabaseContext _dbContext;
        private readonly IMapper _mapper;
        public RepositoryFactory(IMapper mapper, RwaDatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        /*
        public static RepositoryFactory Instance { get; private set; }

        public static void Initialize(IMapper mapper, RwaDatabaseContext dbContext)
        {
            Instance ??= new RepositoryFactory(mapper, dbContext);
        }
        */
        /*
        private static readonly Lazy<IRepository> repository = new Lazy<IRepository>(() => new SqlRepository());
        public static IRepository SqlRepository => repository.Value;
        
        private static readonly Lazy<IHashRepository> hashRepository = new Lazy<IHashRepository>(() => new HashGenerator());
        public static IHashRepository HashRepository => hashRepository.Value;

        private static readonly Lazy<IJwtTokenRepository> jwtTokenRepository = new Lazy<IJwtTokenRepository>(() => new JwtTokenGenerator());
        public static IJwtTokenRepository JwtTokenRepository => jwtTokenRepository.Value;
        */
        public Lazy<IVideoRepository> VideoRepository => new Lazy<IVideoRepository>(() => new VideoRepository(_dbContext, _mapper));
        public Lazy<ITokenRepository> TokenRepository => new Lazy<ITokenRepository>(() => new TokenManager(_dbContext, _mapper));
        public Lazy<IUserRepository> UserRepository => new Lazy<IUserRepository>(() => new UserRepository(_dbContext, _mapper));
    }
}
