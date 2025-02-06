using AMDevIT.OPDSDotNet.Client.Core.Feeds.v1_2;
using AMDevIT.Restling.Core;
using AMDevIT.Restling.Core.Network.Builders;
using Microsoft.Extensions.Logging;

namespace AMDevIT.OPDSDotNet.Client.Core
{
    public class OPDSClient
        : IDisposable
    {
        #region Fields

        private readonly HttpClientContext httpClientContext;
        private readonly RestlingClient restlingClient;
        private readonly ILogger? logger;

        private bool disposedValue;

        #endregion

        #region Properties

        protected HttpClientContext HttpClientContext => this.httpClientContext;
        protected RestlingClient NetClient => this.restlingClient;        
        protected ILogger? Logger => this.logger;

        public bool Disposed => this.disposedValue;

        #endregion

        #region .ctor

        public OPDSClient(IHttpClientContextBuilder httpClientContextBuilder, ILogger? logger)
        {
            this.httpClientContext = httpClientContextBuilder.Build();
            this.restlingClient = new(this.httpClientContext, this.logger)
            {
                DisposeContext = true
            };
            this.logger = logger;
        }

        public OPDSClient(ILogger logger)
            : this(new HttpClientContextBuilder(), logger)
        {
        }

        public OPDSClient()
            : this(new HttpClientContextBuilder(), null)
        {
        }

        #endregion

        #region Methods

        public async Task<OPDSFeed?> GetFeedAsync(string url, CancellationToken cancellationToken = default)
        {
            try
            {
                RestRequestResult<OPDSFeed> response = await this.restlingClient.GetAsync<OPDSFeed>(url, cancellationToken);

                if (response.IsSuccessful)
                {
                }

                return response.Data;
            }
            catch (Exception exc)
            {
                this.logger?.LogError(exc, "Error during feed download.");
                return null;
            }
        }     

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.restlingClient.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
