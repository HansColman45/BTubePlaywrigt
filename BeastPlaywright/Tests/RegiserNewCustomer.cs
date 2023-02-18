using Beast.Domain.Entities;
using Beast.Testing.Builders.EntityBuilders;
using Beast.Testing.Helpers;
using BeastPlaywright.Pages;
using FluentAssertions;
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;

namespace BeastPlaywright.Tests
{
    [TestClass]
    public class RegiserNewCustomer: PageTest
    {
        private readonly User _user;
        private MainPage? _mainPage;
        public RegiserNewCustomer(): base() 
        {
            _user = new UserBuilder().Build();
        }
        [TestMethod]
        public async Task IWantToRegisterANewUser()
        {
            _mainPage = new MainPage(Page);
            await _mainPage.GotoAsync(Settings.Url);
            RegisterPage registerPage = await _mainPage.Register();
            await registerPage.FirstName.FillAsync(_user.FirtName);
            await registerPage.LastName.FillAsync(_user.LastName);
            await registerPage.EMailAddress.FillAsync(_user.EMailAddress);
            await registerPage.Password.FillAsync(_user.Password);
            await registerPage.VerifyPassword.FillAsync(_user.Password);
            await registerPage.JoinUs();
        }
        [TestMethod]
        public async Task IWantToRegisterANewCustomerAndLoging()
        {
            _mainPage = new MainPage(Page);
            await _mainPage.GotoAsync(Settings.Url);
            RegisterPage registerPage = await _mainPage.Register();
            await registerPage.FirstName.FillAsync(_user.FirtName);
            await registerPage.LastName.FillAsync(_user.LastName);
            await registerPage.EMailAddress.FillAsync(_user.EMailAddress);
            await registerPage.Password.FillAsync(_user.Password);
            await registerPage.VerifyPassword.FillAsync(_user.Password);
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
        public async Task IWantToRegisterAndLogin()
        {
            await Page.GotoAsync(Settings.Url);
            Thread.Sleep(500);
            await Page.ReloadAsync();
            //First click on Register
            await Page.Locator("//button[@id='RegisterButton']").ClickAsync();
            //Now fill in the users details
            await Page.Locator("xpath=//input[@id='RegisterFirstName']").FillAsync(_user.FirtName);
            await Page.Locator("xpath=//input[@id='RegisterLastName']").FillAsync(_user.LastName);
            await Page.Locator("xpath=//input[@id='RegisterEmail']").FillAsync(_user.EMailAddress);
            await Page.Locator("xpath=//input[@id='RegisterPassword']").FillAsync(_user.Password);
            await Page.Locator("xpath=//input[@id='RegisterRePassword']").FillAsync(_user.Password);
            //Click JoinUs
            await Page.Locator("xpath=//button[@id='RegisterButtonComplete']").ClickAsync();
            //Try to log on with the user
            await Page.Locator("xpath=//input[@id='SignInEmail']").FillAsync(_user.EMailAddress);
            await Page.Locator("xpath=//input[@id='SignInPassword']").FillAsync(_user.Password);
            //Click Login
            await Page.Locator("xpath=//button[@id='SignInButtonComplete']").ClickAsync();
            //Check button
            var myVidieosBtn = Page.GetByRole(AriaRole.Button, new() { Name = "My movies"});
            await Expect(myVidieosBtn).ToBeVisibleAsync();
        }
    }
}
