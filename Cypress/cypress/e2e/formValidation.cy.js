import formConfirmation from "../../Pages/formConfirmation"
import formValidation from "../../Pages/formValidation"
import mainPage from "../../Pages/mainPage"

const mainPageObj = new mainPage
const formValidationObj = new formValidation
const formConfirmationObj = new formConfirmation

describe('Form validation tests (POM)', () => {
  beforeEach(() => {
    cy.intercept({ resourceType: /xhr|fetch/ }, { log: false })

    cy.visit('/')
    mainPageObj.openFormValidation()
    formValidationObj.clearContactNameInput()
  })

  it('Checks error messages (POM)', () => {
    formValidationObj.clickSubmitButton();
    formValidationObj.checkErrorMessages({
      contactNameInput: 'Please enter your Contact name.',
      contactnumberInput: 'Please provide your Contact number.',
      pickupdateInput: 'Please provide valid Date.',
      paymentSelect: 'Please select the Paymeny Method.'
    });
  });


  it('Submits form properly (POM)', () => {
    const data = {
      name: 'Andrzej',
      number: '123-4567890',
      date: '2020-11-11',
      option: 'card'
    }
    formValidationObj.fillForm(data)
    formValidationObj.clickSubmitButton()
    formConfirmationObj.checkUrl()
    formConfirmationObj.checkMessage()
  })
})

describe('Form validation tests (NO POM)', () => {
  beforeEach(() => {
    cy.intercept({ resourceType: /xhr|fetch/ }, { log: false })
    cy.visit('/')
    cy.get('#examples').within(() => {
      cy.contains('h3.card-title', 'Form Validation').click()
    })
    cy.get('input[name="ContactName"]').clear()
  })

  it('Checks error mesages (NO POM)', () => {
    cy.get('button[type="submit"]').click()
    cy.get('input[name="ContactName"]').siblings('.invalid-feedback').should('contain.text', 'Please enter your Contact name.')
    cy.get('input[name="contactnumber"]').siblings('.invalid-feedback').should('contain.text', 'Please provide your Contact number.')
    cy.get('input[name="pickupdate"]').siblings('.invalid-feedback').should('contain.text', 'Please provide valid Date.')
    cy.get('select[name="payment"]').siblings('.invalid-feedback').should('contain.text', 'Please select the Paymeny Method.')
  })

  it('Submits form properly (NO POM)', () => {
    const data = {
      name: 'Andrzej',
      number: '123-4567890',
      date: '2020-11-11',
      option: 'card'
    }

    cy.get('input[name="ContactName"]').type(data.name)
    cy.get('input[name="contactnumber"]').type(data.number)
    cy.get('input[name="pickupdate"]').type(data.date)
    cy.get('select[name="payment"]').select(data.option)
    cy.get('button[type="submit"]').click()
    cy.url().should('eq', 'https://practice.expandtesting.com/form-confirmation')
    cy.get('[role="alert"].alert').should('contain.text', 'Thank you for validating your ticket')
  })
})