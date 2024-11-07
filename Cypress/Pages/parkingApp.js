class parkinApp {
  elements = {
    header: () => cy.get('h1').contains('Parking Cost Calculator App - Practice Test Automation'),
    parkingRatesCard: () => cy.contains('.card-title', 'Parking Rates').closest('.card-body'),
    reservation: {
      reservationCard: () => cy.contains('.card-title', 'Reservation details').closest('.card-body'),
      parkingLotSelect: () => cy.get('#parkingLot'),
      entryDateInput: () => cy.get('#entryDate'),
      entryTimeInput: () => cy.get('#entryTime'),
      exitDateInput: () => cy.get('#exitDate'),
      exitTimeInput: () => cy.get('#exitTime'),
      calculateButton: () => cy.get('#calculateCost'),
      bookNowButton: () => cy.get('#reserveOnline'),

    },
    result: {
      resultContainer: () => cy.get('#result'),
      resultValue: () => cy.get('#resultValue'),
      resultMessage: () => cy.get('#resultMessage')
    }
  }
  data = {
    url: 'https://practice.expandtesting.com/webpark',
    headerText: 'Parking Cost Calculator App - Practice Test Automation'
  }

  request = {
    calculateCost: {
      setIntercept: () => cy.intercept('https://practice.expandtesting.com/webpark/calculate-cost').as('calculateCost'),
      get: () => cy.get('@calculateCost')
    }

  }

  verifyParkingAppUrl() {
    cy.url().should('eq', this.data.url)
  }
  verifyHeaderExistance() {
    this.elements.header().should('exist').and('contain.text', this.data.headerText)
  }

  selectParking(parkingType) {
    this.elements.reservation.parkingLotSelect().select(parkingType)
  }

  setEntryDateTime(entryDate) {
    this.elements.reservation.entryDateInput().clear().type(entryDate.toISOString().split('T')[0])
    this.elements.reservation.entryTimeInput().clear().type(entryDate.toTimeString().split(' ')[0].substring(0, 5))
  }

  setExitDateTime(exitDate) {
    this.elements.reservation.exitDateInput().clear().type(exitDate.toISOString().split('T')[0])
    this.elements.reservation.exitTimeInput().clear().type(exitDate.toTimeString().split(' ')[0].substring(0, 5))
  }

  clickCalculateButton() {
    this.elements.reservation.calculateButton().click()
  }

  clickBookNowButton() {
    this.elements.reservation.bookNowButton().click()
  }

  verifyReservationCardFields() {
    this.elements.reservation.reservationCard().within(() => {
      this.elements.reservation.parkingLotSelect().should('exist')
      this.elements.reservation.entryDateInput().should('exist')
      this.elements.reservation.entryTimeInput().should('exist')
      this.elements.reservation.exitDateInput().should('exist')
      this.elements.reservation.exitimeInput().should('exist')
      this.elements.reservation.calculateButton().should('exist').click()
      this.elements.reservation.bookNowButton().should('exist')
      this.elements.reservation.calculationResult().should('exist')
    })
  }

  readPrice() {
    return this.elements.result.resultValue().invoke('text').then(price => {
      return price
    })
  }
  readDuration() {
    return this.elements.result.resultMessage().invoke('text').then(resultText => {

      if (!resultText || resultText.trim() === '') {
        throw new Error("Unable to parse duration")
      }

      // Extract days, hours, and minutes from the resultText using a regex
      const matches = resultText.match(/\d+/g)

      if (!matches || matches.length < 3) {
        throw new Error("Invalid duration format")
      }

      const days = parseInt(matches[0], 10)
      const hours = parseInt(matches[1], 10)
      const minutes = parseInt(matches[2], 10)

      // Return the parsed duration inside a Cypress chain to avoid the issue with async/sync mismatch
      return {
        days,
        hours,
        minutes
      }
    })
  }

  checkPrice(price, expectedPrice) {
    expect(price).to.be.equal(expectedPrice)
  }

  checkDuration(duration, expectedDuration) {
    expect(`${duration.days} Day(s), ${duration.hours} Hour(s), ${duration.minutes} Minute(s)`).to.be.equal(expectedDuration)
  }

}
export default parkinApp