using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using eCommerce.Core.Configuration;

namespace eCommerce.Core.Infrastructure
{
    public class WebsiteRoute : AppDomainRoute
    {
        private bool ensureBinFolderAssembliesLoaded = true;
        private bool binFolderAssembliesLoaded = false;

        public WebsiteRoute(Config config)
        {
            this.ensureBinFolderAssembliesLoaded = config.Automation.Enabled;
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

            if (this.ensureBinFolderAssembliesLoaded && !this.binFolderAssembliesLoaded)
            {
                binFolderAssembliesLoaded = true;
                string binPath = GetBinDirectory();
                this.UploadAssembliesToAppDomain(new string[] { binPath });
            }
        }
    }
}
