using eCommerce.Core.Configuration;
using System;
using System.Web;
using System.Web.Hosting;

namespace eCommerce.Core.Infrastructure
{
    public class WebsiteSearcher : AppDomainSearcher
    {
        private readonly bool _ensureBinFolderAssembliesLoaded = true;
        private bool _binFolderAssembliesLoaded;

        public WebsiteSearcher(Config config)
        {
            this._ensureBinFolderAssembliesLoaded = config.Automation.Enabled;
        }

        public string GetBinDirectory()
        {
            if (HostingEnvironment.IsHosted)
            {
                //hosted
                return HttpRuntime.BinDirectory;
            }
            else
            {
                //not hosted. For example, run either in unit tests
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public override void Init()
        {
            base.Init();

            if (this._ensureBinFolderAssembliesLoaded && !this._binFolderAssembliesLoaded)
            {
                _binFolderAssembliesLoaded = true;
                string binPath = GetBinDirectory();
                this.UploadAssembliesToAppDomain(new string[] { binPath });
            }
        }
    }
}
