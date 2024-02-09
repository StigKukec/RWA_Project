using AutoMapper;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RWAMovies.ViewModels;

namespace RWAMovies.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class VideoAdminController : Controller
    {
        private readonly ILogger<VideoAdminController> _logger;
        private readonly IVideoRepository _videoRepo;
        private readonly IMapper _mapper;
        #region constants
        private const string videosTableBodyPartial = "_VideosTableBodyPartial";
        private const string videoJsonPartial = "videoJsonPartial";
        private const int page = 0;
        private const int size = 10;
        private const string orderBy = "";
        private const string direction = "";
        #endregion
        public VideoAdminController(ILogger<VideoAdminController> logger, IVideoRepository videoRepo, IMapper mapper)
        {
            _logger = logger;
            _videoRepo = videoRepo;
            _mapper = mapper;
        }
        // GET: VideoAdminController
        public ActionResult Index()
        {
            var blGenres = _videoRepo.GetAllGenres();
            var vmGenres = _mapper.Map<IEnumerable<VMGenre>>(blGenres);

            var blVideoCount = _videoRepo.GetAllVideos();
            var vmVideoCount = _mapper.Map<IEnumerable<VMVideo>>(blVideoCount);
            var blVideos = _videoRepo.GetPartialData(page, size, orderBy, direction);
            var vmVideos = _mapper.Map<IEnumerable<VMVideo>>(blVideos);

            ViewBag.Page = page;
            ViewBag.Size = size;
            ViewBag.OrderBy = orderBy;
            ViewBag.Direction = direction;
            ViewBag.Pages = (vmVideoCount.Count() / size);

            ViewBag.Genres = vmGenres;

            return View(vmVideos);
        }
        public IActionResult VideosTableBodyPartial(int page, int size, string orderBy, string direction)
        {
            if (size.Equals(0))
                size = 10;

            var blVideos = _videoRepo.GetPartialData(page, size, orderBy, direction);
            var vmVideos = _mapper.Map<IEnumerable<VMVideo>>(blVideos);

            ViewBag.Page = page;
            ViewBag.Size = size;
            ViewBag.OrderBy = orderBy;
            ViewBag.Direction = direction;
            ViewBag.Pages = (vmVideos.Count() / size);

            return PartialView(videosTableBodyPartial, vmVideos);
        }
      
        public IActionResult VideoTableBodyPartial(int idVideo)
        {
            var blVideo = _videoRepo.GetVideo(idVideo);
            var vmVideo = _mapper.Map<VMVideo>(blVideo);
            List<VMVideo> vmVideos = new()
            {
                vmVideo
            };
            return PartialView(videosTableBodyPartial, vmVideos);
        }
        public IActionResult AllVideosTableBodyPartial()
        {
            var blVideos = _videoRepo.GetAllVideos();
            var vmVideos = _mapper.Map<IEnumerable<VMVideo>>(blVideos);

            return PartialView(videosTableBodyPartial, vmVideos);
        }
        public IActionResult SearchVideosTabPartial(int page, int size, string orderBy, string direction, string search)
        {
            if (size.Equals(0))
                size = 10;

            var blVideos = _videoRepo.GetSearchedData(page, size, orderBy, direction, search);
            var vmVideos = _mapper.Map<IEnumerable<VMVideo>>(blVideos);

            return PartialView(videosTableBodyPartial, vmVideos);
        }
        // GET: VideoAdminController/Details/5
        public ActionResult Details(int id)
        {
            var blVideo = _videoRepo.GetVideo(id);
            var vmVideo = _mapper.Map<VMVideo>(blVideo);
            return View(vmVideo);
        }

        // GET: VideoAdminController/Create
        public ActionResult Create()
        {
            
            var blGenres = _videoRepo.GetAllGenres();
            var vmGenres = _mapper.Map<IEnumerable<VMGenre>>(blGenres);
            ViewBag.Genres = new SelectList(vmGenres, nameof(VMGenre.Idgenre), nameof(VMGenre.Name));

            var BLTags = _videoRepo.GetAllTags();
            var vmTags = _mapper.Map<IEnumerable<VMTag>>(BLTags);
            ViewBag.Tags = new SelectList(vmTags, nameof(VMTag.Idtag), nameof(VMTag.Name));
            
            return View();
        }

        // POST: VideoAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMVideoCreate video)
        {
            try
            {
                string? genres = Request.Form["hiddenGenres"];
                string? tags = Request.Form["hiddenTags"];
                var currentVideo = _videoRepo.CreateVideo(video.Name, video.Description, video.UrlImage, video.TotalTime, video.StreamingUrl);

                if (!genres.IsNullOrEmpty())
                {
                    string[] detailedGenres = genres!.Split(',');
                    // video.VideoGenres.ToList().ForEach(v => v.VideoId = currentVideo!.Idvideo);
                    for (int i = 0; i < detailedGenres.Length; i++)
                    {
                        _videoRepo.AddGenresToVideo(currentVideo.Idvideo, int.Parse(detailedGenres[i]));
                    }
                }
                if (!tags.IsNullOrEmpty())
                {
                    string[] detailedTags = tags!.Split(',');
                    for (int i = 0; i < detailedTags.Length; i++)
                    {
                        _videoRepo.AddTagsToVideo(currentVideo.Idvideo, int.Parse(detailedTags[i]));
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VideoAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            var blGenres = _videoRepo.GetAllGenres();
            var vmGenres = _mapper.Map<IEnumerable<VMGenre>>(blGenres);
            ViewBag.Genres = new SelectList(vmGenres, nameof(VMGenre.Idgenre), nameof(VMGenre.Name));

            var BLTags = _videoRepo.GetAllTags();
            var vmTags = _mapper.Map<IEnumerable<VMTag>>(BLTags);
            ViewBag.Tags = new SelectList(vmTags, nameof(VMTag.Idtag), nameof(VMTag.Name));

            var blVideo = _videoRepo.GetVideo(id);
            var vmVideo = _mapper.Map<VMVideo>(blVideo);

            return View(vmVideo);
        }

        // POST: VideoAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VMVideo video)
        {
            try
            {
                string? genres = Request.Form["hiddenGenres"];
                string? tags = Request.Form["hiddenTags"];
                //var nes = new List<VMVideoGenre>(video.VideoGenres);
                var currentVideo = _videoRepo.UpdateVideo(id, video.Name, video.Description, video.UrlImage, video.TotalTime, video.StreamingUrl);

                if (!genres.IsNullOrEmpty())
                {
                    string[] detailedGenres = genres!.Split(',');
                    for (int i = 0; i < detailedGenres.Length; i++)
                    {
                        _videoRepo.AddGenresToVideo(currentVideo.Idvideo, int.Parse(detailedGenres[i]));
                    }
                }
                if (!tags.IsNullOrEmpty())
                {
                    string[] detailedTags = tags!.Split(',');
                    for (int i = 0; i < detailedTags.Length; i++)
                    {
                        _videoRepo.AddTagsToVideo(currentVideo.Idvideo, int.Parse(detailedTags[i]));
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VideoAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            var blVideo = _videoRepo.GetVideo(id);
            var vmVideo = _mapper.Map<VMVideo>(blVideo);
            return View(vmVideo);
        }

        // POST: VideoAdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, VMVideo video)
        {
            try
            {
                _videoRepo.DeleteVideo(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
