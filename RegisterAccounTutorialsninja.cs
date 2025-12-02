
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace SeleniumCsharpNUnitDemo
{
    [TestFixture]
    public class RegisterAccounTutorialsninja
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://tutorialsninja.com/demo/index.php?route=account/register");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        [Test]
        public void CompleteRegistrationForm()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input-firstname"))).SendKeys("Tinu");
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input-lastname"))).SendKeys("Tony");
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input-email"))).SendKeys("tinu.tony" + DateTime.Now.Ticks + "@example.com");
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input-telephone"))).SendKeys("9012345678");
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input-password"))).SendKeys("SecurePassword123");
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input-confirm"))).SendKeys("SecurePassword123");

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[name='newsletter'][value='1']"))).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("agree"))).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("input[type='submit'][value='Continue']"))).Click();

            string successMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#content h1"))).Text;
            Assert.That(successMessage, Does.Contain("Your Account Has Been Created!"), "Account creation message not found");

            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a.btn.btn-primary"))).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("View your order history"))).Click();
        }

        [TearDown]
        public void Cleanup()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
