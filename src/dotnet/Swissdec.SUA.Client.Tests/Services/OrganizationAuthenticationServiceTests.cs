using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swissdec.SUA.Client.Services.Tests
{
    public class OrganizationAuthenticationServiceTests
    {
        const string organizationAuthenticationUri = "https://tst.itserve.ch/swissdec/refapps/next/receiver/services/OrganizationAuthenticationService20230301";

        [Fact]
        public async Task PingAsyncTest()
        {
            var pingResponse = await OrganizationAuthenticationService.PingAsync(organizationAuthenticationUri);

            Assert.Equal(1m, pingResponse.UserAgent.StandardVersion);
        }
    }
}