namespace Selenium.Pages.Sections
{
    public class BookingSummarySection
    {
        private IWebDriver _driver;

        public BookingSummarySection(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement BookingSummaryList => _driver.FindElement(By.CssSelector("#reservationDetails"));

        private IWebElement SummaryParkingLotText => GetSummaryElement("Parking:");
        private IWebElement SummaryCheckInText => GetSummaryElement("Check In:");
        private IWebElement SummaryCheckOutText => GetSummaryElement("Check Out:");
        private IWebElement SummaryTotalEURText => GetSummaryElement("Total (EUR):");

        private IWebElement GetSummaryElement(string labelText)
        {
            return BookingSummaryList.FindElement(By.XPath($"//li[contains(., '{labelText}')]"));
        }

        public ParkingLot ParkingLot()
        {
            var textContent = SummaryParkingLotText.Text;
            return Enum.Parse<ParkingLot>(textContent.Split(":")[1].Trim());
        }

        public DateTime CheckInTime()
        {
            var textContent = SummaryCheckInText.Text;
            return DateTime.ParseExact(textContent.Split("Check In:")[1].Trim(), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
        }

        public DateTime CheckOutTime()
        {
            var textContent = SummaryCheckOutText.Text;
            return DateTime.ParseExact(textContent.Split("Check Out:")[1].Trim(), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
        }

        public double TotalEUR()
        {
            var textContent = SummaryTotalEURText.Text;
            return double.Parse(textContent.Split(":")[1].Trim(), NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-IE"));
        }
    }
}
