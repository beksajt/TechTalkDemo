export default class MainPage {
  constructor(page) {
    this.page = page;
    this.page.route(/.*(googleads|doubleclick\.net|pagead2).*/, route => route.abort());


    this.elements = {
      examplesSection: this.page.locator('#examples'),
      appsSection: this.page.locator('#apps'),

      // Correct usage of hasText instead of withText
      formValidationCard: this.page.locator('h3.card-title', { hasText: 'Form Validation' }),
      webParkingAppCard: this.page.locator('h3.card-title', { hasText: 'Web Parking' })
    };
  }

  async openCard(section, card) {
    await section.locator(card).click();
  }

  async openParkingApp() {
    await this.elements.appsSection.locator(this.elements.webParkingAppCard).click();
  }

  async openFormValidationPage() {
    await this.elements.examplesSection.locator(this.elements.formValidationCard).click();
  }
}
