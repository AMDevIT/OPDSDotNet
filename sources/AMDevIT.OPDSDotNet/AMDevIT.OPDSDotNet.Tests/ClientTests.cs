using AMDevIT.OPDSDotNet.Client.Core;
using AMDevIT.OPDSDotNet.Client.Core.Feeds.v1_2;
using AMDevIT.OPDSDotNet.Tests.Configuration;
using AMDevIT.OPDSDotNet.Tests.Diagnostics;
using AMDevIT.Restling.Core.Network;
using AMDevIT.Restling.Core.Network.Builders;
using AMDevIT.Restling.Core.Network.Builders.Security.Headers;
using Microsoft.Extensions.Configuration;

namespace AMDevIT.OPDSDotNet.Tests
{
    [TestClass]
    public sealed class ClientTests
    {
        #region Methods

        #region Tests

        [TestMethod]
        [DataRow("localServer", "username", "password")]
        public async Task TestGetFeedWithBasicAuthenticationAsync(string localServerKey, string usernameKey, string passwordKey)
        {
            HttpClientContextBuilder clientContextBuilder = new();
            BasicAuthenticationBuilder basicAuthenticationBuilder;
            AuthenticationHeader authenticationHeader;
            TraceLogger<OPDSClient> traceLogger = new();
            OPDSClient client;
            OPDSFeed? feed;
            TestSecrets testSecrets;

            testSecrets = GetTestSecrets(localServerKey, usernameKey, passwordKey);
            string? localServer = testSecrets.LocalServer;
            string? username = testSecrets.Username;
            string? password = testSecrets.Password;
            string callUri = $"{localServer}/opds/v1.2/catalog";
            
            Assert.IsNotNull(username);
            Assert.IsNotNull(password);
            Assert.IsFalse(string.IsNullOrWhiteSpace(username));

            basicAuthenticationBuilder = new();
            basicAuthenticationBuilder = (BasicAuthenticationBuilder) basicAuthenticationBuilder.SetUser(username);
            basicAuthenticationBuilder = (BasicAuthenticationBuilder) basicAuthenticationBuilder.SetPassword(password);
            authenticationHeader = basicAuthenticationBuilder.Build();

            clientContextBuilder = clientContextBuilder.AddAuthenticationHeader(authenticationHeader);
            client = new (clientContextBuilder, traceLogger);

            feed = await client.GetFeedAsync(callUri);
            Assert.IsNotNull(feed, $"Feed from {callUri} is null.");
        }

        #endregion

        private static TestSecrets GetTestSecrets(string localServerKey, string usernameKey, string passwordKey)
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<ClientTests>()
                                                                  .Build();
            IConfigurationSection authenticationSection = config.GetSection("authentication");
            IConfigurationSection? basicAuthenticationSection = authenticationSection?.GetSection("basic");
            string? username = basicAuthenticationSection?[usernameKey];
            string? password = basicAuthenticationSection?[passwordKey];
            string? localServer = config[localServerKey];

            return new TestSecrets(localServer, username, password);
        }

        #endregion
    }
}
