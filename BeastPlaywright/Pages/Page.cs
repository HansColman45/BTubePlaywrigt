using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeastPlaywright.Pages
{       
    public class Page
    {
        protected readonly IPage _page;
        public Page(IPage page)
        {
            _page = page;
        }
        public async Task GotoAsync(string url)
        {
            await _page.GotoAsync(url);
            Thread.Sleep(500);
            await _page.ReloadAsync();
        }
    }
}
