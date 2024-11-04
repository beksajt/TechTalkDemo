namespace Playwright.Pages;

internal abstract class BasePage
{
    public abstract string URL { get; }
    public IPage Page { get; }

    public BasePage(IPage page)
    {
        Page = page;
        Page.RouteAsync(new Regex("https://googleads"), route => route.AbortAsync());
    }

    protected static string BaseURL => "https://practice.expandtesting.com/";
    public ILocatorAssertions Expect(ILocator locator) => Assertions.Expect(locator);
    public IPageAssertions Expect(IPage page) => Assertions.Expect(page);

    public async Task OpenAsync()
    {
        await Page.GotoAsync(URL);
    }
}