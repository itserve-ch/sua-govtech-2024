using Swissdec.SUA.OrganizationAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Swissdec.SUA.Client.Services
{
    public class OrganizationAuthenticationService
    {
        public static OrganizationAuthenticationPortClient CreateOrganizationAuthenticationClient(string uri) 
        {
            var basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            var endpointAddress = new EndpointAddress(uri);

            return new OrganizationAuthenticationPortClient(basicHttpBinding, endpointAddress);
        }

        public static UserAgentType UserAgent { get; } = new UserAgentType
        {
            Producer = "yxcyxc",
            Name = "yxcyxc",
            StandardVersion = 1.0m,
            Certificate = "yxcyxc",
            Version = "yxcxc"
        };

        public static Task<PingResponse> PingAsync(string uri, string? monitoringId = null)
        {
            var client = CreateOrganizationAuthenticationClient(uri);
            var pingRequest = new PingRequest(UserAgent, DateTime.UtcNow, monitoringId);
            return client.PingAsync(pingRequest);
        }
    }
}
