export default class PaymentPage {
  constructor(page) {
    this.page = page;
    this.elements = {
      cardNumberInput: this.page.locator('#cardNumber'),
      expirationDateInput: this.page.locator('#expirationDate'),
      securityCodeInput: this.page.locator('#securityCode'),
      completeReservationButton: this.page.locator('#completeReservation')
    };
  }

  async fillPaymentForm(data) {
    await this.elements.cardNumberInput.fill(data.creditCardNumber);
    await this.elements.expirationDateInput.fill(data.expirationDate);
    await this.elements.securityCodeInput.fill(data.securityCode);
  }

  async clickCompleteReservationButton() {
    await this.elements.completeReservationButton.click();
  }
}


