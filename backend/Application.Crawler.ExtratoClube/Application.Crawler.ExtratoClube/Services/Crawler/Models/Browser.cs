using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Application.Crawler.ExtratoClube.Services.Crawler.Models;

public class Browser
{
    public IWebDriver Driver;

    public void Start()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless"); 
        Driver = new ChromeDriver(options);
    }

    public void Exit()
    {
        if (Driver != null)
        {
            Driver.Quit();
            Driver = null;
        }
    }
}