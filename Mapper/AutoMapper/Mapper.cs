using AutoMapper.Internal;
using AutoMapper;


namespace Mapper.AutoMapper
{
    public class Mapper : Application.Abstracts.AutoMapper.IMapper
    {
        public static List<TypePair> typePairs = new();
        private IMapper mapperContainer;
        public TDestination Map<TDestination, TSource>(TSource source, string? ignore = null)
        {
            Config<TDestination, TSource>(5, ignore);

            return mapperContainer.Map<TSource, TDestination>(source);
        }

        public IList<TDestination> Map<TDestination, TSource>(IList<TSource> source, string? ignore = null)
        {
            Config<TDestination, TSource>(5, ignore);

            return mapperContainer.Map<IList<TSource>, IList<TDestination>>(source);
        }

        public TDestination Map<TDestination>(object source, string? ignore = null)
        {
            Config<TDestination, object>(5, ignore);

            return mapperContainer.Map<TDestination>(source);
        }

        public IList<TDestination> Map<TDestination>(IList<object> source, string? ignore = null)
        {
            Config<TDestination, IList<object>>(5, ignore);

            return mapperContainer.Map<IList<TDestination>>(source);
        }

        protected void Config<TDestination, TSource>(int depth = 5, string? ignore = null)
        {
            var typePair = new TypePair(typeof(TDestination), typeof(TSource));
            if (typePairs.Any(t => t.DestinationType == typePair.DestinationType && t.SourceType == typePair.SourceType) && ignore is not null) ;

          typePairs.Add(typePair);

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var pair in typePairs) 
                {
                    if(ignore is not null)
                        cfg.CreateMap(pair.SourceType, pair.DestinationType).MaxDepth(depth).ForMember(ignore, x=>x.Ignore()).ReverseMap();
                    else
                        cfg.CreateMap(pair.SourceType, pair.DestinationType).MaxDepth(depth).ReverseMap();
                }
            });
            mapperContainer = config.CreateMapper();
        }
    }
}
