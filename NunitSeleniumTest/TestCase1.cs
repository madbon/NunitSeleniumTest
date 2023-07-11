using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NunitSeleniumTest
{
    public class TestCase1
    {
        TestCase14 apiHelper = new TestCase14();

        private IWebDriver driver;
        WebDriverWait wait;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://automationexercise.com");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test, Order(0)]
        public void VerifyHomePage()
        {
            bool isSliderCarouselDisplayed = driver.FindElement(By.CssSelector("#slider-carousel")).Displayed;

            // Assert that the slider carousel is displayed.
            Assert.IsTrue(isSliderCarouselDisplayed, "Homepage does not load properly");
        }

        [Test, Order(1)]
        public void NavigateToRegisterPage()
        {
            Thread.Sleep(3000);
            IWebElement signUpLink = wait.Until(e => e.FindElement(By.CssSelector(".shop-menu ul.nav li:nth-child(4) a")));
            signUpLink.Click();
        }

        [Test, Order(2)]
        public void RegisterNewUser()
        {
            IWebElement signUpName = wait.Until(e => e.FindElement(By.CssSelector("[data-qa='signup-name']")));
            signUpName.SendKeys("John Wick");

            IWebElement signUpEmail = wait.Until(e => e.FindElement(By.CssSelector("[data-qa='signup-email']")));
            signUpEmail.SendKeys("johnwick@gm.com");

            IWebElement signUpButton = wait.Until(e => e.FindElement(By.CssSelector("[data-qa='signup-button']")));
            signUpButton.Click();

            Thread.Sleep(5000);

            IWebElement radioButton = wait.Until(e => e.FindElement(By.Id("id_gender1")));
            radioButton.Click();

            IWebElement passWord = wait.Until(e => e.FindElement(By.CssSelector("#password")));
            passWord.SendKeys("1234567890");

            IWebElement dropdownDays = wait.Until(e => e.FindElement(By.Id("days")));
            SelectElement selectElementDays = new SelectElement(dropdownDays);
            selectElementDays.SelectByValue("10");

            IWebElement dropdownMonths = wait.Until(e => e.FindElement(By.Id("months")));
            SelectElement selectElementMonth = new SelectElement(dropdownMonths);
            selectElementMonth.SelectByValue("10");

            IWebElement dropdownYears = wait.Until(e => e.FindElement(By.Id("years")));
            SelectElement selectElementYears = new SelectElement(dropdownYears);
            selectElementYears.SelectByValue("1990");

            IWebElement checkNewsletter = wait.Until(e => e.FindElement(By.Id("newsletter")));
            checkNewsletter.Click();

            IWebElement receivedSpecialOffers = wait.Until(e => e.FindElement(By.Id("optin")));
            receivedSpecialOffers.Click();

            IWebElement firstName = wait.Until(e => e.FindElement(By.Id("first_name")));
            firstName.SendKeys("John");

            IWebElement lastName = wait.Until(e => e.FindElement(By.Id("last_name")));
            lastName.SendKeys("Wick");

            IWebElement company = wait.Until(e => e.FindElement(By.Id("company")));
            company.SendKeys("Continental Inc.");

            IWebElement address1 = wait.Until(e => e.FindElement(By.Id("address1")));
            address1.SendKeys("033B Somewhere St., Los Angeles");

            IWebElement address2 = wait.Until(e => e.FindElement(By.Id("address2")));
            address2.SendKeys("031B Somewhere St., Toronto");

            IWebElement dropdownCountry = wait.Until(e => e.FindElement(By.Id("country")));
            SelectElement selectElementCounty = new SelectElement(dropdownCountry);
            selectElementCounty.SelectByText("United States");

            IWebElement stateDetail = wait.Until(e => e.FindElement(By.Id("state")));
            stateDetail.SendKeys("California");

            IWebElement city = wait.Until(e => e.FindElement(By.Id("city")));
            city.SendKeys("Los Angeles");

            IWebElement zipCode = wait.Until(e => e.FindElement(By.Id("zipcode")));
            zipCode.SendKeys("90001");

            IWebElement mobileNumber = wait.Until(e => e.FindElement(By.Id("mobile_number")));
            mobileNumber.SendKeys("551-123-456-029");

            IWebElement createAccount = wait.Until(e => e.FindElement(By.CssSelector("[data-qa='create-account']")));
            createAccount.Click();

            Thread.Sleep(3000);
            bool accountCreated = driver.PageSource.Contains("Account Created!");
            Assert.IsTrue(accountCreated, "Expected 'Account Created!' but was not found on the page");

            Thread.Sleep(3000);
            IWebElement continueButton = wait.Until(e => e.FindElement(By.CssSelector("[data-qa='continue-button']")));
            continueButton.Click();

            Thread.Sleep(3000);
            IWebElement deleteAccountMenu = wait.Until(e => e.FindElement(By.CssSelector(".shop-menu ul.nav li:nth-child(5) a")));
            deleteAccountMenu.Click();

            Thread.Sleep(3000);
            string expectedDeleteMessage = "Account Deleted!";
            bool isDeleteMessagePresent = driver.PageSource.Contains(expectedDeleteMessage);
            Assert.IsTrue(isDeleteMessagePresent, $"Expected text '{expectedDeleteMessage}' not found on the page");

            Thread.Sleep(3000);
            IWebElement continueButtonAfterDelete = wait.Until(e => e.FindElement(By.CssSelector("[data-qa='continue-button']")));
            continueButtonAfterDelete.Click();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }

    }
}
