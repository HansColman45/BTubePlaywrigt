using Beast.Domain.Entities;
using Beast.Testing.Builders.EntityBuilders;
using Beast.Testing.Helpers;
using BeastPlaywright.Pages;
using FluentAssertions;
using Microsoft.Playwright.MSTest;

namespace BeastPlaywright.Tests
{
    [TestClass]
    public class SearchAndOrderMovie: PageTest
    {
        private readonly User _user;
        private MainPage? _mainPage;
        public SearchAndOrderMovie():base()
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
        public async Task IWantToSearchAndOrderAMovie()
        {
            //Add Funds
            ProfilePage profile = await _mainPage.Profile();
            await profile.AddCredits(100);
            //Search for the movie and rent
            MovieInfoPage  infoPage=  await _mainPage.Search("Aladdin");
            await infoPage.RentMovie();
            //Check if movie is available in my movies
            MyMoviePage moviePage = await _mainPage.MyMovies();
            bool result = await moviePage.IsMovieAvailable("Aladdin");
            result.Should().BeTrue();
        }
    }
}
