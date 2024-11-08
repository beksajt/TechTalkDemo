import { expect } from '@playwright/test';

export default class BookingPage {
  constructor(page) {
    this.page = page;
    this.elements = {
      firstNameInput: this.page.locator('#firstName'),
      lastNameInput: this.page.locator('#lastName'),
      emailInput: this.page.locator('#email'),
      phoneInput: this.page.locator('#phone'),
      vehicleSizeSelect: this.page.locator('#vehicleSize'),
      lpNumberInput: this.page.locator('#lpNumber'),
      summarySection: this.page.locator('#reservationDetails'),
      continueButton: this.page.locator('#continue')
    };
  }

  async fillBookingForm(data) {
    await this.elements.firstNameInput.fill(data.firstName);
    await this.elements.lastNameInput.fill(data.lastName);
    await this.elements.emailInput.fill(data.emailAddress);
    await this.elements.phoneInput.fill(data.phoneNumber);
    await this.elements.vehicleSizeSelect.selectOption(data.vehicleSize);
    await this.elements.lpNumberInput.fill(data.licensePlateNumber);
  }

  async checkSummary(parkingType, startDate, endDate, price) {
    const summarySection = this.elements.summarySection;

    await expect(summarySection.locator('ul > li').nth(0).locator('span.text-muted')).toHaveText(parkingType);
    await expect(summarySection.locator('ul > li').nth(1).locator('span.text-muted')).toHaveText(this.formatDate(startDate));
    await expect(summarySection.locator('ul > li').nth(2).locator('span.text-muted')).toHaveText(this.formatDate(endDate));
    await expect(summarySection.locator('ul > li').nth(3).locator('strong')).toHaveText(price);

  }

  async clickContinueButton() {
    await expect(this.elements.continueButton).toBeEnabled();
    await expect(this.elements.continueButton).toBeVisible();
    await this.elements.continueButton.click();

  }

  formatDate(date) {
    return date.toLocaleString('sv-SE', {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit',
    }).replace(' ', 'T').replace('T', ' ');
  }
}

