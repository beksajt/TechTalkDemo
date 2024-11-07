class confirmationPage {
  elements = {
    summarySection: () => cy.get('#reservationDetails')
  }

  checkSummary(parkingType, startDate, endDate, price) {
    this.elements.summarySection().find('ul').within(() => {
      cy.get('span').eq(1).should('have.text', parkingType)
      cy.get('span').eq(2).should('have.text', this.formatDate(startDate))
      cy.get('span').eq(3).should('have.text', this.formatDate(endDate))
      cy.get('li').eq(4).should('contain.text', `Total (EUR):`).and('contain.text', price)

    })
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
export default confirmationPage