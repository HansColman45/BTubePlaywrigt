using Microsoft.Playwright;

namespace BeastPlaywright.Pages
{
    public class ProfilePage : Page
    {
        private ILocator _addCreditBtn;
        private int _credit;
        public ProfilePage(IPage page) : base(page)
        {
            _addCreditBtn = _page.GetByRole(AriaRole.Button, new() { Name = "add credits" });
        }
        public async Task AddCredits(int credits)
        {
            _credit = credits;
            await _addCreditBtn.ClickAsync();
            await _page.GetByPlaceholder("5").IsEnabledAsync();
            await _page.GetByRole(AriaRole.Button, new() { Name = "buy" }).IsEnabledAsync();
            await _page.GetByPlaceholder("5").FillAsync($"{credits}");
            await _page.GetByRole(AriaRole.Button, new() { Name = "buy" }).ClickAsync();
            await _page.Locator($"xpath=//p[contains(text(),'{_credit}')]").IsVisibleAsync();
        }
        public async Task<bool> AreCreditsAvailable()
        {
            bool result = await _page.Locator($"xpath=//p[contains(text(),'{_credit}')]").IsVisibleAsync();
            return result;
        }
    }
}
