namespace Selenium.Pages
{
    internal class SummaryParkingPage : BasePage
    {
        public SummaryParkingPage(IWebDriver driver) : base(driver)
        {
            BookingSummarySection = new BookingSummarySection(driver);
        }

        private IWebElement Id => Driver.FindElement(By.CssSelector("[data-testid='booking-id']"));

        public BookingSummarySection BookingSummarySection { get; }

        public override string URL => BaseURL + "webpark/confirmation/";

        public string ConfirmationId()
        {
            return Id.Text;
        }
    }
}
