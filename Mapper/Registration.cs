using Application.Abstracts.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Mapper
{
    public static class Registration
    {
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, AutoMapper.Mapper>();
        }
    }
}
