class formValidation {
  elements = {
    contactNameInput: () => cy.get('input[name="ContactName"]'),
    contactnumberInput: () => cy.get('input[name="contactnumber"]'),
    pickupdateInput: () => cy.get('input[name="pickupdate"]'),
    paymentSelect: () => cy.get('select[name="payment"]'),
    submitButton: () => cy.get('button[type="submit"]'),
    invalidFeedbackField: '.invalid-feedback'  // Change to a selector string for sibling chaining
  }

  clearContactNameInput() {
    this.elements.contactNameInput().clear()
  }

  clickSubmitButton() {
    this.elements.submitButton().click()
  }

  checkErrorMessages(expectedMessages) {
    Object.entries(expectedMessages).forEach(([inputElement, message]) => {
      this.elements[inputElement]().siblings(this.elements.invalidFeedbackField).should('contain.text', message)
    })
  }



  fillForm(data) {
    this.elements.contactNameInput().type(data.name)
    this.elements.contactnumberInput().type(data.number)
    this.elements.pickupdateInput().type(data.date)
    this.elements.paymentSelect().select(data.option)
  }
}

export default formValidation
