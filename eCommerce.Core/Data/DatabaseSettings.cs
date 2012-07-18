using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Configuration;
using eCommerce.Core.Common;
using System.Linq.Expressions;

namespace eCommerce.Core.Data
{
    /// <summary>
    /// Database settings
    /// </summary>
    /// <remarks>Modified INotifyPropertyChanged pattern according to
    /// http://blogs.clariusconsulting.net/kzu/simplified-inotifypropertychanged-implementation-with-weakreference-support-and-typed-property-access-api/
    /// </remarks>
    public class DatabaseSettings : INotifyPropertyChanged, ISettings
    {
        private string dataProvider;
        private string dataConnectionString;
        private PropertyChangeManager<DatabaseSettings> propertyChanges;

        /// <summary>
        /// Database setting items
        /// </summary>
        public IDictionary<string, string> UnknownItems
        {
            get;
            private set;
        }

        /// <summary>
        /// Database's provider
        /// </summary>
        public string DataProvider 
        {
            get { return this.dataProvider; }
            set
            {
                this.dataProvider = value;
                //OnPropertyChanged("DataProvider");
                this.propertyChanges.NotifyChanged(x => x.DataProvider);
            }
        }

        /// <summary>
        /// Database connection string
        /// </summary>
        public string DataConnectionString 
        {
            get { return this.dataConnectionString; }
            set
            {
                this.dataConnectionString = value;
                this.propertyChanges.NotifyChanged(x => x.DataConnectionString);
            }
        }

        public DatabaseSettings()
        {
            UnknownItems = new Dictionary<string, string>();
            this.propertyChanges = new PropertyChangeManager<DatabaseSettings>(this);
        }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(dataProvider) && !String.IsNullOrEmpty(dataConnectionString);
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { this.propertyChanges.AddHandler(value); }
            remove { this.propertyChanges.RemoveHandler(value); }
        }


        //protected void OnPropertyChanged(string name)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null)
        //    {
        //        handler(this, new PropertyChangedEventArgs(name));
        //    }
        //}
    }
}
