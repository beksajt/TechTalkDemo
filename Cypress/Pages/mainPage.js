class mainPage {

  elements = {
    examplesSection: () => cy.get('#examples'),
    appsSection: () => cy.get('#apps'),

    formValidationCard: () => cy.contains('h3.card-title', 'Form Validation'),
    webParkingAppCard: () => cy.contains('h3.card-title', 'Web Parking')
  }

  openCard(section, card) {
    section().within(() => {
      card().click()
    })
  }

  openParkingApp() {
    this.elements.appsSection().within(() => {
      this.elements.webParkingAppCard().click()
    })
  }

  openFormValidationPage() {
    this.elements.examplesSection().within(() => {
      this.elements.formValidationCard().click()
    })
  }
}

export default mainPage