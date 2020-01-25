using AutoMapper;

namespace OfflineOrderManager.Utils.AutoMapper.Contracts
{
    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile profile);
    }
}
