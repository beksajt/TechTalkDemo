import { test, expect } from '@playwright/test';
import MainPage from '../Pages/mainPage';
import FormValidationPage from '../Pages/formValidation';
import FormConfirmationPage from '../Pages/formConfirmation';
import ParkingAppPage from '../Pages/parkingApp';
import BookingPage from '../Pages/bookingPage';
import PaymentPage from '../Pages/paymenPage';
import ConfirmationPage from '../Pages/confirmationPage';

test.describe('Tech talk', () => {
  let mainPageObj;
  let formValidationObj;
  let formConfirmationObj;
  let parkingAppObj;
  let bookingPageObj;
  let paymentPageObj;
  let confirmationPageObj;

  test.beforeEach(async ({ page }) => {
    mainPageObj = new MainPage(page);
    formValidationObj = new FormValidationPage(page);
    formConfirmationObj = new FormConfirmationPage(page);
    parkingAppObj = new ParkingAppPage(page);
    bookingPageObj = new BookingPage(page);
    paymentPageObj = new PaymentPage(page);
    confirmationPageObj = new ConfirmationPage(page);
  });

  test('Form validation test', async ({ page }) => {
    const data = {
      name: 'Andrzej',
      number: '123-4567890',
      date: '2020-11-11',
      option: 'card',
    };

    await page.goto('/');
    await mainPageObj.openFormValidationPage();
    await formValidationObj.fillForm(data);
    await formValidationObj.clickSubmitButton();
    await formConfirmationObj.checkUrl();
    await formConfirmationObj.checkMessage();
  });

  test('Web Parking', async ({ page }) => {
    const bookingData = {
      firstName: 'John',
      lastName: 'Smith',
      emailAddress: 'test@gmail.com',
      phoneNumber: '0123456789',
      vehicleSize: 'medium',
      licensePlateNumber: 'GD02137',
    };

    const parkingType = 'ShortTerm';
    const date = new Date();
    const expectedPrice = '96.00€';
    const expectedMessage = '3 Day(s), 7 Hour(s), 14 Minute(s)';
    const exitDate = new Date(date);
    exitDate.setDate(exitDate.getDate() + 3);
    exitDate.setHours(exitDate.getHours() + 7);
    exitDate.setMinutes(exitDate.getMinutes() + 14);

    await page.goto('/');
    await mainPageObj.openParkingApp();
    await parkingAppObj.selectParking(parkingType);
    await parkingAppObj.setEntryDateTime(date);
    await parkingAppObj.setExitDateTime(exitDate);
    await parkingAppObj.clickCalculateButton();

    const price = await parkingAppObj.readPrice();
    const duration = await parkingAppObj.readDuration();

    await parkingAppObj.checkPrice(price, expectedPrice);
    await parkingAppObj.checkDuration(duration, expectedMessage);

    await parkingAppObj.clickBookNowButton();
    await bookingPageObj.fillBookingForm(bookingData);
    await bookingPageObj.checkSummary(parkingType, date, exitDate, expectedPrice);
    await bookingPageObj.clickContinueButton();

    const paymentData = {
      creditCardNumber: '5200828282828223',
      expirationDate: '1027',
      securityCode: '123',
    };

    await paymentPageObj.fillPaymentForm(paymentData);
    await paymentPageObj.clickCompleteReservationButton();

    await confirmationPageObj.checkSummary(parkingType, date, exitDate, expectedPrice);
  });
});
