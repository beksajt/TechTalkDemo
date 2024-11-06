namespace Selenium.Pages;

internal abstract class BasePage
{
    protected IWebDriver Driver;
    protected WebDriverWait Wait;

    public abstract string URL { get; }

    protected BasePage(IWebDriver driver)
    {
        Driver = driver;
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
    }

    protected static string BaseURL => "https://practice.expandtesting.com/";

    // This method navigates to the page's URL
    public void Open()
    {
        Driver.Navigate().GoToUrl(URL);
    }
}
