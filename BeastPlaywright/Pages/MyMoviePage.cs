using Microsoft.Playwright;

namespace BeastPlaywright.Pages
{
    public class MyMoviePage : Page
    {
        public MyMoviePage(IPage page) : base(page)
        {
        }
        public async Task<bool> IsMovieAvailable(string movie)
        {
            bool result = await _page.Locator($"xpath=//p[contains(text(),'{movie}')]//..//p[contains(text(),'Available until:')]").IsVisibleAsync();
            return result;
        }
    }
}
