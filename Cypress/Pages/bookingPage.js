class bookingPage {
  elements = {
    firstNameInput: () => cy.get('#firstName'),
    lastNameInput: () => cy.get('#lastName'),
    emailInput: () => cy.get('#email'),
    phoneInput: () => cy.get('#phone'),
    vehicleSizeSelect: () => cy.get('#vehicleSize'),
    lpNumberInput: () => cy.get('#lpNumber'),
    summarySection: () => cy.get('#reservationDetails'),
    continueButton: () => cy.get('#continue')
  }

  fillBookingForm(data) {
    this.elements.firstNameInput().clear().type(data.firstName)
    this.elements.lastNameInput().clear().type(data.lastName)
    this.elements.emailInput().clear().type(data.emailAddress)
    this.elements.phoneInput().clear().type(data.phoneNumber)
    this.elements.vehicleSizeSelect().select(data.vehicleSize)
    this.elements.lpNumberInput().clear().type(data.licensePlateNumber)
  }

  checkSummary(parkingType, startDate, endDate, price) {
    this.elements.summarySection().find('ul').within(() => {
      cy.get('span').eq(0).should('have.text', parkingType)
      cy.get('span').eq(1).should('have.text', this.formatDate(startDate))
      cy.get('span').eq(2).should('have.text', this.formatDate(endDate))
      cy.get('li').eq(3).should('contain.text', `Total (EUR):`).and('contain.text', price)

    })
  }

  clickContinueButton() {
    this.elements.continueButton().click()
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

export default bookingPage