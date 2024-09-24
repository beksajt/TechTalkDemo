using Playwright.Pages.Sections;

namespace Playwright.Pages;

internal class SummaryParkingPage : BasePage
{
    public SummaryParkingPage(IPage page) : base(page)
    {
        BookingSummarySection = new BookingSummarySection(Page);
    }

    private ILocator Id => Page.GetByTestId("booking-id");

    public BookingSummarySection BookingSummarySection { get; }
    public override string URL => BaseURL + "webpark/confirmation/";

    public async Task<string> ConfirmationId()
    {
        return await Id.InnerTextAsync();
    }
}
