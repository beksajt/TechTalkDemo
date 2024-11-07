import { expect } from '@playwright/test';

export default class FormValidation {
  constructor(page) {
    this.page = page;
    this.elements = {
      contactNameInput: this.page.locator('input[name="ContactName"]'),
      contactnumberInput: this.page.locator('input[name="contactnumber"]'),
      pickupdateInput: this.page.locator('input[name="pickupdate"]'),
      paymentSelect: this.page.locator('select[name="payment"]'),
      submitButton: this.page.locator('button[type="submit"]'),
      invalidFeedbackField: '.invalid-feedback'  // A simple string locator for sibling selection
    };
  }

  async clearContactNameInput() {
    await this.elements.contactNameInput.fill('');  // Playwright's equivalent of Cypress's `clear()`
  }

  async clickSubmitButton() {
    await this.elements.submitButton.click();
  }

  async checkErrorMessages(expectedMessages) {
    for (const [inputElement, message] of Object.entries(expectedMessages)) {
      const inputLocator = this.elements[inputElement];
      const feedbackLocator = inputLocator.locator(this.elements.invalidFeedbackField);
      await expect(feedbackLocator).toContainText(message);
    }
  }

  async fillForm(data) {
    await this.elements.contactNameInput.fill(data.name);
    await this.elements.contactnumberInput.fill(data.number);
    await this.elements.pickupdateInput.fill(data.date);
    await this.elements.paymentSelect.selectOption(data.option);
  }
}

