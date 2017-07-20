using FluentAssertions;
using Hermes.Infrastructure.Commands.Users;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Hermes.Tests.Integration
{
    public class UserControllerTest : BaseControllerTest
    {
        public UserControllerTest(ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        [Trait("Category", "UserTests")]
        public async Task RegisterNewUser()
        {
            var command = new CreateUser
            {
                Username = "Tester2",
                Password = "Qwer123$",
                Email = "tester2@example.com"
            };

            var response = await _Client.SendAsync(BuildMessage(HttpMethod.Post, command, "user/register"));
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Created);
        }

        [Theory]
        [Trait("Category", "UserTests")]
        [InlineData("Tester", "Qwer123%", false)]
        [InlineData("Tester", "Qwer123$", true)]
        [InlineData("Tester0", "Qwer123$", false)]
        public async Task WhenValidCredentialsUserExist(string username, string password, bool shouldPass)
        {
            if (!shouldPass)
            {
                Exception ex = await Assert.ThrowsAsync<Exception>(async () => await loginUserAsync(username, password));
                Assert.Equal("Invalid credentials.", ex.Message);
                return;
            }

            var jwt = await loginUserAsync(username, password);
            _Output.WriteLine(jwt);
            var message = BuildMessage(HttpMethod.Get, null, "user", jwt);
            var response = await _Client.SendAsync(message);

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UserTests")]
        public async Task ChangeUserPassword()
        {
            var command = new ChangePassword
            {
                OldPassword = "Qwer123$",
                NewPassword = "Qwer1234"
            };

            var jwt = await loginUserAsync("Tester", "Qwer123$");
            var message = BuildMessage(HttpMethod.Put, command, "user/password", jwt);
            var response = await _Client.SendAsync(message);
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);

            command = new ChangePassword
            {
                OldPassword = "Qwer1234",
                NewPassword = "Qwer123$"
            };
            message = BuildMessage(HttpMethod.Put, command, "user/password", jwt);
            var responseBack = await _Client.SendAsync(message);
            responseBack.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        private async Task<string> loginUserAsync(string username, string password)
        {
            var response = await _Client.GetAsync($"user/login?Username={username}&Password={password}");
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody.Trim('"');
        }
    }
}
