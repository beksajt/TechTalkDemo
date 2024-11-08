/// <reference types="cypress" />

import formConfirmation from "../../Pages/formConfirmation"
import formValidation from "../../Pages/formValidation"
import mainPage from "../../Pages/mainPage"
import parkinApp from "../../Pages/parkingApp"
import bookingPage from "../../Pages/bookingPage"
import paymentPage from "../../Pages/paymenPage"
import confirmationPage from "../../Pages/confirmationPage"

const mainPageObj = new mainPage
const formValidationObj = new formValidation
const formConfirmationObj = new formConfirmation
const parkingAppObj = new parkinApp
const bookingPageObj = new bookingPage
const paymentPageObj = new paymentPage
const confirmationPageObj = new confirmationPage

describe('Tech talk', () => {
  it('Form validation test', () => {
    const data = {
      name: 'Andrzej',
      number: '123-4567890',
      date: '2020-11-11',
      option: 'card'
    }
    cy.visit('/')
    mainPageObj.openFormValidationPage()
    formValidationObj.fillForm(data)
    formValidationObj.clickSubmitButton()
    formConfirmationObj.checkUrl()
    formConfirmationObj.checkMessage()
  })

  it('Web Parking', () => {
    const bookingData = {
      firstName: "John",
      lastName: "Smith",
      emailAddress: "test@gmail.com",
      phoneNumber: "0123456789",
      vehicleSize: "medium",
      licensePlateNumber: "GD02137"
    }
    const parkingType = 'ShortTerm'
    const date = new Date()
    const expectedPrice = "96.00€"
    const expectedMessage = "3 Day(s), 7 Hour(s), 14 Minute(s)"
    const exitDate = new Date(date)
    exitDate.setDate(exitDate.getDate() + 3),
      exitDate.setHours(exitDate.getHours() + 7)
    exitDate.setMinutes(exitDate.getMinutes() + 14)


    cy.visit('/')
    mainPageObj.openParkingApp()
    parkingAppObj.selectParking(parkingType)
    parkingAppObj.setEntryDateTime(date)
    parkingAppObj.setExitDateTime(exitDate)
    parkingAppObj.clickCalculateButton()
    parkingAppObj.readPrice().then(price => {
      parkingAppObj.readDuration().then(duration => {
        parkingAppObj.checkPrice(price, expectedPrice)
        parkingAppObj.checkDuration(duration, expectedMessage)
      })
    })
    parkingAppObj.clickBookNowButton()
    bookingPageObj.fillBookingForm(bookingData)
    bookingPageObj.checkSummary(parkingType, date, exitDate, expectedPrice)
    bookingPageObj.clickContinueButton()
    const paymentData = {
      creditCardNumber: "5200828282828223",
      expirationDate: "1027",
      securityCode: "123"
    }
    paymentPageObj.fillPaymentForm(paymentData)
    paymentPageObj.clickCompleteReservationButton()
    confirmationPageObj.checkSummary(parkingType, date, exitDate, expectedPrice)
  })
})