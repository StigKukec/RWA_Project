using AutoMapper;
using DataLayer.Repositories;
using IntegrationModule.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationModule.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVideoRepository _videoRepository;
        public GenreController(IMapper mapper, IVideoRepository videoRepository)
        {
            _mapper = mapper;
            _videoRepository = videoRepository;
        }
        [HttpGet("[action]")]
        public IEnumerable<MGenre> GetAllGenres()
        {
            var blgenres = _videoRepository.GetAllGenres();
            var mgenres = _mapper.Map<IEnumerable<MGenre>>(blgenres);
            return mgenres;
        }
        [HttpPost("create")]
        public void CreateGenre(MGenreCreate genre)
        {
            _videoRepository.CreateGenre(genre.Name, genre.Description);
        }
        [HttpPut("update")]
        public void UpdateGenre(MGenreCreate genre, int id)
        {
            _videoRepository.UpdateGenre(id, genre.Name, genre.Description);
        }

        [HttpDelete("delete")]
        public void DeleteGenre(int id)
        {
            _videoRepository.DeleteGenre(id);
        }
    }
}
