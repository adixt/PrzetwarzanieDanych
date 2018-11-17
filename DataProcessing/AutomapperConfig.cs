using AutoMapper;

namespace DataProcessing
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<InputClass, OutputClass>()
                    .ForMember(dest => dest.Year_of_Release, x => x.ResolveUsing(src =>
                    {
                        int.TryParse(src.Year_of_Release, out int yearOfRelease);
                        return yearOfRelease;
                    })).ForMember(dest => dest.Critic_Score, x => x.ResolveUsing(src =>
                    {
                        int.TryParse(src.Critic_Score, out int critScore);
                        return critScore;
                    })).ForMember(dest => dest.User_Score, x => x.ResolveUsing(src =>
                    {
                        int.TryParse(src.User_Score, out int userScore);
                        return userScore;
                    })).ForMember(dest => dest.Critic_Count, x => x.ResolveUsing(src =>
                    {
                        int.TryParse(src.Critic_Count, out int critCount);
                        return critCount;
                    })).ForMember(dest => dest.User_Count, x => x.ResolveUsing(src =>
                    {
                        int.TryParse(src.User_Count, out int userCount);
                        return userCount;
                    }));
            });
        }
    }
}
