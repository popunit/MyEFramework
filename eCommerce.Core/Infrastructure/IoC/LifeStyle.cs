
namespace eCommerce.Core.Infrastructure.IoC
{
    public enum LifeStyle
    {
        Singleton = 0, // notice the result in web farm
        Transient = 1,
        Scope = 2
    }
}
