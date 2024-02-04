using IntegracijskiModul.BLModels;
using IntegracijskiModul.Mapper;
using IntegracijskiModul.Modeli;
using IntegracijskiModul.Models;

namespace IntegracijskiModul.Repozitoriji
{
    public class ImageRepo : IImageRepo
    {
        private readonly DatabaseContext _dbc;

        public ImageRepo(DatabaseContext dbc)
        {
            _dbc = dbc;
        }

        public BLImage Add(BLImage image)
        {
            var dbn = ImageMapper.FromBLModelToModel((IEnumerable<BLImage>)image);

            _dbc.Images.Add((Image)dbn);

            _dbc.SaveChanges();

            return image;
        }

        public void Delete(int id)
        {
            var t = _dbc.Images.FirstOrDefault(n => n.Id == id);
            if (t != null)
            {
                _dbc.Remove(t);
                _dbc.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Slika nije pronadjena!");
            }
        }

        public IEnumerable<BLImage> GetAll()
        {
            var dbi = _dbc.Images;

            var bli = ImageMapper.FromModelToBLModel(dbi);

            return bli;
        }

        public BLImage GetById(int idImage)
        {
            var dbi = _dbc.Images.FirstOrDefault(i => i.Id == idImage);

            if (dbi == null)
            {
                throw new InvalidOperationException("Slika nije pronadjena!");
            }

            var bli = ImageMapper.FromModelToBLModel(dbi);

            return bli;
        }

        public BLImage Update(int id, BLImage image)
        {
            var dbn = _dbc.Images.FirstOrDefault(n => n.Id == id);

            if (dbn == null)
            {
                throw new InvalidOperationException("Slika nije pronadjena!");
            }

            dbn.Content = image.Content;

            var bln = ImageMapper.FromModelToBLModel((IEnumerable<Image>)dbn);

            return (BLImage)bln;
        }
    }
}
