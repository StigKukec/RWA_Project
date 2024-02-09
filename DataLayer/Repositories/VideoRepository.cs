using AutoMapper;
using DataLayer.BLModels;
using DataLayer.DALModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DataLayer.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly RwaDatabaseContext _dbContext;
        private readonly IMapper _mapper;
        #region constants
        private const string descending = "descending";
        #endregion
        public VideoRepository(RwaDatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IEnumerable<BLVideo> GetAllVideos()
        {
            var dbVideos = _dbContext.Videos;

            var blVideos = _mapper.Map<IEnumerable<BLVideo>>(dbVideos);

            return blVideos;
        }
        public IEnumerable<BLVideo> PageVideoFilter(int page, int size, string orderBy, string direction)
        {
            IEnumerable<Video> dbVideos = _dbContext.Videos
                .Include(x => x.VideoGenres)
                    .ThenInclude(vg => vg.Genre);
            if (string.Compare(orderBy, nameof(Video.Name), true).Equals(0))
            {
                dbVideos = dbVideos.OrderBy(x => x.Name);
            }
            else if (string.Compare(orderBy, nameof(Genre), true).Equals(0))
            {
                var dbGenre = _dbContext.Genres.First(x => x.Name.Equals(direction));
                dbVideos = dbVideos.Where(x => x.VideoGenres.Any(x => x.GenreId.Equals(dbGenre.Idgenre)));
            }
            else
            {
                dbVideos = dbVideos.OrderBy(x => x.Idvideo);
            }

            if (string.Compare(direction, descending, true).Equals(0))
            {
                dbVideos = dbVideos.Reverse();
            }
            var blVideos = _mapper.Map<IEnumerable<BLVideo>>(dbVideos);

            return blVideos;
        }
        public IEnumerable<BLVideo> GetPartialData(int page, int size, string orderBy, string direction)
        {
            var blVideos = PageVideoFilter(page, size, orderBy, direction);

            blVideos = blVideos.Skip(page * size).Take(size);

            return blVideos;
        }
        public IEnumerable<BLVideo> GetSearchedData(int page, int size, string orderBy, string direction, string search)
        {
            var blVideos = PageVideoFilter(page, size, orderBy, direction);

            if (search.IsNullOrEmpty())
            {
                blVideos = GetAllVideos();
                return blVideos;
            }
            blVideos = blVideos.Where(x => x.Name.Contains(search, StringComparison.OrdinalIgnoreCase) && x.Name.StartsWith(search, StringComparison.OrdinalIgnoreCase));
            blVideos = blVideos.Skip(page * size).Take(size);
            return blVideos;
        }

        public BLVideo GetVideo(int id)
        {
            var dalVideo = _dbContext.Videos
                .Include(x => x.VideoGenres)
                    .ThenInclude(vg => vg.Genre)
                .Include(x => x.VideoTags)
                    .ThenInclude(vt => vt.Tag)
                .SingleOrDefault(x => x.Idvideo.Equals(id));

            BLVideo bLVideo = _mapper.Map<BLVideo>(dalVideo);
            return bLVideo;
        }
        public BLVideo CreateVideo(string name, string description, string urlImage, int totalTime, string streamingUrl)
        {
            var dalVideo = new Video
            {
                Name = name,
                Description = description,
                UrlImage = urlImage,
                TotalTime = totalTime,
                StreamingUrl = streamingUrl
            };
            _dbContext.Videos.Add(dalVideo);
            _dbContext.SaveChanges();

            BLVideo bLVideo = _mapper.Map<BLVideo>(dalVideo);

            return bLVideo;
        }
        public BLVideo UpdateVideo(int id, string name, string description, string urlImage, int totalTime, string streamingUrl)
        {
            var video = new Video
            {
                Idvideo = id,
                Name = name,
                Description = description,
                UrlImage = urlImage,
                TotalTime = totalTime,
                StreamingUrl = streamingUrl
            };

            _dbContext.Videos.Update(video);
            _dbContext.SaveChanges();

            BLVideo bLVideo = _mapper.Map<BLVideo>(video);

            return bLVideo;
        }
        public void AddGenresToVideo(int videoId, int genreId)
        {
            DeleteGenresInVideo(videoId);
            var dalVideoGenre = new VideoGenre
            {
                GenreId = genreId,
                VideoId = videoId
            };
            _dbContext.VideoGenres.Add(dalVideoGenre);
            _dbContext.SaveChanges();

        }
        public void AddTagsToVideo(int videoId, int tagId)
        {
            DeleteTagsInVideo(videoId);
            var dalVideoTag = new VideoTag
            {
                TagId = tagId,
                VideoId = videoId
            };
            _dbContext.VideoTags.Add(dalVideoTag);
            _dbContext.SaveChanges();

        }
        private void DeleteGenresInVideo(int videoId)
        {
            var genres = _dbContext.VideoGenres.Where(x => x.VideoId.Equals(videoId));
            _dbContext.VideoGenres.RemoveRange(genres);
            _dbContext.SaveChanges();
        }
        private void DeleteTagsInVideo(int videoId)
        {
            var tags = _dbContext.VideoTags.Where(x => x.VideoId.Equals(videoId));
            _dbContext.VideoTags.RemoveRange(tags);
            _dbContext.SaveChanges();
        }

        public void DeleteVideo(int videoId)
        {
            var video = new Video
            {
                Idvideo = videoId
            };

            _dbContext.Videos.Remove(video);
            _dbContext.SaveChanges();
        }
        public IEnumerable<BLGenre> GetAllGenres()
        {
            var dbGenres = _dbContext.Genres;

            var blGenres = _mapper.Map<IEnumerable<BLGenre>>(dbGenres);

            return blGenres;
        }
        public BLGenre GetGenre(int IdGenre)
        {
            var dbGenre = _dbContext.Genres.FirstOrDefault(x => x.Idgenre.Equals(IdGenre));

            var blGenre = _mapper.Map<BLGenre>(dbGenre);

            return blGenre;
        }

        public void CreateGenre(string name, string description)
        {
            var dalGenre = new Genre
            {
                Name = name,
                Description = description
            };

            _dbContext.Genres.Add(dalGenre);
            _dbContext.SaveChanges();
        }

        public void UpdateGenre(int id, string name, string description)
        {
            var genre = new Genre
            {
                Idgenre = id,
                Name = name,
                Description = description
            };
            _dbContext.Genres.Update(genre);
            _dbContext.SaveChanges();

        }

        public void DeleteGenre(int genreId)
        {
            var genre = new Genre
            {
                Idgenre = genreId
            };
            _dbContext.Genres.Remove(genre);
            _dbContext.SaveChanges();

        }
        public IEnumerable<BLTag> GetAllTags()
        {
            var dbTags = _dbContext.Tags;

            var blTags = _mapper.Map<IEnumerable<BLTag>>(dbTags);

            return blTags;
        }
        public BLTag GetTag(int idTag)
        {
            var dbTag = _dbContext.Tags.FirstOrDefault(x => x.Idtag.Equals(idTag));

            var blTag = _mapper.Map<BLTag>(dbTag);

            return blTag;
        }

        public void CreateTag(string name)
        {
            var tag = new Tag
            {
                Name = name
            };
            _dbContext.Tags.Add(tag);
            _dbContext.SaveChanges();
        }

        public void UpdateTag(int id, string name)
        {
            var tag = new Tag
            {
                Idtag = id,
                Name = name
            };
            _dbContext.Tags.Update(tag);
            _dbContext.SaveChanges();
        }

        public void DeleteTag(int tagId)
        {
            var tag = new Tag
            {
                Idtag = tagId
            };
            _dbContext.Tags.Remove(tag);
            _dbContext.SaveChanges();
        }

        public IEnumerable<BLGenre> GetPartialGenres(int page, int size)
        {
            IEnumerable<Genre> dbGenres = _dbContext.Genres;
            dbGenres = dbGenres.Skip(page * size).Take(size);
            var blGenres = _mapper.Map<IEnumerable<BLGenre>>(dbGenres);

            return blGenres;
        }
    }
}
