using IntegracijskiModul.BLModels;
using IntegracijskiModul.Modeli;

namespace IntegracijskiModul.Mapper
{
    public static class ImageMapper
    {
        // Iz modela u BL model
        public static IEnumerable<BLImage> FromModelToBLModel(IEnumerable<Image> mImage)
            => (IEnumerable<BLImage>)mImage.Select(i => FromModelToBLModel(i));

        public static BLImage FromModelToBLModel(Image mImage)
        {
            return new BLImage
            {
                Id = mImage.Id,
                Content = mImage.Content
            };
        }

        // obrnuto
        public static IEnumerable<Image> FromBLModelToModel(IEnumerable<BLImage> blImage)
            => (IEnumerable<Image>)blImage.Select(i => FromBLModelToModel(i));

        public static Image FromBLModelToModel(BLImage blImage)
        {
            return new Image
            {
                Id = blImage.Id,
                Content = blImage.Content
            };
        }
    }
}
