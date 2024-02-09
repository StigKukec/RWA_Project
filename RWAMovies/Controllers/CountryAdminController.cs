using AutoMapper;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RWAMovies.ViewModels;
using System.Data;
using System.Drawing;

namespace RWAMovies.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CountryAdminController : Controller
    {
        private readonly ILogger<CountryAdminController> _logger;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        #region constants
        private const string countryTablePartial = "_CountryTableBodyPartial";
        private const int page = 0;
        private const int size = 10;
        private const string orderBy = "";
        private const string direction = "";
        #endregion

        public CountryAdminController(ILogger<CountryAdminController> logger, IUserRepository userRepo, IMapper mapper)
        {
            _logger = logger;
            _userRepo = userRepo;
            _mapper = mapper;
        }
        // GET: CountryController
        public ActionResult Index()
        {
            var blCountries = _userRepo.GetPartialCountries(page, size, orderBy, direction);
            var vmCountries = _mapper.Map<IEnumerable<VMCountry>>(blCountries);

            ViewBag.Page = page;
            ViewBag.Size = size;
            ViewBag.OrderBy = orderBy;
            ViewBag.Direction = direction;
            ViewBag.Pages = (vmCountries.Count() / size);

            return View(vmCountries);
        }
        public ActionResult CountryTablePartial(int page, int size, string orderBy, string direction)
        {
            if (size == 0)
            {
                size = 10;
            }
            var blCountries = _userRepo.GetPartialCountries(page, size, orderBy, direction);
            var vmCountries = _mapper.Map<IEnumerable<VMCountry>>(blCountries);

            return PartialView(countryTablePartial, vmCountries);
        }

        // GET: CountryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CountryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CountryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CountryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CountryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CountryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CountryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
