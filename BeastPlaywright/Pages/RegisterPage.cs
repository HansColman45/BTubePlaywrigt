using Microsoft.Playwright;

namespace BeastPlaywright.Pages
{
    public class RegisterPage : Page
    {
        private readonly ILocator _firstNameInput, _lastNameInput, _emailInput, _passwordInput, _verifyPasswordInput, _joinUsBtn;
        public ILocator FirstName {
            get => _firstNameInput;
        }
        public ILocator LastName
        {
            get => _lastNameInput;
        }
        public ILocator EMailAddress
        {
            get => _emailInput;
        }
        public ILocator Password
        {
            get => _passwordInput;
        }
        public ILocator VerifyPassword
        {
            get => _verifyPasswordInput;
        }
        public RegisterPage(IPage page) : base(page)
        {
            _firstNameInput = _page.Locator("xpath=//input[@id='RegisterFirstName']");
            _lastNameInput = _page.Locator("xpath=//input[@id='RegisterLastName']");
            _emailInput = _page.Locator("xpath=//input[@id='RegisterEmail']");
            _passwordInput = _page.Locator("xpath=//input[@id='RegisterPassword']");
            _verifyPasswordInput = _page.Locator("xpath=//input[@id='RegisterRePassword']");
            _joinUsBtn = _page.Locator("xpath=//button[@id='RegisterButtonComplete']");
        }
        public async Task EnterFirstName(string firstName)
        {
            if(string.IsNullOrEmpty(firstName))
                throw new ArgumentNullException(firstName);
            await _firstNameInput.FillAsync(firstName);
        }
        public async Task EnterLastName(string lastName)
        {
            if(string.IsNullOrEmpty(lastName))
                throw new ArgumentNullException(lastName);
            await _lastNameInput.FillAsync(lastName);
        }
        public async Task EnterEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(email);
            await _emailInput.FillAsync(email);
        }
        public async Task EnterPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(password);
            await _passwordInput.FillAsync(password);
            await _verifyPasswordInput.FillAsync(password);
        }
        public async Task JoinUs()
        {
            await _joinUsBtn.ClickAsync();
        }
    }
}
