using Microsoft.Playwright;

namespace BeastPlaywright.Pages
{
    public class LoginPage : Page
    {
        private ILocator _eMailInput;
        private ILocator _passwordInput;
        private ILocator _loginBtn;
        public LoginPage(IPage page) : base(page)
        {
            _eMailInput = _page.Locator("xpath=//input[@id='SignInEmail']");
            _passwordInput = _page.Locator("xpath=//input[@id='SignInPassword']");
            _loginBtn = _page.Locator("xpath=//button[@id='SignInButtonComplete']");
        }
        public async Task EnterEMail(string email)
        {
            await _eMailInput.FillAsync(email);
        }
        public async Task EnterPassword(string password)
        {
            await _passwordInput.FillAsync(password);
        }
        public async Task<MainPage> Login()
        {
            await _loginBtn.ClickAsync();
            await _page.GetByRole(AriaRole.Button, new() { Name = "My movies" }).IsVisibleAsync();
            await _page.GetByRole(AriaRole.Button, new() { Name = "Profile" }).IsVisibleAsync();
            await _page.GetByRole(AriaRole.Button, new() { Name = "Sign out" }).IsVisibleAsync();
            Thread.Sleep(700);
            return new MainPage(_page);
        }
    }
}
