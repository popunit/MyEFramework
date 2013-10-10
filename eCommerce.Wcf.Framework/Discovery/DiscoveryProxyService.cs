using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Threading;
using System.Xml;

namespace eCommerce.Wcf.Framework.Discovery
{
    /// <summary>
    /// Use a single discovery proxy service to send and receive announcements
    /// </summary>
    /// <remarks>http://msdn.microsoft.com/en-us/library/dd456787.aspx</remarks>
    /*
     * We can use AnnouncementService to achieve this too.
     * e.g.
     *      AnnouncementService announcementService = new AnnouncementService();
            announcementService.OnlineAnnouncementReceived += (sender, e) =>
            {
                string contractTypes = string.Empty;
                Console.WriteLine("Receive Service Online Announcement.");
                Console.WriteLine("\tAddress: {0}", e.EndpointDiscoveryMetadata.Address.Uri);
                Console.WriteLine("\tContract: {0}", e.EndpointDiscoveryMetadata.ContractTypeNames[0]);
            };
            announcementService.OfflineAnnouncementReceived += (sender, e) =>
            {
                string contractTypes = string.Empty;
                Console.WriteLine("Receive Service Offline Announcement.");
                Console.WriteLine("\tAddress: {0}", e.EndpointDiscoveryMetadata.Address.Uri);
                Console.WriteLine("\tContract: {0}", e.EndpointDiscoveryMetadata.ContractTypeNames[0]);
            };
            using (ServiceHost host = new ServiceHost(announcementService))
            {
                host.Open();
            }
     */
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DiscoveryProxyService : DiscoveryProxy
    {
        // Repository to store EndpointDiscoveryMetadata. A database or a flat file could also be used instead.
        readonly Dictionary<EndpointAddress, EndpointDiscoveryMetadata> _onlineServices;

        public DiscoveryProxyService()
        {
            this._onlineServices = new Dictionary<EndpointAddress, EndpointDiscoveryMetadata>();
        }

        // OnBeginOnlineAnnouncement method is called when a Hello message is received by the Proxy
        protected override IAsyncResult OnBeginOnlineAnnouncement(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state)
        {
            this.AddOnlineService(endpointDiscoveryMetadata);
            return new OnOnlineAnnouncementAsyncResult(callback, state);
        }

        protected override void OnEndOnlineAnnouncement(IAsyncResult result)
        {
            OnOnlineAnnouncementAsyncResult.End(result);
        }

        // OnBeginOfflineAnnouncement method is called when a Bye message is received by the Proxy
        protected override IAsyncResult OnBeginOfflineAnnouncement(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state)
        {
            this.RemoveOnlineService(endpointDiscoveryMetadata);
            return new OnOfflineAnnouncementAsyncResult(callback, state);
        }

        protected override void OnEndOfflineAnnouncement(IAsyncResult result)
        {
            OnOfflineAnnouncementAsyncResult.End(result);
        }

        // OnBeginFind method is called when a Probe request message is received by the Proxy
        protected override IAsyncResult OnBeginFind(FindRequestContext findRequestContext, AsyncCallback callback, object state)
        {
            this.MatchFromOnlineService(findRequestContext);
            return new OnFindAsyncResult(callback, state);
        }

        protected override void OnEndFind(IAsyncResult result)
        {
            OnFindAsyncResult.End(result);
        }

        // OnBeginFind method is called when a Resolve request message is received by the Proxy
        protected override IAsyncResult OnBeginResolve(ResolveCriteria resolveCriteria, AsyncCallback callback, object state)
        {
            return new OnResolveAsyncResult(this.MatchFromOnlineService(resolveCriteria), callback, state);
        }

        protected override EndpointDiscoveryMetadata OnEndResolve(IAsyncResult result)
        {
            return OnResolveAsyncResult.End(result);
        }

        // The following are helper methods required by the Proxy implementation
        void AddOnlineService(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
        {
            lock (this._onlineServices)
            {
                this._onlineServices[endpointDiscoveryMetadata.Address] = endpointDiscoveryMetadata;
            }

            PrintDiscoveryMetadata(endpointDiscoveryMetadata, "Adding");
        }

        void RemoveOnlineService(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
        {
            if (endpointDiscoveryMetadata != null)
            {
                lock (this._onlineServices)
                {
                    this._onlineServices.Remove(endpointDiscoveryMetadata.Address);
                }

                PrintDiscoveryMetadata(endpointDiscoveryMetadata, "Removing");
            }
        }

        void MatchFromOnlineService(FindRequestContext findRequestContext)
        {
            lock (this._onlineServices)
            {
                foreach (EndpointDiscoveryMetadata endpointDiscoveryMetadata in this._onlineServices.Values)
                {
                    if (findRequestContext.Criteria.IsMatch(endpointDiscoveryMetadata))
                    {
                        findRequestContext.AddMatchingEndpoint(endpointDiscoveryMetadata);
                    }
                }
            }
        }

        EndpointDiscoveryMetadata MatchFromOnlineService(ResolveCriteria criteria)
        {
            EndpointDiscoveryMetadata matchingEndpoint = null;
            lock (this._onlineServices)
            {
                foreach (EndpointDiscoveryMetadata endpointDiscoveryMetadata in this._onlineServices.Values)
                {
                    if (criteria.Address == endpointDiscoveryMetadata.Address)
                    {
                        matchingEndpoint = endpointDiscoveryMetadata;
                    }
                }
            }
            return matchingEndpoint;
        }

        void PrintDiscoveryMetadata(EndpointDiscoveryMetadata endpointDiscoveryMetadata, string verb)
        {
            // TO-DO: to change log function here
            var sw = File.CreateText(string.Format(@"C:\AIP\{0}.txt", Guid.NewGuid().ToString().Replace("-", "_")));
            sw.WriteLine("\n**** " + verb + " service of the following type from cache. ");
            foreach (XmlQualifiedName contractName in endpointDiscoveryMetadata.ContractTypeNames)
            {
                sw.WriteLine("** " + contractName.ToString());
                break;
            }
            sw.WriteLine("**** Operation Completed");
            sw.Close();
        }

        sealed class OnOnlineAnnouncementAsyncResult : AsyncResult
        {
            public OnOnlineAnnouncementAsyncResult(AsyncCallback callback, object state)
                : base(callback, state)
            {
                this.Complete(true);
            }

            public static void End(IAsyncResult result)
            {
                AsyncResult.End<OnOnlineAnnouncementAsyncResult>(result);
            }
        }

        sealed class OnOfflineAnnouncementAsyncResult : AsyncResult
        {
            public OnOfflineAnnouncementAsyncResult(AsyncCallback callback, object state)
                : base(callback, state)
            {
                this.Complete(true);
            }

            public static void End(IAsyncResult result)
            {
                AsyncResult.End<OnOfflineAnnouncementAsyncResult>(result);
            }
        }

        sealed class OnFindAsyncResult : AsyncResult
        {
            public OnFindAsyncResult(AsyncCallback callback, object state)
                : base(callback, state)
            {
                this.Complete(true);
            }

            public static void End(IAsyncResult result)
            {
                AsyncResult.End<OnFindAsyncResult>(result);
            }
        }

        sealed class OnResolveAsyncResult : AsyncResult
        {
            readonly EndpointDiscoveryMetadata _matchingEndpoint;

            public OnResolveAsyncResult(EndpointDiscoveryMetadata matchingEndpoint, AsyncCallback callback, object state)
                : base(callback, state)
            {
                this._matchingEndpoint = matchingEndpoint;
                this.Complete(true);
            }

            public static EndpointDiscoveryMetadata End(IAsyncResult result)
            {
                var thisPtr = AsyncResult.End<OnResolveAsyncResult>(result);
                return thisPtr._matchingEndpoint;
            }
        }
    }

    abstract class AsyncResult : IAsyncResult
    {
        readonly AsyncCallback _callback;
        bool _completedSynchronously;
        bool _endCalled;
        Exception _exception;
        bool _isCompleted;
        volatile ManualResetEvent _manualResetEvent;
        readonly object _state;
        readonly object _thisLock;

        protected AsyncResult(AsyncCallback callback, object state)
        {
            this._callback = callback;
            this._state = state;
            this._thisLock = new object();
        }

        public object AsyncState
        {
            get
            {
                return _state;
            }
        }

        public WaitHandle AsyncWaitHandle
        {
            get
            {
                if (_manualResetEvent != null)
                {
                    return _manualResetEvent;
                }
                lock (ThisLock)
                {
                    if (_manualResetEvent == null)
                    {
                        _manualResetEvent = new ManualResetEvent(_isCompleted);
                    }
                }
                return _manualResetEvent;
            }
        }

        public bool CompletedSynchronously
        {
            get
            {
                return _completedSynchronously;
            }
        }

        public bool IsCompleted
        {
            get
            {
                return _isCompleted;
            }
        }

        object ThisLock
        {
            get
            {
                return this._thisLock;
            }
        }

        protected static TAsyncResult End<TAsyncResult>(IAsyncResult result)
            where TAsyncResult : AsyncResult
        {
            if (result == null)
            {
                throw new ArgumentNullException("result");
            }

            var asyncResult = result as TAsyncResult;

            if (asyncResult == null)
            {
                throw new ArgumentException("Invalid async result.", "result");
            }

            if (asyncResult._endCalled)
            {
                throw new InvalidOperationException("Async object already ended.");
            }

            asyncResult._endCalled = true;

            if (!asyncResult._isCompleted)
            {
                asyncResult.AsyncWaitHandle.WaitOne();
            }

            if (asyncResult._manualResetEvent != null)
            {
                asyncResult._manualResetEvent.Close();
            }

            if (asyncResult._exception != null)
            {
                throw asyncResult._exception;
            }

            return asyncResult;
        }

        protected void Complete(bool completedSynchronously)
        {
            if (_isCompleted)
            {
                throw new InvalidOperationException("This async result is already completed.");
            }

            this._completedSynchronously = completedSynchronously;

            if (completedSynchronously)
            {
                this._isCompleted = true;
            }
            else
            {
                lock (ThisLock)
                {
                    this._isCompleted = true;
                    if (this._manualResetEvent != null)
                    {
                        this._manualResetEvent.Set();
                    }
                }
            }

            if (_callback != null)
            {
                _callback(this);
            }
        }

        protected void Complete(bool completedSynchronously, Exception exception)
        {
            this._exception = exception;
            Complete(completedSynchronously);
        }
    }
}
