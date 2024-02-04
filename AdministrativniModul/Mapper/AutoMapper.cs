using AdministrativniModul.ViewModeli;
using AutoMapper;

namespace IntegracijskiModul.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<BLModels.BLCountry, VMCountry>();
            CreateMap<BLModels.BLGenre, VMGenre>();
            CreateMap<BLModels.BLImage, VMImage>();
            CreateMap<BLModels.BLNotification, VMNotification>();
            CreateMap<BLModels.BLTag, VMTag>();
            CreateMap<BLModels.BLUser, VMUser>();
            CreateMap<BLModels.BLVideo, VMVideo>();
            CreateMap<BLModels.BLVideoTag, VMVideoTag>();

            CreateMap<VMCountry, BLModels.BLCountry>();
            CreateMap<VMGenre, BLModels.BLGenre>();
            CreateMap<VMImage, BLModels.BLImage>();
            CreateMap<VMNotification, BLModels.BLNotification>();
            CreateMap<VMTag, BLModels.BLTag>();
            CreateMap<VMUser, BLModels.BLUser>();
            CreateMap<VMVideo, BLModels.BLVideo>();
            CreateMap<VMVideoTag, BLModels.BLVideoTag>();
        }
    }
}
