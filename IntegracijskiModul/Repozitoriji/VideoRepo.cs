using IntegracijskiModul.BLModels;
using IntegracijskiModul.Mapper;
using IntegracijskiModul.Modeli;
using IntegracijskiModul.Models;

namespace IntegracijskiModul.Repozitoriji
{
    public class VideoRepo : IVideoRepo
    {
        private readonly DatabaseContext _dbc;

        public VideoRepo(DatabaseContext dbc)
        {
            _dbc = dbc;
        }

        public BLVideo AddVideo(string name, string description, int genreId, int imageId, int totalTime, string url, List<Tag> tags)
        {
            var dbVideo = new Video
            {
                Name = name,
                Description = description,
                Created = DateTime.UtcNow,
                GenreId = genreId,
                ImageId = imageId,
                TotalTime = totalTime,
                StreamingUrl = url
            };

            _dbc.Videos.Add(dbVideo);

            _dbc.SaveChanges();

            var blVideo = VideoMapper.FromModelToBLModel((IEnumerable<Video>)dbVideo);

            return (BLVideo)blVideo;
        }

        public void DeleteVideo(int idVideo)
        {
            var t = _dbc.Videos.FirstOrDefault(x => x.Id == idVideo);
            if (t != null)
            {
                _dbc.Remove(t);
                _dbc.SaveChanges();
            }
        }

        public BLVideo EditVideo(BLVideo v)
        {
            var t = _dbc.Videos.FirstOrDefault(x => x.Id == v.Id);

            t.Name = v.Name;
            t.Description = v.Description;
            t.GenreId = v.GenreId;
            t.ImageId = v.ImageId;
            t.TotalTime = v.TotalTime;
            t.StreamingUrl = v.StreamingUrl;

            var blv = VideoMapper.FromModelToBLModel((IEnumerable<Video>)t);

            _dbc.SaveChanges();

            return (BLVideo)blv;
        }

        public IEnumerable<BLVideo> GetAll()
        {
            var dbv = _dbc.Videos;

            var blv = VideoMapper.FromModelToBLModel(dbv);

            return blv;
        }

        public BLVideo GetVideo(int idVideo)
        {
            var dbv = _dbc.Videos.FirstOrDefault(v => v.Id == idVideo);

            if (dbv == null)
            {
                throw new InvalidOperationException("Video nije pronadjen!");
            }

            var blv = VideoMapper.FromModelToBLModel((IEnumerable<Video>)dbv);

            return (BLVideo)blv;
        }
    }
}
