namespace Playwright.Pages.Sections;

public class BookingSummarySection
{
    public BookingSummarySection(IPage page)
    {
        Page = page;
    }

    private ILocator BookingSummaryList => Page.Locator("#reservationDetails").GetByRole(AriaRole.List);
    private ILocator SummaryParkingLotText => BookingSummaryList.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "Parking: " });
    private ILocator SummaryCheckInText => BookingSummaryList.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "Check In: " });
    private ILocator SummaryCheckOutText => BookingSummaryList.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "Check Out: " });
    private ILocator SummaryTotalEURText => BookingSummaryList.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "Total (EUR): " });

    public IPage Page { get; }

    public async Task<ParkingLot> ParkingLot()
    {
        var textContent = await SummaryParkingLotText.TextContentAsync();
        return Enum.Parse<ParkingLot>(textContent.Split(":")[1].Trim());
    }

    public async Task<DateTime> CheckInTime()
    {
        var textContent = await SummaryCheckInText.TextContentAsync();
        return DateTime.ParseExact(textContent.Split("Check In:")[1].Trim(), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
    }

    public async Task<DateTime> CheckOutTime()
    {
        var textContent = await SummaryCheckOutText.TextContentAsync();
        return DateTime.ParseExact(textContent.Split("Check Out:")[1].Trim(), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
    }

    public async Task<double> TotalEUR()
    {
        var textContent = await SummaryTotalEURText.TextContentAsync();
        return double.Parse(textContent.Split(":")[1].Trim(), NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-IE"));
    }
}
