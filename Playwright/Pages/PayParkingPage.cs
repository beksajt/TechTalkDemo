using Playwright.Pages.Sections;

namespace Playwright.Pages;

internal class PayParkingPage : BasePage
{
    public PayParkingPage(IPage page) : base(page)
    {
        BookingSummarySection = new BookingSummarySection(page);
    }

    private ILocator CreditCardNumberInput => Page.Locator("#cardNumber");
    private ILocator ExpirationDateInput => Page.Locator("#expirationDate");
    private ILocator SecurityCodeInput => Page.Locator("#securityCode");
    private ILocator CompleteReservationButton => Page.Locator("#completeReservation");

    public BookingSummarySection BookingSummarySection { get; }

    public override string URL => BaseURL + "webpark/payment/";

    public async Task FillCreditCardNumber(string creditCardNumber)
    {
        await CreditCardNumberInput.FillAsync(creditCardNumber);
    }

    public async Task FillExpirationDate(string expirationDate)
    {
        await ExpirationDateInput.FillAsync(expirationDate);
    }

    public async Task FillSecurityCode(string securityCode)
    {
        await SecurityCodeInput.FillAsync(securityCode);
    }

    public async Task<SummaryParkingPage> ClickCompleteReservationButton()
    {
        await CompleteReservationButton.ClickAsync();
        return new SummaryParkingPage(Page);
    }
}
