using Beast.Domain.Entities;
using Beast.Testing.Builders.EntityBuilders;
using Beast.Testing.Helpers;
using BeastPlaywright.Pages;
using FluentAssertions;
using Microsoft.Playwright.MSTest;

namespace BeastPlaywright.Tests
{
    [TestClass]
    public class AddCredirts: PageTest
    {
        private readonly User _user;
        private MainPage? _mainPage;
        public AddCredirts(): base()
        {
            _user = new UserBuilder().Build();
        }
        [TestInitialize]
        public async Task Init()
        {
            _mainPage = new MainPage(Page);
            await _mainPage.GotoAsync(Settings.Url);

            RegisterPage registerPage = await _mainPage.Register();
            await registerPage.EnterFirstName(_user.FirtName);
            await registerPage.EnterLastName(_user.LastName);
            await registerPage.EnterEmail(_user.EMailAddress);
            await registerPage.EnterPassword(_user.Password);
            await registerPage.JoinUs();
            //Login
            LoginPage login = await _mainPage.SignIn();
            await login.EnterEMail(_user.EMailAddress);
            await login.EnterPassword(_user.Password);
            _mainPage = await login.Login();
            bool result = await _mainPage.IsLoggedIn();
            result.Should().BeTrue();
        }
        [TestMethod]
        public async Task IWantToAddCredirts()
        {
            //Add Funds
            ProfilePage profile = await _mainPage.Profile();
            await profile.AddCredits(100);
            bool result =  await profile.AreCreditsAvailable();
            result.Should().BeTrue();
        }
    }
}
