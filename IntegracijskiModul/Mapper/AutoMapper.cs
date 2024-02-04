using AutoMapper;

namespace IntegracijskiModul.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<BLModels.BLCountry, Modeli.Country>();
            CreateMap<BLModels.BLGenre, Modeli.Genre>();
            CreateMap<BLModels.BLImage, Modeli.Image>();
            CreateMap<BLModels.BLNotification, Models.Notification>();
            CreateMap<BLModels.BLTag, Modeli.Tag>();
            CreateMap<BLModels.BLUser, Modeli.User>();
            CreateMap<BLModels.BLVideo, Modeli.Video>();
            CreateMap<BLModels.BLVideoTag, Modeli.VideoTag>();

            CreateMap<Modeli.Country, BLModels.BLCountry>();
            CreateMap<Modeli.Genre, BLModels.BLGenre>();
            CreateMap<Modeli.Image, BLModels.BLImage>();
            CreateMap<Models.Notification, BLModels.BLNotification>();
            CreateMap<Modeli.Tag, BLModels.BLTag>();
            CreateMap<Modeli.User, BLModels.BLUser>();
            CreateMap<Modeli.Video, BLModels.BLVideo>();
            CreateMap<Modeli.VideoTag, BLModels.BLVideoTag>();
        }
    }
}
