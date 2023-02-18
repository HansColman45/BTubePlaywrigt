using Microsoft.Playwright;

namespace BeastPlaywright.Pages
{
    public class MainPage : Page
    {
        private readonly ILocator _searchTermInput;
        private readonly ILocator _signInBtn,_registerBtn,_LogOutBtn, _myMoviesBtn, _profileBtn;
        public MainPage(IPage page) : base(page)
        {
            _searchTermInput = _page.GetByPlaceholder("Search..");
            _signInBtn = _page.Locator("//button[@id='SignInButton']");
            _registerBtn = _page.Locator("//button[@id='RegisterButton']");
            _LogOutBtn = _page.GetByRole(AriaRole.Button, new() { Name = "Sign out" });
            _myMoviesBtn = _page.GetByRole(AriaRole.Button, new() { Name = "My movies" });
            _profileBtn = _page.GetByRole(AriaRole.Button, new() { Name = "Profile" });
        }
        public async Task<LoginPage> SignIn()
        {
            await _signInBtn.ClickAsync();
            return new LoginPage(_page);
        }
        public async Task<ProfilePage> Profile()
        {
            await _profileBtn.ClickAsync();
            await _page.GetByRole(AriaRole.Button, new() { Name = "add credits" }).IsEnabledAsync();
            return new ProfilePage(_page);
        }
        public async Task<RegisterPage> Register()
        {
            await _registerBtn.ClickAsync();
            await _page.Locator("xpath=//input[@id='RegisterFirstName']").IsVisibleAsync();
            await _page.Locator("xpath=//input[@id='RegisterLastName']").IsVisibleAsync();
            await _page.Locator("xpath=//input[@id='RegisterEmail']").IsVisibleAsync();
            await _page.Locator("xpath=//input[@id='RegisterPassword']").IsVisibleAsync();
            await _page.Locator("xpath=//input[@id='RegisterRePassword']").IsVisibleAsync();
            await _page.Locator("xpath=//button[@id='RegisterButtonComplete']").IsVisibleAsync();
            return new RegisterPage(_page);
        }
        public async Task<MovieInfoPage> Search(string searchTerm)
        {
            await _searchTermInput.FillAsync(searchTerm);
            await _page.Keyboard.PressAsync("ArrowDown");
            await _page.Keyboard.PressAsync("Enter");
            await _page.Locator("xpath=//Button[@id='RentMovieButton']").IsEnabledAsync();
            return new MovieInfoPage(_page);
        }
        public async Task<MyMoviePage> MyMovies()
        {
            await _myMoviesBtn.ClickAsync();
            await _page.GetByRole(AriaRole.Button, new() { Name = "Watch now" }).IsEnabledAsync();
            return new MyMoviePage(_page);
        }
        public async Task<bool> IsLoggedIn()
        {
            bool result  = await _myMoviesBtn.IsVisibleAsync();
            return result;
        }
        public async Task LogOut()
        {
            await _LogOutBtn.ClickAsync();
        }
    }
}
