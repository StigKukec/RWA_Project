using AutoMapper;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace RWAMovies.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class NotificationAdminController : Controller
    {
        private readonly ILogger<NotificationAdminController> _logger;
        private readonly IVideoRepository _videoRepo;
        private readonly IMapper _mapper;

        public NotificationAdminController(ILogger<NotificationAdminController> logger, IVideoRepository userRepo, IMapper mapper)
        {
            _logger = logger;
            _videoRepo = userRepo;
            _mapper = mapper;
        }
        // GET: NotificationAdminController
        public ActionResult Index()
        {
            return View();
        }

        // GET: NotificationAdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NotificationAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NotificationAdminController/Create
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

        // GET: NotificationAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NotificationAdminController/Edit/5
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

        // GET: NotificationAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NotificationAdminController/Delete/5
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
