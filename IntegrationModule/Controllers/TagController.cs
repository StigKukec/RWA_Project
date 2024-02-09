using AutoMapper;
using DataLayer.DALModels;
using DataLayer.Repositories;
using IntegrationModule.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntegrationModule.Controllers
{
    [Route("api/tags")] 
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVideoRepository _videoRepository;
        public TagController(IMapper mapper, IVideoRepository videoRepository)
        {
            _mapper = mapper;
            _videoRepository = videoRepository;
        }
        [HttpGet("select")]
        public IEnumerable<MTag> GetAllTags()
        {
            var blTags = _videoRepository.GetAllTags();
            var mTags = _mapper.Map<IEnumerable<MTag>>(blTags);
            return mTags;
        }

        [HttpPost("create")]
        public void CreateTag(MTagCreate tag)
        {
            _videoRepository.CreateTag(tag.Name);
        }

        [HttpPut("update")]
        public void UpdateTag(MTagCreate tag, int id)
        {
            _videoRepository.UpdateTag(id, tag.Name);
        }

        [HttpDelete("delete")]
        public void DeleteTag(int id)
        {
            _videoRepository.DeleteTag(id);
        }
    }
}
