using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Configuration
{
    /// <summary>
    /// Provider for all the configuration setting class (ISettings)
    /// </summary>
    /// <typeparam name="TSettings"></typeparam>
    /// <remarks>Should use this interface to initialize, config, register all the ISetting-inherited classes</remarks>
    public interface IConfigurationProvider<TSettings> where TSettings : ISettings, new()
    {
        TSettings Settings { get; }
        void SaveSettings(TSettings settings);
    }
}
