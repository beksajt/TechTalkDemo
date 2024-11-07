import { expect } from '@playwright/test';

export default class FormConfirmation {
  constructor(page) {
    this.page = page;
    this.elements = {
      confirmationAlert: this.page.locator('[role="alert"].alert')
    };
    this.data = {
      url: 'https://practice.expandtesting.com/form-confirmation',
      expectedMessage: 'Thank you for validating your ticket'
    };
  }

  async checkUrl() {
    await expect(this.page).toHaveURL(this.data.url);
  }

  async checkMessage() {
    await expect(this.elements.confirmationAlert).toContainText(this.data.expectedMessage);
  }
}


