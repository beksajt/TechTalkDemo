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

        // Optionally, if you want to block certain requests (like Google Ads), 
        // you would need to set this up with a proxy or network management tool outside Selenium.
    }

    protected static string BaseURL => "https://practice.expandtesting.com/";

    // This method navigates to the page's URL
    public void Open()
    {
        Driver.Navigate().GoToUrl(URL);
    }
}
