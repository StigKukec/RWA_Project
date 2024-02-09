using AutoMapper;
using DataLayer.Repositories;
using IntegrationModule.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationModule.Controllers
{
    [Authorize]
    [Route("api/videos")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVideoRepository _videoRepository;
        public VideoController(IMapper mapper, IVideoRepository videoRepository)
        {
            _mapper = mapper;
            _videoRepository = videoRepository;
        }

        [HttpGet("selectAll")]
        public IEnumerable<MVideo> GetAllVideos()
        {
            var dalVideos = _videoRepository.GetAllVideos();
            var videos = _mapper.Map<IEnumerable<MVideo>>(dalVideos);

            return videos;
        }

        [HttpGet("[action]")]
        public IEnumerable<MVideo> FilterVideoByName(string name, string orderById)
        {
            var dalVideos = _videoRepository.GetAllVideos();
            var videos = _mapper.Map<IEnumerable<MVideo>>(dalVideos);
            videos = videos.Where(v => v.Name.Contains(name) && v.Name.StartsWith(name,StringComparison.CurrentCultureIgnoreCase));

            if (orderById == "desc")
            {
                var orderByDesc = videos.OrderByDescending(n => n.Idvideo);
                //videos.Sort((a,b) => a.Name.CompareTo(b.Name));
                return orderByDesc;
            }
            else if (orderById == "asc")
            {
                var orderByAsc = videos.OrderBy(n => n.Idvideo);
                //videos.Sort((a,b) => a.Name.CompareTo(b.Name));
                return orderByAsc;
            }
            else
            {
                throw new Exception("Wrong command. Use 'desc' for ordering by descending or 'asc' for ordering by acsending id only.");
            }
        }
        [HttpGet("[action]")]
        public IEnumerable<MVideo> Paging(int page, int capacity)
        {
            var dalVideos = _videoRepository.GetAllVideos();
            var videos = _mapper.Map<IEnumerable<MVideo>>(dalVideos);
            videos = videos.Skip(page * capacity).Take(capacity);
            return videos;
        }
        [HttpGet("{id}")]
        public ActionResult<MVideo> GetVideo(int id)
        {
            try
            {
                var dalVideo = _videoRepository.GetVideo(id);
                var video = _mapper.Map<MVideo>(dalVideo);
                if (video == null)
                {
                    return NotFound();
                }
                return video;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public ActionResult CreateVideo(MVideoCreate video)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _videoRepository.CreateVideo(video.Name,video.Description,video.Image,video.TotalTime,video.StreamingUrl);

            return Ok(video);
        }

        [HttpPut("[action]")]
        public ActionResult UpdateVideo([FromBody] MVideoCreate video, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _videoRepository.UpdateVideo(id,video.Name, video.Description, video.Image, video.TotalTime, video.StreamingUrl);

            return Ok(video);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteVideo(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _videoRepository.DeleteVideo(id);

            return Ok();
        }
        /*
        [HttpGet("[action]")]
        public ActionResult<Video> SessionData(string? data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                HttpContext.Session.SetString("sessionData", data);

                return Ok($"Session data set to {data}.");
            }
            else
            {
                var storedData = HttpContext.Session.GetString("sessionData");

                return Ok($"Current session data is {storedData}.");
            }
        }
        */
    }
}
