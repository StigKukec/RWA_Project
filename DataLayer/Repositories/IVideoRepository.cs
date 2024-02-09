using DataLayer.BLModels;


namespace DataLayer.Repositories
{
    public interface IVideoRepository
    {
        IEnumerable<BLVideo> GetAllVideos();
        BLVideo GetVideo(int id);
        BLVideo CreateVideo(string name, string description, string urlImage, int totalTime, string streamingUrl);
        BLVideo UpdateVideo(int id, string name, string description, string urlImage, int totalTime, string streamingUrl);
        public void DeleteVideo(int videoId);
        IEnumerable<BLGenre> GetAllGenres();
        IEnumerable<BLTag> GetAllTags();
        public void AddGenresToVideo(int videoId, int tagId);
        public void AddTagsToVideo(int videoId, int tagId);
        public BLGenre GetGenre(int IdGenre);
        public void CreateGenre(string name, string description);
        public void UpdateGenre(int id, string name, string description);
        public void DeleteGenre(int genreId);
        public BLTag GetTag(int IdTag);
        public void CreateTag(string name);
        public void UpdateTag(int id, string name);
        public void DeleteTag(int tagId);
        IEnumerable<BLVideo> GetPartialData(int page, int size, string orderBy, string direction);
        IEnumerable<BLVideo> GetSearchedData(int page, int size, string orderBy, string direction, string search);
        IEnumerable<BLGenre> GetPartialGenres(int page, int size);
    }
}
