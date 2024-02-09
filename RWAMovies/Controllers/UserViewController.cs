using AutoMapper;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RWAMovies.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Web;

namespace RWAMovies.Controllers
{
    [Authorize(Roles = "Administrator,Member")]
    public class UserViewController : Controller
    {
        private readonly ILogger<UserViewController> _logger;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;
        #region constants
        private const string videosTabPartial = "_VideosTabPartial";
        private const int page = 0;
        private const int size = 10;
        private const string orderBy = "";
        private const string direction = "";
        #endregion
        public UserViewController(ILogger<UserViewController> logger, IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
        }

        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(VMLogin login)
        {
            try
            {
                if (_repositoryFactory.UserRepository.Value.Authenticate(login.Username, login.Password))
                {
                    var blUsers = _repositoryFactory.UserRepository.Value.GetAllUsers();
                    var blUser = blUsers.FirstOrDefault(x => x.Username.Equals(login.Username));
                    var vmUser = _mapper.Map<VMUserProfile>(blUser);
                    string memberSince = vmUser.Created.ToString("dd-MM-yyyy");

                    var claims = new List<Claim>
                    {
                        new Claim("CreatedUserDate", memberSince),
                        new Claim("FirstName", vmUser.FirstName),
                        new Claim("LastName", vmUser.LastName),
                        new Claim(ClaimTypes.Name, login.Username),
                        new Claim(ClaimTypes.Email, vmUser.Email),
                        new Claim(ClaimTypes.Country, vmUser.Country.Name),
                        new Claim(ClaimTypes.Role, vmUser.UserType.Type)

                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        //IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                    };

                    HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties).Wait();

                    //Token JWT = _repositoryFactory.TokenRepository.Value.AssignToken(login.Username, login.Password);
                    //HttpContext.Items["JwtToken"] = JWT.SerializedToken;
                    //HttpContext.SignInAsync(jwt)
                    /*
                    Response.Cookies.Append("jwtToken", JWT.SerializedToken, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddMinutes(5),
                        HttpOnly = true,
                        Secure = true, // samo preko HTTPS-a
                        SameSite = SameSiteMode.Strict
                    });
                    */
                    // Primjer koda za dodavanje JWT tokena u HTTP zaglavlje na serveru
                    //HttpClient client = new HttpClient();
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWT.SerializedToken);

                    // Slanje zahtjeva
                    //HttpResponseMessage response = await client.GetAsync("VideoAdmin/Index");

                    return vmUser.UserType.Type switch
                    {
                        "Administrator" => RedirectToAction("Index", "VideoAdmin"),
                        "Member" => RedirectToAction(nameof(PageVideos)),
                        _ => RedirectToAction(nameof(LogIn)),
                    };
                }
                return RedirectToAction(nameof(LogIn));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut(VMLogin login)
        {
            try
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
                return RedirectToAction(nameof(LogIn));
            }
            catch
            {
                return View();
            }
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            var blCountries = _repositoryFactory.UserRepository.Value.GetAllCountries();
            var vmCountries = _mapper.Map<IEnumerable<VMCountry>>(blCountries);
            ViewBag.Countries = new SelectList(vmCountries, "Idcountry", "Name");
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(VMRegister register)
        {
            try
            {

                var blRegisteredUser = _repositoryFactory.UserRepository.Value.RegisterUser(register.FirstName, register.LastName, register.Username, register.Email, register.Password, register.Country.Idcountry);
                var vmRegisteredUser = _mapper.Map<VMUser>(blRegisteredUser);
                var encodedSecurityToken = HttpUtility.UrlEncode(vmRegisteredUser.SecurityToken);
                return RedirectToAction("ValidateEmail", new { email = vmRegisteredUser.Email, securityToken = encodedSecurityToken });

                //ViewBag.UserExists = true;
                //return View();
            }
            catch
            {
                ViewBag.FailedRegistration = true;
                return View();
            }
        }
        [AllowAnonymous]
        public ActionResult ValidateEmail(string email, string securityToken)
        {
            if (email.IsNullOrEmpty() || securityToken.IsNullOrEmpty())
            {
                return RedirectToAction(nameof(LogIn));
            }
            var decodedSecurityToken = HttpUtility.UrlDecode(securityToken);

            ViewBag.Email = email;
            ViewBag.SecurityToken = decodedSecurityToken;

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ValidateEmail(VMValidateEmail validateUser)
        {
            try
            {
                _repositoryFactory.UserRepository.Value.ValidateEmail(validateUser.Email, validateUser.SecurityToken);

                return RedirectToAction(nameof(LogIn));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult UserProfile()
        {
            /*
            var blUsers = _repositoryFactory.UserRepository.Value.GetAllUsers();
            var blUser = blUsers.FirstOrDefault(x => x.Username.Equals(_username));
            var vmUser = _mapper.Map<VMUserProfile>(blUser);
            */
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(VMChangePassword password)
        {
            try
            {
                var changedPassword = _repositoryFactory.UserRepository.Value.UserChangePassword(password.Username, password.OldPassword, password.NewPassword);
                if (!changedPassword)
                {
                    return View();
                }

                return RedirectToAction(nameof(UserProfile));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult PageVideos()
        {
            var blGenres = _repositoryFactory.VideoRepository.Value.GetAllGenres();
            var vmGenres = _mapper.Map<IEnumerable<VMGenre>>(blGenres);

            var blVideoCount = _repositoryFactory.VideoRepository.Value.GetAllVideos();
            var vmVideoCount = _mapper.Map<IEnumerable<VMVideo>>(blVideoCount);
            var blVideos = _repositoryFactory.VideoRepository.Value.GetPartialData(page, size, orderBy, direction);
            var vmVideos = _mapper.Map<IEnumerable<VMVideo>>(blVideos);

            ViewBag.Page = page;
            ViewBag.Size = size;
            ViewBag.OrderBy = orderBy;
            ViewBag.Direction = direction;
            ViewBag.Pages = (vmVideoCount.Count() / size);

            ViewBag.Genres = vmGenres;

            return View(vmVideos);
        }
        public ActionResult VideoPreview(int id)
        {
            var blVideo = _repositoryFactory.VideoRepository.Value.GetVideo(id);
            var vmVideo = _mapper.Map<VMVideo>(blVideo);
            if (vmVideo == null)
            {
                return RedirectToAction(nameof(PageVideos));
            }
            return View(vmVideo);
        }
        public IActionResult VideosTabPartial(int page, int size, string orderBy, string direction)
        {
            if (size.Equals(0))
                size = 10;

            var blVideos = _repositoryFactory.VideoRepository.Value.GetPartialData(page, size, orderBy, direction);
            var vmVideos = _mapper.Map<IEnumerable<VMVideo>>(blVideos);


            ViewBag.Page = page;
            ViewBag.Size = size;
            ViewBag.OrderBy = orderBy;
            ViewBag.Direction = direction;
            ViewBag.Pages = (vmVideos.Count() / size);


            return PartialView(videosTabPartial, vmVideos);
        }
        public IActionResult AllVideosTabPartial()
        {
            var blVideos = _repositoryFactory.VideoRepository.Value.GetAllVideos();
            var vmVideos = _mapper.Map<IEnumerable<VMVideo>>(blVideos);

            return PartialView(videosTabPartial, vmVideos);
        }
        public IActionResult VideoTabPartial(int idVideo)
        {
            var blVideo = _repositoryFactory.VideoRepository.Value.GetVideo(idVideo);
            var vmVideo = _mapper.Map<VMVideo>(blVideo);
            List<VMVideo> vmVideos = new()
            {
                vmVideo
            };
            return PartialView(videosTabPartial, vmVideos);
        }
        public IActionResult SearchVideosTabPartial(int page, int size, string orderBy, string direction, string search)
        {
            if (size.Equals(0))
                size = 10;

            var blVideos = _repositoryFactory.VideoRepository.Value.GetSearchedData(page, size, orderBy, direction, search);
            var vmVideos = _mapper.Map<IEnumerable<VMVideo>>(blVideos);

            return PartialView(videosTabPartial, vmVideos);
        }
        public IActionResult VideoJson()
        {
            var blVideos = _repositoryFactory.VideoRepository.Value.GetAllVideos();
            var vmVideos = _mapper.Map<IEnumerable<VMVideo>>(blVideos);
            var json = vmVideos.Select(x => new { idVideo = x.IDVideo, name = x.Name, value = x.StreamingUrl });
            //var json = JsonConvert.SerializeObject(vmVideos);
            return Json(json);
        }
        public IActionResult UsersJson()
        {
            var blUsers = _repositoryFactory.UserRepository.Value.GetAllUsers();
            var vmUsers = _mapper.Map<IEnumerable<VMUser>>(blUsers);
            var json = vmUsers.Select(x => new { iduser = x.Iduser, username = x.Username });
            return Json(json);
        }
    }
}
