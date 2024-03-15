using Swissdec.SUA.Client.Helpers;
using Swissdec.SUA.OrganizationAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Swissdec.SUA.Client.Services
{
    public class OrganizationAuthenticationService(string uri, X509Certificate2? serviceCertificate, X509Certificate2? clientCertificate)
    {
        public OrganizationAuthenticationPortClient CreateOrganizationAuthenticationClient() 
        {
            var basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            basicHttpBinding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.Certificate;


            var endpointAddress = new EndpointAddress(uri);

            var organizationAuthenticationClient = new OrganizationAuthenticationPortClient(basicHttpBinding, endpointAddress);
            organizationAuthenticationClient.ClientCredentials.ServiceCertificate.DefaultCertificate = serviceCertificate;
            organizationAuthenticationClient.ClientCredentials.ClientCertificate.Certificate = clientCertificate;

            var cryptographyBehavior = new CryptographyBehavior(clientCertificate);
            //To be implemented
            //organizationAuthenticationClient.ChannelFactory.Endpoint.EndpointBehaviors.Add(cryptographyBehavior);

            return organizationAuthenticationClient;
        }

        public UserAgentType UserAgent { get; } = new UserAgentType
        {
            Producer = "GovTechHackaton",
            Name = "SUA Client",
            StandardVersion = 1.0m,
            Certificate = "N/A",
            Version = "N/A"
        };

        public Task<PingResponse> PingAsync(string? monitoringId = null)
        {
            var client = CreateOrganizationAuthenticationClient();
            var pingRequest = new PingRequest(UserAgent, DateTime.UtcNow, monitoringId);
            return client.PingAsync(pingRequest);
        }

        public Task<RegisterOrganizationResponse> RegisterOrganizationAsync(OrganizationRegistrationType organizationRegistration, string? monitoringId = null)
        {
            var client = CreateOrganizationAuthenticationClient();

            var requestContext = new RequestContextType
            {
                 UserAgent = UserAgent,
                 MonitoringID = monitoringId
            };

            var registerOrganizationRequest = new RegisterOrganizationRequest
            {
                RequestContext = requestContext,
                OrganizationRegistration = organizationRegistration
            };
            return client.RegisterOrganizationAsync(registerOrganizationRequest);
        }

    }
}
