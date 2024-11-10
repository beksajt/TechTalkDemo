namespace Playwright;

public class UITests : BaseTest
{
    [Test]
    public async Task FormValidationTest()
    {
        //Arrange
        var contactName = "Michael";
        var contactNumber = "123-3333333";
        var pickupDate = DateTime.Now;
        var paymentMethod = PaymentMethod.cashondelivery;
        var expectedMessage = "Thank you for validating your ticket";
        var homePage = new PracticeHomePage(Page);

        //Act
        await homePage.OpenAsync();
        var formValidationPage = await homePage.GoToFormValidationPage();
        await formValidationPage.FillContactName(contactName);
        await formValidationPage.FillContactNumber(contactNumber);
        await formValidationPage.FillPickupDate(pickupDate);
        await formValidationPage.SelectPaymentMethod(paymentMethod);
        await formValidationPage.ClickRegisterButton();

        //Assert
        await Expect(Page).ToHaveURLAsync(homePage.URL + "form-confirmation");
        await Expect(Page.GetByRole(AriaRole.Alert)).ToHaveTextAsync(expectedMessage);
    }

    [Test]
    public async Task WebParkingEndToEndTest()
    {
        //Arrange 1 - calculate
        var parkingLot = ParkingLot.ShortTerm;
        var parkingStartTime = DateTime.Now;
        var parkingDuration = new TimeSpan()
            .Add(TimeSpan.FromDays(3))
            .Add(TimeSpan.FromHours(7))
            .Add(TimeSpan.FromMinutes(14));
        var expectedPrice = 96.0;
        var homePage = new PracticeHomePage(Page);
        await homePage.OpenAsync();

        //Act 1 - calculate
        var webParkingPage = await homePage.GoToWebParkingPage();
        await webParkingPage.SelectParkingLot(parkingLot);
        await webParkingPage.SetEntryDateTime(parkingStartTime);
        await webParkingPage.SetExitDateTime(parkingStartTime.Add(parkingDuration));
        await webParkingPage.ClickCalculateCost();
        var price = await webParkingPage.ReadPrice();
        var duration = await webParkingPage.ReadDuration();

        //Assert 1 - calculate
        await Expect(Page).ToHaveURLAsync(webParkingPage.URL);

        Assert.Multiple(() =>
        {
            Assert.That(price, Is.EqualTo(expectedPrice));
            Assert.That(duration, Is.EqualTo(parkingDuration));
        });

        //Arrange 2 - book
        var firstName = "John";
        var lastName = "Smith";
        var emailAddress = "test@gmail.com";
        var phoneNumber = "0123456789";
        var vehicleSize = VehicleSize.medium;
        var licensePlateNumber = "GD02137";

        //Act 2 - book
        var bookingPage = await webParkingPage.ClickBookNow();
        await bookingPage.FillFirstName(firstName);
        await bookingPage.FillLastName(lastName);
        await bookingPage.FillEmailAddress(emailAddress);
        await bookingPage.FillPhoneNumber(phoneNumber);
        await bookingPage.SetVehicleSize(vehicleSize);
        await bookingPage.FillLicensePlateNumber(licensePlateNumber);

        //Assert 2 - book
        Assert.Multiple(async () =>
        {
            Assert.That(await bookingPage.BookingSummarySection.ParkingLot(), Is.EqualTo(parkingLot));
            Assert.That(await bookingPage.BookingSummarySection.CheckInTime(), Is.EqualTo(parkingStartTime).Within(1).Minutes);
            Assert.That(await bookingPage.BookingSummarySection.CheckOutTime(), Is.EqualTo(parkingStartTime.Add(parkingDuration)).Within(1).Minutes);
            Assert.That(await bookingPage.BookingSummarySection.TotalEUR(), Is.EqualTo(expectedPrice));
        });

        //Arrange 3 - pay
        const string creditCardNumber = "5200828282828223";
        var expirationDate = "1027";
        var securityCode = "123";
        var confirmationCode = Page.Url.Split("/").Last();

        //Act 3 - pay
        var paymentPage = await bookingPage.ClickContinueButton();
        await paymentPage.FillCreditCardNumber(creditCardNumber);
        await paymentPage.FillExpirationDate(expirationDate);
        await paymentPage.FillSecurityCode(securityCode);

        //Assert 3 - pay
        Assert.Multiple(async () =>
        {
            Assert.That(await paymentPage.BookingSummarySection.ParkingLot(), Is.EqualTo(parkingLot));
            Assert.That(await paymentPage.BookingSummarySection.CheckInTime(), Is.EqualTo(parkingStartTime).Within(1).Minutes);
            Assert.That(await paymentPage.BookingSummarySection.CheckOutTime(), Is.EqualTo(parkingStartTime.Add(parkingDuration)).Within(1).Minutes);
            Assert.That(await paymentPage.BookingSummarySection.TotalEUR(), Is.EqualTo(expectedPrice));
        });

        //Act 4 - summary
        var summaryPage = await paymentPage.ClickCompleteReservationButton();

        //Assert 4 - summary
        Assert.Multiple(async () =>
        {
            Assert.That(await summaryPage.BookingSummarySection.ParkingLot(), Is.EqualTo(parkingLot));
            Assert.That(await summaryPage.BookingSummarySection.CheckInTime(), Is.EqualTo(parkingStartTime).Within(1).Minutes);
            Assert.That(await summaryPage.BookingSummarySection.CheckOutTime(), Is.EqualTo(parkingStartTime.Add(parkingDuration)).Within(1).Minutes);
            Assert.That(await summaryPage.BookingSummarySection.TotalEUR(), Is.EqualTo(expectedPrice));
            Assert.That(await summaryPage.ConfirmationId(), Is.EqualTo(confirmationCode));
        });
    }
}