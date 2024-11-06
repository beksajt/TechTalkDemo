using System.Text.RegularExpressions;

namespace Selenium.Pages
{
    internal class WebParkingPage : BasePage
    {
        public WebParkingPage(IWebDriver driver) : base(driver)
        {
        }

        public override string URL => BaseURL + "webpark";

        private IWebElement ParkingLotDropdown => Driver.FindElement(By.Id("parkingLot"));
        private IWebElement EntryDate => Driver.FindElement(By.Id("entryDate"));
        private IWebElement ExitDate => Driver.FindElement(By.Id("exitDate"));
        private IWebElement EntryTime => Driver.FindElement(By.Id("entryTime"));
        private IWebElement ExitTime => Driver.FindElement(By.Id("exitTime"));
        private IWebElement CalculateCostButton => Driver.FindElement(By.Id("calculateCost"));
        private IWebElement BookNowButton => Driver.FindElement(By.Id("reserveOnline"));
        private IWebElement ResultValueText => Driver.FindElement(By.Id("resultValue"));
        private IWebElement ResultMessageText => Driver.FindElement(By.Id("resultMessage"));

        public void SelectParkingLot(ParkingLot parkingLot)
        {
            var selectElement = new SelectElement(ParkingLotDropdown);
            selectElement.SelectByValue(parkingLot.ToString());
        }

        public void SetEntryDateTime(DateTime dateTime)
        {
            EntryDate.Clear();
            EntryDate.SendKeys(dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            EntryTime.Clear();
            EntryTime.SendKeys(dateTime.ToString("HH:mm", CultureInfo.InvariantCulture));
        }

        public void SetExitDateTime(DateTime dateTime)
        {
            ExitDate.Clear();
            ExitDate.SendKeys(dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            ExitTime.Clear();
            ExitTime.SendKeys(dateTime.ToString("HH:mm", CultureInfo.InvariantCulture));
        }

        public void ClickCalculateCost()
        {
            CalculateCostButton.Click();
        }

        public BookParkingPage ClickBookNow()
        {
            BookNowButton.Click();
            return new BookParkingPage(Driver);
        }

        public double ReadPrice()
        {
            var resultText = ResultValueText.Text;

            if (double.TryParse(resultText, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-IE"), out var result))
                return result;

            throw new Exception("Unable to parse price");
        }

        public TimeSpan ReadDuration()
        {
            var resultText = ResultMessageText.Text;

            if (string.IsNullOrEmpty(resultText))
                throw new Exception("Unable to parse duration");

            var matches = new Regex("\\d+").Matches(resultText);
            var days = TimeSpan.FromDays(int.Parse(matches[0].Value));
            var hours = TimeSpan.FromHours(int.Parse(matches[1].Value));
            var minutes = TimeSpan.FromMinutes(int.Parse(matches[2].Value));

            return new TimeSpan().Add(days).Add(hours).Add(minutes);
        }
    }
}
