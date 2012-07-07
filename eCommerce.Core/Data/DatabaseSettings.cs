using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Configuration;

namespace eCommerce.Core.Data
{
    /// <summary>
    /// Database settings
    /// </summary>
    public class DatabaseSettings : INotifyPropertyChanged, ISettings
    {
        private string dataProvider;
        private string dataConnectionString;

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
                OnPropertyChanged("DataProvider");
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
                OnPropertyChanged("DataConnectionString");
            }
        }

        public DatabaseSettings()
        {
            UnknownItems = new Dictionary<string, string>();
        }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(dataProvider) && !String.IsNullOrEmpty(dataConnectionString);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
