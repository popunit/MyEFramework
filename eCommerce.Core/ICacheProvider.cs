
namespace eCommerce.Core
{
    /// <summary>
    /// Work with config to return proper cache manager
    /// </summary>
    public interface ICacheProvider
    {
        ICacheManager Cache { get; }
    }
}
