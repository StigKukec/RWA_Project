using AutoMapper;
using DataLayer.Repositories;
using IntegrationModule.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationModule.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryRepository;
        public UserController(IMapper mapper, IRepositoryFactory repositoryRepository)
        {
            _mapper = mapper;
            _repositoryRepository = repositoryRepository;
        }

        [HttpPost("[action]")]
        public ActionResult SignIn(MUserSignInRequest request)
        {
            try
            {
                return Ok(_repositoryRepository.TokenRepository.Value.AssignToken(request.Username, request.Password));
            }
            catch (InvalidOperationException ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost("[action]")]
        public ActionResult Register([FromBody] MUserRegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var users = _repositoryRepository.UserRepository.Value.GetAllUsers();
                if (users.Any(x => x.Username.Equals(request.Username)))
                {
                    throw new InvalidOperationException("Username already exists");
                }
                var blUser = _repositoryRepository.UserRepository.Value.RegisterUser(request.FirstName, request.LastName, request.Username, request.Email, request.Password, request.CountryId);
                var mUser = _mapper.Map<MUser>(blUser);

                var link = $"api/users/EmailVerification?email={mUser.Email}securityToken={mUser.SecurityToken}";
               
                return Ok(link);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
       [HttpPost("[action]")]
       public ActionResult EmailVerification(string email, string securityToken)
       {
           try
           {
                _repositoryRepository.UserRepository.Value.ValidateEmail(email,securityToken);
               return Ok();
           }
           catch (InvalidOperationException ex)
           {
               return BadRequest(ex.Message);
           }
       }
    }
}
