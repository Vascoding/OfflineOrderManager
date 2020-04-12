using AutoMapper;
using OfflineOrderManager.Services.Contracts;
using OfflineOrderManager.Utils.AutoMapper;

namespace OfflineOrderManager.Services.Implementations
{
    public class MappingService : IMappingService
    {
        private Mapper mapper;
        
        public MappingService()
        {
            var configurations = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            this.mapper = new Mapper(configurations);
        }

        public TDestination Map<TDestination>(object source)
        {
            return this.mapper.Map<TDestination>(source);
        }
    }
}