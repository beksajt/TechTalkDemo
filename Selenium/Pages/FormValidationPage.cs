namespace Selenium.Pages
{
    internal class FormValidationPage : BasePage
    {
        public FormValidationPage(IWebDriver driver) : base(driver)
        {
        }

        public override string URL => BaseURL + "form-validation";

        private IWebElement ContactNameInput => Driver.FindElement(By.XPath("//label[text()='Contact Name']/following-sibling::input"));
        private IWebElement ContactNumberInput => Driver.FindElement(By.XPath("//label[text()='Contact number']/following-sibling::input"));
        private IWebElement PickupDateInput => Driver.FindElement(By.Name("pickupdate"));
        private IWebElement PaymentMethodDropdown => Driver.FindElement(By.XPath("//label[text()='Payment Method']/following-sibling::select"));
        private IWebElement RegisterButton => Driver.FindElement(By.XPath("//button[text()=' Register ']"));

        public void FillContactName(string contactName)
        {
            ContactNameInput.Clear(); // Clear any existing text before filling
            ContactNameInput.SendKeys(contactName);
        }

        public void FillContactNumber(string contactNumber)
        {
            ContactNumberInput.Clear(); // Clear any existing text before filling
            ContactNumberInput.SendKeys(contactNumber);
        }

        public void FillPickupDate(DateTime pickupDate)
        {
            PickupDateInput.Click(); // Click the input field to focus
            PickupDateInput.Clear(); // Clear any existing text before filling
            PickupDateInput.SendKeys(pickupDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
        }

        public void SelectPaymentMethod(PaymentMethod paymentMethod)
        {
            var selectElement = new SelectElement(PaymentMethodDropdown);
            selectElement.SelectByValue(paymentMethod.ToString());
        }

        public void ClickRegisterButton()
        {
            RegisterButton.Click();
        }
    }
}
