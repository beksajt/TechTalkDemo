using System.Collections.Generic;

namespace Playwright.Pages;

internal class FormValidationPage : BasePage
{
    public FormValidationPage(IPage page) : base(page)
    {
    }

    public override string URL => BaseURL + "form-validation";
    private ILocator ContactNameInput => Page.GetByLabel("Contact Name");
    private ILocator ContactNumberInput => Page.GetByLabel("Contact number");
    private ILocator PickupDateInput => Page.Locator("input[name='pickupdate']");
    private ILocator PaymentMethodDropdown => Page.GetByLabel("Payment Method");
    private ILocator RegisterButton => Page.GetByRole(AriaRole.Button, new() { Name = "Register" });

    public async Task FillContactName(string contactName)
    {
        await ContactNameInput.FillAsync(contactName);
    }

    public async Task FillContactNumber(string contactNumber)
    {
        await ContactNumberInput.FillAsync(contactNumber);
    }

    public async Task FillPickupDate(DateTime pickupDate)
    {
        await PickupDateInput.ClickAsync();
        await PickupDateInput.FillAsync(pickupDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
    }

    public async Task SelectPaymentMethod(PaymentMethod paymentMethod)
    {
        await PaymentMethodDropdown.SelectOptionAsync(paymentMethod.ToString());
    }

    public async Task ClickRegisterButton()
    {
        await RegisterButton.ClickAsync();
    }
}