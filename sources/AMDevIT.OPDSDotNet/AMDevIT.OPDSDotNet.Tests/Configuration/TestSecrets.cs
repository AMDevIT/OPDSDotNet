namespace AMDevIT.OPDSDotNet.Tests.Configuration
{
    internal class TestSecrets(string? localServer, 
                               string? username, 
                               string? password)
    {
        #region Properties

        public string? LocalServer
        {
            get;
            set;
        } = localServer;

        public string? Username
        {
            get;
            set;
        } = username;

        public string? Password
        {
            get;
            set;
        } = password;

        #endregion
    }
}
