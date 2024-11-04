namespace Selenium.Pages
{
    internal class PayParkingPage : BasePage
    {
        public PayParkingPage(IWebDriver driver) : base(driver)
        {
            BookingSummarySection = new BookingSummarySection(driver);
        }

        private IWebElement CreditCardNumberInput => Driver.FindElement(By.Id("cardNumber"));
        private IWebElement ExpirationDateInput => Driver.FindElement(By.Id("expirationDate"));
        private IWebElement SecurityCodeInput => Driver.FindElement(By.Id("securityCode"));
        private IWebElement CompleteReservationButton => Driver.FindElement(By.Id("completeReservation"));

        public BookingSummarySection BookingSummarySection { get; }

        public override string URL => BaseURL + "webpark/payment/";

        public void FillCreditCardNumber(string creditCardNumber)
        {
            CreditCardNumberInput.Clear(); // Clear any existing text before filling
            CreditCardNumberInput.SendKeys(creditCardNumber);
        }

        public void FillExpirationDate(string expirationDate)
        {
            ExpirationDateInput.Clear(); // Clear any existing text before filling
            ExpirationDateInput.SendKeys(expirationDate);
        }

        public void FillSecurityCode(string securityCode)
        {
            SecurityCodeInput.Clear(); // Clear any existing text before filling
            SecurityCodeInput.SendKeys(securityCode);
        }

        public SummaryParkingPage ClickCompleteReservationButton()
        {
            CompleteReservationButton.Click();
            return new SummaryParkingPage(Driver);
        }
    }
}
