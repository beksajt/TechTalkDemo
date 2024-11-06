using OpenQA.Selenium.Interactions;

namespace Selenium.Pages
{
    internal class PracticeHomePage : BasePage
    {
        public PracticeHomePage(IWebDriver driver) : base(driver)
        {
        }

        public override string URL => BaseURL;
        private IWebElement FormValidation => Driver.FindElement(By.LinkText("Form Validation"));
        private IWebElement WebParking => Driver.FindElement(By.LinkText("Web Parking"));

        public FormValidationPage GoToFormValidationPage()
        {
            Actions actions = new Actions(Driver);
            actions.MoveToElement(FormValidation).Click().Perform();
            //FormValidation.Click();
            return new FormValidationPage(Driver);
        }

        public WebParkingPage GoToWebParkingPage()
        {
            Actions actions = new Actions(Driver);
            actions.MoveToElement(WebParking).Click().Perform();
            //WebParking.Click();
            return new WebParkingPage(Driver);
        }
    }
}
