using IntegracijskiModul.BLModels;
using IntegracijskiModul.Modeli;

namespace IntegracijskiModul.Mapper
{
    public static class VideoMapper
    {
        // Iz modela u BL model
        public static IEnumerable<BLVideo> FromModelToBLModel(IEnumerable<Video> mv)
            => (IEnumerable<BLVideo>)mv.Select(v => FromModelToBLModel(v));

        private static object FromModelToBLModel(Video v)
        {
            if (v == null)
            {
                throw new ArgumentNullException("Video nije pronađen!");
            }
            else
            {
                try
                {
                    return new BLVideo // tagovi + image + genre
                    {
                        Id = v.Id,
                        Name = v.Name,
                        Description = v.Description,
                        ImageId = v.ImageId,
                        TotalTime = v.TotalTime,
                        StreamingUrl = v.StreamingUrl,
                        GenreId = v.GenreId,
                        Created = v.Created
                    };
                }
                catch
                {
                    throw new ArgumentNullException("Video nije pronađen!");
                }

            }
        }

        // obrnuto
        public static IEnumerable<Video> FromBLModelToModel(IEnumerable<BLVideo> blv)
            => (IEnumerable<Video>)blv.Select(v => FromBLModelToModel(v));

        private static object FromBLModelToModel(BLVideo v)
        {
            if (v == null)
            {
                throw new ArgumentNullException("Video nije pronađen!");
            }
            else
            {
                return new Video // tagovi + image + genre
                {
                    Id = v.Id,
                    Name = v.Name,
                    Description = v.Description,
                    ImageId = v.ImageId,
                    TotalTime = v.TotalTime,
                    StreamingUrl = v.StreamingUrl,
                    GenreId = v.GenreId,
                    Created = v.Created
                };
            }
        }
    }
}
