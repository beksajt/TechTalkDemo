namespace Selenium.Pages
{
    internal class BookParkingPage : BasePage
    {
        public BookParkingPage(IWebDriver driver) : base(driver)
        {
            BookingSummarySection = new BookingSummarySection(driver);
        }

        private IWebElement FirstNameInput => Driver.FindElement(By.Id("firstName"));
        private IWebElement LastNameInput => Driver.FindElement(By.Id("lastName"));
        private IWebElement EmailAddressInput => Driver.FindElement(By.Id("email"));
        private IWebElement PhoneNumberInput => Driver.FindElement(By.Id("phone"));
        private IWebElement LicensePlateNumberInput => Driver.FindElement(By.Id("lpNumber"));
        private IWebElement VehicleSizeDropdown => Driver.FindElement(By.Id("vehicleSize"));
        private IWebElement ContinueButton => Driver.FindElement(By.Id("continue"));

        public BookingSummarySection BookingSummarySection { get; }

        public override string URL => BaseURL + "webpark/booking/";

        public void FillFirstName(string firstName)
        {
            FirstNameInput.Clear(); // Clear any existing text before filling
            FirstNameInput.SendKeys(firstName);
        }

        public void FillLastName(string lastName)
        {
            LastNameInput.Clear(); // Clear any existing text before filling
            LastNameInput.SendKeys(lastName);
        }

        public void FillEmailAddress(string emailAddress)
        {
            EmailAddressInput.Clear(); // Clear any existing text before filling
            EmailAddressInput.SendKeys(emailAddress);
        }

        public void FillPhoneNumber(string phoneNumber)
        {
            PhoneNumberInput.Clear(); // Clear any existing text before filling
            PhoneNumberInput.SendKeys(phoneNumber);
        }

        public void FillLicensePlateNumber(string licensePlateNumber)
        {
            LicensePlateNumberInput.Clear(); // Clear any existing text before filling
            LicensePlateNumberInput.SendKeys(licensePlateNumber);
        }

        public void SetVehicleSize(VehicleSize vehicleSize)
        {
            var selectElement = new SelectElement(VehicleSizeDropdown);
            selectElement.SelectByValue(vehicleSize.ToString());
        }

        public PayParkingPage ClickContinueButton()
        {
            ContinueButton.Click();
            return new PayParkingPage(Driver);
        }
    }
}
