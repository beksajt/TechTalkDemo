class formConfirmation {
  elements = {
    confirmationAlert: () => cy.get('[role="alert"].alert')
  }

  data = {
    url: 'https://practice.expandtesting.com/form-confirmation',
    expectedMessage: 'Thank you for validating your ticket'
  }

  checkUrl() {
    cy.url().should('eq', this.data.url)
  }

  checkMessage() {
    this.elements.confirmationAlert().should('contain.text', this.data.expectedMessage)
  }
}

export default formConfirmation