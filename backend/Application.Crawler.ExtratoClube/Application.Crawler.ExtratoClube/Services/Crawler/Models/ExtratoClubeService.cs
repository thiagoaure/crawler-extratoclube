using Application.Crawler.ExtratoClube.Commands.Helpers;
using Application.Crawler.ExtratoClube.DTOS;
using Application.Crawler.ExtratoClube.Entities;
using Application.Crawler.ExtratoClube.Interfaces.Commands;
using Application.Crawler.ExtratoClube.Interfaces.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Application.Crawler.ExtratoClube.Services.Crawler.Models;

public class ExtratoClubeService : IExtratoClubeService
{
    public Browser Browser = new Browser();
    private string url = "http://extratoclube.com.br/";
    public IWebDriver Frame;
    IWebDriver Driver;


    public bool SignIn(string username, string password)
    {
        Browser.Start();
        Driver = Browser.Driver;
        Driver.Navigate().GoToUrl(url);
        IWebElement FrameElement = Driver.FindElement(By.TagName("frame"));
        Frame = Driver.SwitchTo().Frame(FrameElement);

        IWebElement inputUser = Frame.FindElement(By.XPath("//*[@id='user']"));
        inputUser.SendKeys(username);

        IWebElement inputPassword = Frame.FindElement(By.XPath("//*[@id='pass']"));
        inputPassword.SendKeys(password);

        IWebElement btnLogar = Frame.FindElement(By.XPath("//*[@id='botao']"));
        btnLogar.Click();

        if (Frame == null)
        {
            Console.WriteLine("fail credentials");
            return false;
        }
        return true;
    }

    public ICommandResult<UserResponse> ExtractBenefit(User user)
    {
        try
        {
            Browser.Start();
            Driver = Browser.Driver;
            Driver.Navigate().GoToUrl(url);
            IWebElement FrameElement = Driver.FindElement(By.TagName("frame"));
            Frame = Driver.SwitchTo().Frame(FrameElement);

            IWebElement inputUser = Frame.FindElement(By.XPath("//*[@id='user']"));
            inputUser.SendKeys(user.Login);

            IWebElement inputPassword = Frame.FindElement(By.XPath("//*[@id='pass']"));
            inputPassword.SendKeys(user.Password);

            IWebElement btnLogin = Frame.FindElement(By.XPath("//*[@id='botao']"));
            btnLogin.Click();

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            IWebElement closeView = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("ion-button[title='Fechar']")));
            closeView.Click();

            IWebElement closeMenu = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("ion-menu[menu-id='first']")));
            closeMenu.Click();

            Actions actions = new Actions(Driver);
            IWebElement content = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"main\"]/ion-content")));
            actions.MoveToElement(content).Click().SendKeys(Keys.End).Perform();

            IWebElement btnBenefit = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"extratoonline\"]/ion-row[2]/ion-col/ion-card/ion-button[16]")));
            btnBenefit.Click();
            actions.MoveToElement(content).SendKeys(Keys.End).Perform();

            IWebElement inputCpf = Frame.FindElement(By.XPath("//*[@id='extratoonline']/ion-row[2]/ion-col/ion-card/ion-grid/ion-row[2]/ion-col/ion-card/ion-item/ion-input/input"));
            inputCpf.SendKeys(user.Cpf);


            IWebElement btnSearch = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"extratoonline\"]/ion-row[2]/ion-col/ion-card/ion-grid/ion-row[2]/ion-col/ion-card/ion-button")));
            btnSearch.Click();
            actions.MoveToElement(content).ClickAndHold().SendKeys(Keys.End).Perform();
            System.Threading.Thread.Sleep(2000);

            IWebElement dataElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector(".item.md.ion-focusable.hydrated.item-label")));
            Console.WriteLine(dataElement.Text);
            if (dataElement != null && int.TryParse(dataElement.Text, out int result))
            {
                string data = dataElement.Text;
                var userResponse = new UserResponse
                {
                    ExistRegistrationNumber = true,
                    RegistrationNumber = data
                };
                return new CommandResult<UserResponse>(true, "", userResponse);
            }
            else
            {
                var userResponse = new UserResponse
                {
                    ExistRegistrationNumber = false,
                    RegistrationNumber = ""
                };
                return new CommandResult<UserResponse>(true, "", userResponse);
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine("Ocorreu um erro ao processar cpf em extratoclube: " + ex.Message);
            return null;
        }
    }
}
