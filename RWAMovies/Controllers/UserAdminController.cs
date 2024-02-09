using AutoMapper;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RWAMovies.ViewModels;

namespace RWAMovies.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserAdminController : Controller
    {
        private readonly ILogger<UserAdminController> _logger;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        #region constants
        private const string usersTableBodyPartial = "_UsersTableBodyPartial";
        private const int page = 0;
        private const int size = 10;
        private const string orderBy = "";
        private const string direction = "";
        #endregion

        public UserAdminController(ILogger<UserAdminController> logger, IUserRepository userRepo, IMapper mapper)
        {
            _logger = logger;
            _userRepo = userRepo;
            _mapper = mapper;
        }
        // GET: UserAdminController
        public ActionResult Index()
        {
            var blUsersCount= _userRepo.GetAllUsers();
            var vmUserCount = _mapper.Map<IEnumerable<VMUser>>(blUsersCount);
            var blUsers = _userRepo.GetPartialUsers(page, size, orderBy, direction);
            var vmUsers = _mapper.Map<IEnumerable<VMUser>>(blUsers);

            var blCountries = _userRepo.GetAllCountries();
            var vmCountries = _mapper.Map<IEnumerable<VMCountry>>(blCountries);

            ViewBag.Page = page;
            ViewBag.Size = size;
            ViewBag.OrderBy = orderBy;
            ViewBag.Direction = direction;
            ViewBag.Pages = (vmUserCount.Count() / size);

            ViewBag.Countries = vmCountries;

            return View(vmUsers);
        }
        public IActionResult UsersTableBodyPartial(int page, int size, string orderBy, string direction)
        {
            if (size.Equals(0))
                size = 10;

            var blUsers = _userRepo.GetPartialUsers(page, size, orderBy, direction);
            var vmUsers = _mapper.Map<IEnumerable<VMUser>>(blUsers);

            return PartialView(usersTableBodyPartial, vmUsers);
        }
        public IActionResult UserTableBodyPartial(int idUser)
        {
            if (idUser.Equals(0))
                return BadRequest();

            var blUser = _userRepo.GetUser(idUser);
            var vmUser = _mapper.Map<VMUser>(blUser);
            List<VMUser> vmUsers = new()
            {
                vmUser
            };
            return PartialView(usersTableBodyPartial, vmUsers);
        }
        public IActionResult AllUsersTableBodyPartial()
        {
            var blUsers = _userRepo.GetAllUsers();
            var vmUsers = _mapper.Map<IEnumerable<VMUser>>(blUsers);

            return PartialView(usersTableBodyPartial, vmUsers); ;
        }
        // GET: UserAdminController/Details/5
        public ActionResult Details(int id)
        {
            var blUsers = _userRepo.GetUser(id);
            var vmUsers = _mapper.Map<VMUser>(blUsers);

            return View(vmUsers);
        }

        // GET: UserAdminController/Create
        public ActionResult Create()
        {
            var blCountries = _userRepo.GetAllCountries();
            var vmCountries = _mapper.Map<IEnumerable<VMCountry>>(blCountries);
            ViewBag.Countries = new SelectList(vmCountries, nameof(VMCountry.Idcountry), nameof(VMCountry.Name));

            var blUserTypes = _userRepo.GetAllUserTypes();
            var vmUserTypes = _mapper.Map<IEnumerable<VMUserType>>(blUserTypes);
            ViewBag.UserTypes = new SelectList(vmUserTypes, nameof(VMUserType.IduserType), nameof(VMUserType.Type));

            return View();
        }

        // POST: UserAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMUserCreate user)
        {
            try
            {
                _userRepo.AdminCreateUser(user.FirstName, user.LastName, user.Username, user.Email, user.Verify, user.Password, user.Country.Idcountry, user.Deactivate, user.UserType.IduserType);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            var blUsers = _userRepo.GetUser(id);
            var vmUsers = _mapper.Map<VMUserEdit>(blUsers);

            var blCountries = _userRepo.GetAllCountries();
            var vmCountries = _mapper.Map<IEnumerable<VMCountry>>(blCountries);
            ViewBag.Countries = new SelectList(vmCountries, nameof(VMCountry.Idcountry), nameof(VMCountry.Name));

            var blUserTypes = _userRepo.GetAllUserTypes();
            var vmUserTypes = _mapper.Map<IEnumerable<VMUserType>>(blUserTypes);
            ViewBag.UserTypes = new SelectList(vmUserTypes, nameof(VMUserType.IduserType), nameof(VMUserType.Type));

            return View(vmUsers);
        }

        // POST: UserAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VMUserEdit user)
        {
            try
            {
                _userRepo.UpdateUser(id, user.FirstName, user.LastName, user.Username, user.Email, user.Verified, user.Country.Idcountry, user.Deactivated, user.UserType.IduserType);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult AdminChangePassword(int id)
        {
            ViewBag.Id = id;

            return View();
        }

        // POST: UserAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminChangePassword(VMAdminChangePassword password)
        {
            try
            {
                _userRepo.AdminChangePassword(password.UserId, password.NewPassword);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            var blUsers = _userRepo.GetUser(id);
            var vmUsers = _mapper.Map<VMUser>(blUsers);

            return View(vmUsers);
        }

        // POST: UserAdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _userRepo.DeleteUser(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
