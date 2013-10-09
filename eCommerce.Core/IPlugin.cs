
namespace eCommerce.Core
{
    public interface IPlugin
    {
        IPluginDescriptor Descriptor { get; set; }

        bool Install();

        bool Uninstall();
    }
}
