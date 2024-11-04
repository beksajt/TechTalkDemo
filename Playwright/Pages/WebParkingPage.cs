namespace Playwright.Pages;

internal class WebParkingPage : BasePage
{
    public WebParkingPage(IPage page) : base(page)
    {
    }

    public override string URL => BaseURL + "webpark";
    private ILocator ParkingLotDropdown => Page.Locator("#parkingLot");
    private ILocator EntryDate => Page.Locator("#entryDate");
    private ILocator ExitDate => Page.Locator("#exitDate");
    private ILocator EntryTime => Page.Locator("#entryTime");
    private ILocator ExitTime => Page.Locator("#exitTime");
    private ILocator CalculateCostButton => Page.Locator("#calculateCost");
    private ILocator BookNowButton => Page.Locator("#reserveOnline");
    private ILocator ResultValueText => Page.Locator("#resultValue");
    private ILocator ResultMessageText => Page.Locator("#resultMessage");

    public async Task SelectParkingLot(ParkingLot parkingLot)
    {
        await ParkingLotDropdown.SelectOptionAsync(parkingLot.ToString());
    }

    public async Task SetEntryDateTime(DateTime dateTime)
    {
        await EntryDate.ClickAsync();
        await EntryDate.FillAsync(dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
        await EntryTime.ClickAsync();
        await EntryTime.FillAsync(dateTime.ToString("HH:mm", CultureInfo.InvariantCulture));
    }

    public async Task SetExitDateTime(DateTime dateTime)
    {
        await ExitDate.ClickAsync();
        await ExitDate.FillAsync(dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
        await ExitTime.ClickAsync();
        await ExitTime.FillAsync(dateTime.ToString("HH:mm", CultureInfo.InvariantCulture));
    }

    public async Task ClickCalculateCost()
    {
        await CalculateCostButton.ClickAsync();
    }

    public async Task<BookParkingPage> ClickBookNow()
    {
        await BookNowButton.ClickAsync();
        return new BookParkingPage(Page);
    }

    public async Task<double> ReadPrice()
    {
        var resultText = await ResultValueText.TextContentAsync();

        if (double.TryParse(resultText,
                NumberStyles.Currency,
                CultureInfo.CreateSpecificCulture("en-IE"),
                out var result))
            return result;

        throw new Exception("Unable to parse price");
    }

    public async Task<TimeSpan> ReadDuration()
    {
        var resultText = await ResultMessageText.TextContentAsync();

        if (string.IsNullOrEmpty(resultText))
            throw new Exception("Unable to parse duration");

        var matches = new Regex("\d+").Matches(resultText);
        var days = TimeSpan.FromDays(int.Parse(matches[0].Value));
        var hours = TimeSpan.FromHours(int.Parse(matches[1].Value));
        var minutes = TimeSpan.FromMinutes(int.Parse(matches[2].Value));

        return new TimeSpan().Add(days).Add(hours).Add(minutes);
    }
}