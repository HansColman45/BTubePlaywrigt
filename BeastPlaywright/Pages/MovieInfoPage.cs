using Microsoft.Playwright;

namespace BeastPlaywright.Pages
{
    public class MovieInfoPage : Page
    {
        private ILocator _rentMovieBtn;
        public MovieInfoPage(IPage page) : base(page)
        {
            _rentMovieBtn = _page.Locator("xpath=//Button[@id='RentMovieButton']");
        }
        public async Task RentMovie() 
        {         
            await _rentMovieBtn.ClickAsync();
        }
    }
}