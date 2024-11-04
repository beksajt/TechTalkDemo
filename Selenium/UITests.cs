using OpenQA.Selenium.Chrome;

namespace Selenium
{
    public class UITests
    {
        private IWebDriver Driver;

        [SetUp]
        public void Setup()
        {
            Driver = CreateWebDriver();
        }

        [Test]
        public void FormValidationTest()
        {
            // Arrange
            var contactName = "Michael";
            var contactNumber = "123-3333333";
            var pickupDate = DateTime.Now;
            var paymentMethod = PaymentMethod.cashondelivery;
            var expectedMessage = "Thank you for validating your ticket";
            var homePage = new PracticeHomePage(Driver);

            // Act
            homePage.Open();
            var formValidationPage = homePage.GoToFormValidationPage();
            formValidationPage.FillContactName(contactName);
            formValidationPage.FillContactNumber(contactNumber);
            formValidationPage.FillPickupDate(pickupDate);
            formValidationPage.SelectPaymentMethod(paymentMethod);
            formValidationPage.ClickRegisterButton();

            // Assert
            Assert.That(Driver.Url, Is.EqualTo(homePage.URL + "form-confirmation"));
            var alert = Driver.FindElement(By.CssSelector(".alert"));
            Assert.That(alert.Text.Contains(expectedMessage), Is.True);
        }

        [Test]
        public void WebParkingEndToEndTest()
        {
            // Arrange 1 - calculate
            var parkingLot = ParkingLot.ShortTerm;
            var parkingStartTime = DateTime.Now;
            var parkingDuration = new TimeSpan().Add(TimeSpan.FromDays(3)).Add(TimeSpan.FromHours(7)).Add(TimeSpan.FromMinutes(14));
            var expectedPrice = 96.0;
            var homePage = new PracticeHomePage(Driver);
            homePage.Open();

            // Act 1 - calculate
            var webParkingPage = homePage.GoToWebParkingPage();
            webParkingPage.SelectParkingLot(parkingLot);
            webParkingPage.SetEntryDateTime(parkingStartTime);
            webParkingPage.SetExitDateTime(parkingStartTime.Add(parkingDuration));
            webParkingPage.ClickCalculateCost();
            var price = webParkingPage.ReadPrice();
            var duration = webParkingPage.ReadDuration();

            // Assert 1 - calculate
            Assert.That(Driver.Url, Is.EqualTo(webParkingPage.URL));

            Assert.Multiple(() =>
            {
                Assert.That(price, Is.EqualTo(expectedPrice));
                Assert.That(duration, Is.EqualTo(parkingDuration));
            });

            // Arrange 2 - book
            var firstName = "John";
            var lastName = "Smith";
            var emailAddress = "test@gmail.com";
            var phoneNumber = "0123456789";
            var vehicleSize = VehicleSize.medium;
            var licensePlateNumber = "GD02137";

            // Act 2 - book
            var bookingPage = webParkingPage.ClickBookNow();
            bookingPage.FillFirstName(firstName);
            bookingPage.FillLastName(lastName);
            bookingPage.FillEmailAddress(emailAddress);
            bookingPage.FillPhoneNumber(phoneNumber);
            bookingPage.SetVehicleSize(vehicleSize);
            bookingPage.FillLicensePlateNumber(licensePlateNumber);

            // Assert 2 - book
            Assert.Multiple(() =>
            {
                Assert.That(bookingPage.BookingSummarySection.ParkingLot(), Is.EqualTo(parkingLot));
                Assert.That(bookingPage.BookingSummarySection.CheckInTime(), Is.EqualTo(parkingStartTime).Within(1).Minutes);
                Assert.That(bookingPage.BookingSummarySection.CheckOutTime(), Is.EqualTo(parkingStartTime.Add(parkingDuration)).Within(1).Minutes);
                Assert.That(bookingPage.BookingSummarySection.TotalEUR(), Is.EqualTo(expectedPrice));
            });

            // Arrange 3 - pay
            const string creditCardNumber = "5200828282828223";
            var expirationDate = "1027";
            var securityCode = "123";
            var confirmationCode = Driver.Url.Split("/").Last();

            // Act 3 - pay
            var paymentPage = bookingPage.ClickContinueButton();
            paymentPage.FillCreditCardNumber(creditCardNumber);
            paymentPage.FillExpirationDate(expirationDate);
            paymentPage.FillSecurityCode(securityCode);

            // Assert 3 - pay
            Assert.Multiple(() =>
            {
                Assert.That(paymentPage.BookingSummarySection.ParkingLot(), Is.EqualTo(parkingLot));
                Assert.That(paymentPage.BookingSummarySection.CheckInTime(), Is.EqualTo(parkingStartTime).Within(1).Minutes);
                Assert.That(paymentPage.BookingSummarySection.CheckOutTime(), Is.EqualTo(parkingStartTime.Add(parkingDuration)).Within(1).Minutes);
                Assert.That(paymentPage.BookingSummarySection.TotalEUR(), Is.EqualTo(expectedPrice));
            });

            // Act 4 - summary
            var summaryPage = paymentPage.ClickCompleteReservationButton();

            // Assert 4 - summary
            Assert.Multiple(() =>
            {
                Assert.That(summaryPage.BookingSummarySection.ParkingLot(), Is.EqualTo(parkingLot));
                Assert.That(summaryPage.BookingSummarySection.CheckInTime(), Is.EqualTo(parkingStartTime).Within(1).Minutes);
                Assert.That(summaryPage.BookingSummarySection.CheckOutTime(), Is.EqualTo(parkingStartTime.Add(parkingDuration)).Within(1).Minutes);
                Assert.That(summaryPage.BookingSummarySection.TotalEUR(), Is.EqualTo(expectedPrice));
                Assert.That(summaryPage.ConfirmationId(), Is.EqualTo(confirmationCode));
            });
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
            Driver.Dispose();
        }

        private IWebDriver CreateWebDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument(@"load-extension=ddkjiahejlhfcafbddmgiahcphecmpfh\2024.10.28.929_0\");
            ChromeDriver driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            return driver;
        }
    }
}
