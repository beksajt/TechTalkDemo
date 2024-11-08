import { expect } from '@playwright/test';

export default class ConfirmationPage {
  constructor(page) {
    this.page = page;
    this.elements = {
      summarySection: this.page.locator('#reservationDetails')
    };
  }

  async checkSummary(parkingType, startDate, endDate, price) {
    const summarySection = this.elements.summarySection;

    await expect(summarySection.locator('ul > li').nth(1).locator('span.text-muted')).toHaveText(parkingType);
    await expect(summarySection.locator('ul > li').nth(2).locator('span.text-muted')).toHaveText(this.formatDate(startDate));
    await expect(summarySection.locator('ul > li').nth(3).locator('span.text-muted')).toHaveText(this.formatDate(endDate));
    await expect(summarySection.locator('ul > li').nth(4).locator('strong')).toHaveText(price);
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
