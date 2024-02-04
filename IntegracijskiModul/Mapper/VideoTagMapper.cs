using IntegracijskiModul.BLModels;
using IntegracijskiModul.Modeli;

namespace IntegracijskiModul.Mapper
{
    public static class VideoTagMapper
    {
        // Iz modela u BL model
        public static IEnumerable<BLVideoTag> FromModelToBLModel(IEnumerable<VideoTag> mvt)
            => (IEnumerable<BLVideoTag>)mvt.Select(v => FromModelToBLModel(v));

        private static object FromModelToBLModel(VideoTag v)
        {
            return new BLVideoTag
            {
                Id = v.Id,
                TagId = v.TagId,
                VideoId = v.VideoId
            };
        }

        // obrnuto
        public static IEnumerable<VideoTag> FromBLModelToModel(IEnumerable<BLVideoTag> blvt)
            => (IEnumerable<VideoTag>)blvt.Select(v => FromBLModelToModel(v));

        private static object FromBLModelToModel(BLVideoTag v)
        {
            return new VideoTag
            {
                Id = v.Id,
                TagId = v.TagId,
                VideoId = v.VideoId
            };
        }
    }
}
