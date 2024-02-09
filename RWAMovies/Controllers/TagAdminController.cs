using AutoMapper;
using DataLayer.BLModels;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RWAMovies.ViewModels;
using System.Data;

namespace RWAMovies.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class TagAdminController : Controller
    {
        private readonly ILogger<TagAdminController> _logger;
        private readonly IVideoRepository _videoRepo;
        private readonly IMapper _mapper;

        public TagAdminController(ILogger<TagAdminController> logger, IVideoRepository userRepo, IMapper mapper)
        {
            _logger = logger;
            _videoRepo = userRepo;
            _mapper = mapper;
        }
        // GET: TagAdminController
        public ActionResult Index()
        {
            var dalTag = _videoRepo.GetAllTags();
            var vmTag = _mapper.Map<IEnumerable<VMTag>>(dalTag);
            return View(vmTag);
        }

        // GET: TagAdminController/Details/5
        public ActionResult Details(int id)
        {
            var dalTag = _videoRepo.GetTag(id);
            var vmTag = _mapper.Map<VMTag>(dalTag);
            return View(vmTag);
        }

        // GET: TagAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TagAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMTag tag)
        {
            try
            {
                _videoRepo.CreateTag(tag.Name);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TagAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            var dalTag = _videoRepo.GetTag(id);
            var vmTag = _mapper.Map<VMTag>(dalTag);
            return View(vmTag);
        }

        // POST: TagAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VMTag tag)
        {
            try
            {
                _videoRepo.UpdateTag(id,tag.Name);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TagAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            var dalTag = _videoRepo.GetTag(id);
            var vmTag = _mapper.Map<VMTag>(dalTag);
            return View(vmTag);
        }

        // POST: TagAdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, VMTag tag)
        {
            try
            {
                _videoRepo.DeleteTag(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
