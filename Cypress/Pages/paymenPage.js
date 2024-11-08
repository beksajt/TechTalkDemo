class paymentPage {
  elements = {
    cardNumberInput: () => cy.get('#cardNumber'),
    expirationDateInput: () => cy.get('#expirationDate'),
    securityCodeInput: () => cy.get('#securityCode'),
    completeReservationButton: () => cy.get('#completeReservation')
  }

  fillPaymentForm(data) {
    this.elements.cardNumberInput().clear().type(data.creditCardNumber)
    this.elements.expirationDateInput().clear().type(data.expirationDate)
    this.elements.securityCodeInput().clear().type(data.securityCode)
  }

  clickCompleteReservationButton() {
    this.elements.completeReservationButton().click()
  }
}

export default paymentPage