/// <reference types="cypress" />

import mainPage from '../../Pages/mainPage'
import parkinApp from '../../Pages/parkingApp'

const mainPageObj = new mainPage()
const parkingAppObj = new parkinApp()

describe('Parking App', () => {
  beforeEach(() => {
    cy.visit('/')
    mainPageObj.openParkingApp()
  })

  it('Opens parking app', () => {
    cy.intercept({ resourceType: /xhr|fetch/ }, { log: false })
    parkingAppObj.request.calculateCost.setIntercept()
    parkingAppObj.verifyParkingAppUrl()
    parkingAppObj.verifyHeaderExistance()
    parkingAppObj.verifyReservationCardFields()
    parkingAppObj.elements.reservation.calculateButton().click()
    parkingAppObj.request.calculateCost.get().its('response.body').then(calculation => {
      cy.log(calculation)
    })
  })
})