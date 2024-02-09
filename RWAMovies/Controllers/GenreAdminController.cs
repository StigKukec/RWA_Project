using AutoMapper;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RWAMovies.ViewModels;
using System.Data;

namespace RWAMovies.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class GenreAdminController : Controller
    {
        private readonly ILogger<GenreAdminController> _logger;
        private readonly IVideoRepository _videoRepo;
        private readonly IMapper _mapper;
        #region constants
        private const string genresTableBodyPartial = "_GenresTableBodyPartial";
        private const int page = 0;
        private const int size = 10;
        #endregion
        public GenreAdminController(ILogger<GenreAdminController> logger, IVideoRepository videoRepo, IMapper mapper)
        {
            _logger = logger;
            _videoRepo = videoRepo;
            _mapper = mapper;
        }
        // GET: GenreAdminController
        public ActionResult Index()
        {
            var blGenres = _videoRepo.GetAllGenres();
            var vmGenres = _mapper.Map<IEnumerable<VMGenre>>(blGenres);

            ViewBag.Size = size;
            ViewBag.Page = page;
            ViewBag.Pages = (vmGenres.Count() / size);

            return View(vmGenres);
        }
        public IActionResult GenresTableBodyPartial(int page, int size, string orderBy, string direction)
        {
            if (size.Equals(0))
                size = 10;

            var blGenres = _videoRepo.GetPartialGenres(page, size);
            var vmGenres = _mapper.Map<IEnumerable<VMGenre>>(blGenres);

            return PartialView(genresTableBodyPartial, vmGenres);
        }
        public IActionResult AllGenresTableBodyPartial()
        {
            var dalGenres = _videoRepo.GetAllGenres();
            var vmGenres = _mapper.Map<IEnumerable<VMGenre>>(dalGenres);

            return PartialView(genresTableBodyPartial, vmGenres);
        }
        // GET: GenreAdminController/Details/5
        public ActionResult Details(int id)
        {
            var dalGenre = _videoRepo.GetGenre(id);
            var vmGenre = _mapper.Map<VMGenre>(dalGenre);

            return View(vmGenre);
        }

        // GET: GenreAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenreAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMGenreCreate genre)
        {
            try
            {
                _videoRepo.CreateGenre(genre.Name,genre.Description);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GenreAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            var dalGenre = _videoRepo.GetGenre(id);
            var vmGenre = _mapper.Map<VMGenre>(dalGenre);

            return View(vmGenre);
        }

        // POST: GenreAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VMGenre genre)
        {
            try
            {
                _videoRepo.UpdateGenre(id,genre.Name,genre.Description);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GenreAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            var dalGenre = _videoRepo.GetGenre(id);
            var vmGenre = _mapper.Map<VMGenre>(dalGenre);

            return View(vmGenre);
        }

        // POST: GenreAdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _videoRepo.DeleteGenre(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
