namespace Playwright.Pages;

internal class PracticeHomePage : BasePage
{
    public PracticeHomePage(IPage page) : base(page)
    {
    }

    public override string URL => BaseURL;
    private ILocator FormValidation => Page.GetByRole(AriaRole.Link, new() { Name = "Form Validation" });
    private ILocator WebParking => Page.GetByRole(AriaRole.Link, new() { Name = "Web Parking" });

    public async Task<FormValidationPage> GoToFormValidationPage()
    {
        await FormValidation.ClickAsync();
        return new FormValidationPage(Page);
    }

    public async Task<WebParkingPage> GoToWebParkingPage()
    {
        await WebParking.ClickAsync();
        return new WebParkingPage(Page);
    }
}