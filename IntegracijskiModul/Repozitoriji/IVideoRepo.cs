using IntegracijskiModul.BLModels;
using IntegracijskiModul.Modeli;

namespace IntegracijskiModul.Repozitoriji
{
    public interface IVideoRepo
    {
        IEnumerable<BLVideo> GetAll();
        BLVideo AddVideo(string name, string description, int genreId, int imageId, int totalTime, string url, List<Tag> tags);
        BLVideo GetVideo(int idVideo);
        BLVideo EditVideo(BLVideo video);
        void DeleteVideo(int idVideo);
    }
}
