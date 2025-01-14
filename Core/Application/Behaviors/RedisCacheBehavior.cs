using Application.Abstracts.RedisCache;
using MediatR;

namespace Application.Behaviors
{
    public class RedisCacheBehavior<TRequest, Tresponse> : IPipelineBehavior<TRequest, Tresponse>
    {
        private readonly IRedisCacheService _redisCacheService;

        public RedisCacheBehavior(IRedisCacheService redisCacheService)
        {
            _redisCacheService = redisCacheService;
        }

        public async Task<Tresponse> Handle(TRequest request, RequestHandlerDelegate<Tresponse> next, CancellationToken cancellationToken)
        {
            if(request is ICacheableQuery query)
            {
                var cacheKey = query.CacheKey;
                var cacheTime = query.CacheTime;

                var cachedData = await _redisCacheService.GetAsync<Tresponse>(cacheKey);

                if(cachedData is not null)
                    return cachedData;

                var response = await next();

                if(response is not null)
                    await _redisCacheService.SetAsync(cacheKey, response, DateTime.Now.AddMinutes(cacheTime  ));

                return response;
            }
            return await next(); 
        }
    }
}
