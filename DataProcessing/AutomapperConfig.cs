using AutoMapper;

namespace DataProcessing
{
    public static class AutoMapperConfig
    {
        private static int TryParseStringToInt(string parser)
        {
            int.TryParse(parser, out int parsed);
            return parsed;
        }

        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<InputClass, OutputClass>()
                    .ForMember(dest => dest.Year_of_Release, x => x.MapFrom(src => TryParseStringToInt(src.Year_of_Release)))
                    .ForMember(dest => dest.Critic_Score, x => x.MapFrom(src => TryParseStringToInt(src.Critic_Score)))
                    .ForMember(dest => dest.User_Score, x => x.MapFrom(src => TryParseStringToInt(src.User_Score)))
                    .ForMember(dest => dest.Critic_Count, x => x.MapFrom(src => TryParseStringToInt(src.Critic_Count)))
                    .ForMember(dest => dest.User_Count, x => x.MapFrom(src => TryParseStringToInt(src.User_Count)));
            });
        }
    }
}
