using eCommerce.Core.Common;
using eCommerce.Core.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace eCommerce.Core.Data
{
    /// <summary>
    /// Database settings (TO-DO: Need to support multiple databases)
    /// </summary>
    /// <remarks>Modified INotifyPropertyChanged pattern according to
    /// http://blogs.clariusconsulting.net/kzu/simplified-inotifypropertychanged-implementation-with-weakreference-support-and-typed-property-access-api/
    /// </remarks>
    public class DatabaseSettings : INotifyPropertyChanged, ISettings
    {
        private string _dataProvider;
        private string _dataConnectionString;
        private readonly PropertyChangeManager<DatabaseSettings> _propertyChanges;

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
            get { return this._dataProvider; }
            set
            {
                this._dataProvider = value;
                //OnPropertyChanged("DataProvider");
                this._propertyChanges.NotifyChanged(x => x.DataProvider);
            }
        }

        /// <summary>
        /// Database connection string
        /// </summary>
        public string DataConnectionString 
        {
            get { return this._dataConnectionString; }
            set
            {
                this._dataConnectionString = value;
                this._propertyChanges.NotifyChanged(x => x.DataConnectionString);
            }
        }

        public DatabaseSettings()
        {
            UnknownItems = new Dictionary<string, string>();
            this._propertyChanges = new PropertyChangeManager<DatabaseSettings>(this);
        }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(_dataProvider) && !String.IsNullOrEmpty(_dataConnectionString);
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { this._propertyChanges.AddHandler(value); }
            remove { this._propertyChanges.RemoveHandler(value); }
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
