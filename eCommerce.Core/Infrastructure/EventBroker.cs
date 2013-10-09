using eCommerce.Core.Common;
using System;
using System.Web;

namespace eCommerce.Core.Infrastructure
{
    /// <summary>
    /// Broker set HttpApplication event
    /// </summary>
    public class EventBroker
    {
        /// <summary>
        /// Make sure there is only one broker in an appdomain
        /// </summary>
        public static EventBroker Current
        {
            get
            {
                return Singleton<EventBroker>.Instance;
            }
            protected set
            {
                Singleton<EventBroker>.Instance = value;
            }
        }

        static EventBroker()
        {
            Current = new EventBroker();
        }

        public virtual void Add(HttpApplication httpApplication)
        {
            if (null == httpApplication ||  null == httpApplication.Request)
                return;

            // according to HttpApplication process order
            httpApplication.BeginRequest += HttpApplication_BeginRequest;
            httpApplication.AuthorizeRequest += HttpApplication_AuthorizeRequest;

            httpApplication.PostResolveRequestCache += HttpApplication_PostResolveRequestCache;
            httpApplication.PostMapRequestHandler += HttpApplication_PostMapRequestHandler;

            httpApplication.AcquireRequestState += HttpApplication_AcquireRequestState;
            httpApplication.Error += HttpApplication_Error;
            httpApplication.EndRequest += HttpApplication_EndRequest;

            httpApplication.Disposed += HttpApplication_Disposed;
        }

        #region delegate
        /// <summary>
        /// Handler more begin requests for httpapplication
        /// </summary>
        public EventHandler<EventArgs> BeginRequest;

        /// <summary>
        /// Handler more authorize requests for httpapplication
        /// </summary>
        public EventHandler<EventArgs> AuthorizeRequest;

        /// <summary>
        /// Handler more post resolve request cache for httpapplication
        /// </summary>
        public EventHandler<EventArgs> PostResolveRequestCache;

        /// <summary>
        /// Handler more post map request handler for httpapplication
        /// </summary>
        public EventHandler<EventArgs> PostMapRequestHandler;

        /// <summary>
        /// Handler more acquire request state for httpapplication
        /// </summary>
        public EventHandler<EventArgs> AcquireRequestState;

        /// <summary>
        /// Handler more errors for httpapplication
        /// </summary>
        public EventHandler<EventArgs> Error;

        /// <summary>
        /// Handler more end request for httpapplication
        /// </summary>
        public EventHandler<EventArgs> EndRequest;

        /// <summary>
        /// Handler more dispose for httpapplication
        /// </summary>
        public EventHandler<EventArgs> Disposed;

        #endregion

        protected void HttpApplication_Disposed(object sender, EventArgs e)
        {
            if (null != Disposed)
                Disposed(sender, e);
        }

        protected void HttpApplication_EndRequest(object sender, EventArgs e)
        {
            if (null != EndRequest)
                EndRequest(sender, e);
        }

        protected void HttpApplication_Error(object sender, EventArgs e)
        {
            if (null != Error)
                Error(sender, e);
        }

        protected void HttpApplication_AcquireRequestState(object sender, EventArgs e)
        {
            if (null != AcquireRequestState)
                AcquireRequestState(sender, e);
        }

        protected void HttpApplication_PostMapRequestHandler(object sender, EventArgs e)
        {
            if (null != PostMapRequestHandler)
                PostMapRequestHandler(sender, e);
        }

        protected void HttpApplication_PostResolveRequestCache(object sender, EventArgs e)
        {
            if (null != PostResolveRequestCache)
                PostResolveRequestCache(sender, e);
        }

        protected void HttpApplication_AuthorizeRequest(object sender, EventArgs e)
        {
            if (null != AuthorizeRequest)
                AuthorizeRequest(sender, e);
        }

        protected virtual void HttpApplication_BeginRequest(object sender, EventArgs e)
        {
            if (null != BeginRequest)
                BeginRequest(sender, e);
        }
    }
}
