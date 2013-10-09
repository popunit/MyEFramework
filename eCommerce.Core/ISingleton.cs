
namespace eCommerce.Core
{
    public interface ISingleton : IOrderable
    {
        // reigster object to static memory, container, cache and so on
        void RegisterAsSingleton();
    }

    public interface IDeploy : ISingleton
    {
        void Initialize();
    }
}
