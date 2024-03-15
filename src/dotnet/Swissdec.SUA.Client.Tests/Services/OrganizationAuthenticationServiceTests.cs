using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace Swissdec.SUA.Client.Services.Tests
{
    public class OrganizationAuthenticationServiceTests
    {
        static readonly string userProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        static readonly string securityResourcesPath = Path.Join(userProfilePath, @"\GitHub\itserve-ch\sua-govtech-2024\resources\security");

        const string organizationAuthenticationUri = "https://tst.itserve.ch/swissdec/refapps/next/receiver/services/OrganizationAuthenticationService20230301";

        [Fact]
        public async Task PingAsyncTest()
        {
            var serviceCertificatePath = Path.Join(securityResourcesPath, "server.crt");
            var serviceCertificate = new X509Certificate2(serviceCertificatePath);
            var clientCertificatePath = Path.Join(securityResourcesPath, "client.p12");
            var clientCertificate = new X509Certificate2(clientCertificatePath, "testZertifikat");

            var organizationAuthenticationService = new OrganizationAuthenticationService(organizationAuthenticationUri, serviceCertificate, clientCertificate);

            var pingResponse = await organizationAuthenticationService.PingAsync();

            Assert.Equal(1m, pingResponse.UserAgent.StandardVersion);
        }
    }
}