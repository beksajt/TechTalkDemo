import { expect } from '@playwright/test';

export default class ParkingApp {
  constructor(page) {
    this.page = page;
    this.elements = {
      header: this.page.locator('h1', { hasText: 'Parking Cost Calculator App - Practice Test Automation' }),
      parkingRatesCard: this.page.locator('.card-title', { hasText: 'Parking Rates' }).locator('..'),
      reservation: {
        reservationCard: this.page.locator('.card-title', { hasText: 'Reservation details' }).locator('..'),
        parkingLotSelect: this.page.locator('#parkingLot'),
        entryDateInput: this.page.locator('#entryDate'),
        entryTimeInput: this.page.locator('#entryTime'),
        exitDateInput: this.page.locator('#exitDate'),
        exitTimeInput: this.page.locator('#exitTime'),
        calculateButton: this.page.locator('#calculateCost'),
        bookNowButton: this.page.locator('#reserveOnline'),
      },
      result: {
        resultContainer: this.page.locator('#result'),
        resultValue: this.page.locator('#resultValue'),
        resultMessage: this.page.locator('#resultMessage'),
      }
    };


    this.data = {
      url: 'https://practice.expandtesting.com/webpark',
      headerText: 'Parking Cost Calculator App - Practice Test Automation'
    };

    this.request = {
      calculateCost: {
        setIntercept: () => this.page.route('https://practice.expandtesting.com/webpark/calculate-cost', route => route.continue()),
        get: async () => await this.page.waitForResponse('https://practice.expandtesting.com/webpark/calculate-cost')
      }
    };
  }

  async verifyParkingAppUrl() {
    await expect(this.page).toHaveURL(this.data.url);
  }

  async verifyHeaderExistance() {
    await expect(this.elements.header).toBeVisible();
    await expect(this.elements.header).toContainText(this.data.headerText);
  }

  async selectParking(parkingType) {
    await this.elements.reservation.parkingLotSelect.selectOption(parkingType);
  }

  async setEntryDateTime(entryDate) {
    await this.elements.reservation.entryDateInput.fill(entryDate.toISOString().split('T')[0]);
    await this.elements.reservation.entryTimeInput.fill(entryDate.toTimeString().split(' ')[0].substring(0, 5));
  }

  async setExitDateTime(exitDate) {
    await this.elements.reservation.exitDateInput.fill(exitDate.toISOString().split('T')[0]);
    await this.elements.reservation.exitTimeInput.fill(exitDate.toTimeString().split(' ')[0].substring(0, 5));
  }

  async clickCalculateButton() {
    await this.elements.reservation.calculateButton.click();
  }

  async clickBookNowButton() {
    await this.elements.reservation.bookNowButton.click();
  }

  async verifyReservationCardFields() {
    await expect(this.elements.reservation.reservationCard).toBeVisible();
    await expect(this.elements.reservation.parkingLotSelect).toBeVisible();
    await expect(this.elements.reservation.entryDateInput).toBeVisible();
    await expect(this.elements.reservation.entryTimeInput).toBeVisible();
    await expect(this.elements.reservation.exitDateInput).toBeVisible();
    await expect(this.elements.reservation.exitTimeInput).toBeVisible();
    await expect(this.elements.reservation.calculateButton).toBeVisible();
    await expect(this.elements.reservation.bookNowButton).toBeVisible();
  }

  async readPrice() {
    return await this.elements.result.resultValue.textContent();
  }

  async readDuration() {
    const resultText = await this.elements.result.resultMessage.textContent();

    if (!resultText || resultText.trim() === '') {
      throw new Error("Unable to parse duration");
    }

    // Extract days, hours, and minutes from the resultText using a regex
    const matches = resultText.match(/\d+/g);

    if (!matches || matches.length < 3) {
      throw new Error("Invalid duration format");
    }

    const days = parseInt(matches[0], 10);
    const hours = parseInt(matches[1], 10);
    const minutes = parseInt(matches[2], 10);

    return { days, hours, minutes };
  }

  checkPrice(price, expectedPrice) {
    expect(price).toBe(expectedPrice);
  }

  checkDuration(duration, expectedDuration) {
    expect(`${duration.days} Day(s), ${duration.hours} Hour(s), ${duration.minutes} Minute(s)`).toBe(expectedDuration);
  }
}

