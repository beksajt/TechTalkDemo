using Playwright.Pages.Sections;

namespace Playwright.Pages;

internal class BookParkingPage : BasePage
{
    public BookParkingPage(IPage page) : base(page)
    {
        BookingSummarySection = new BookingSummarySection(Page);
    }

    private ILocator FirstNameInput => Page.Locator("#firstName");
    private ILocator LastNameInput => Page.Locator("#lastName");
    private ILocator EmailAddressInput => Page.Locator("#email");
    private ILocator PhoneNumberInput => Page.Locator("#phone");
    private ILocator LicensePlateNumberInput => Page.Locator("#lpNumber");
    private ILocator VehicleSizeDropdown => Page.Locator("#vehicleSize");
    private ILocator ContinueButton => Page.Locator("#continue");
    public BookingSummarySection BookingSummarySection { get; }
    public override string URL => BaseURL + "webpark/booking/";

    public async Task FillFirstName(string firstName)
    {
        await FirstNameInput.FillAsync(firstName);
    }

    public async Task FillLastName(string lastName)
    {
        await LastNameInput.FillAsync(lastName);
    }

    public async Task FillEmailAddress(string emailAddress)
    {
        await EmailAddressInput.FillAsync(emailAddress);
    }

    public async Task FillPhoneNumber(string phonenNumber)
    {
        await PhoneNumberInput.FillAsync(phonenNumber);
    }

    public async Task FillLicensePlateNumber(string licensePlateNumber)
    {
        await LicensePlateNumberInput.FillAsync(licensePlateNumber);
    }

    public async Task SetVehicleSize(VehicleSize vehicleSize)
    {
        await VehicleSizeDropdown.SelectOptionAsync(vehicleSize.ToString());
    }

    public async Task<PayParkingPage> ClickContinueButton()
    {
        await ContinueButton.ClickAsync();
        return new PayParkingPage(Page);
    }
}
