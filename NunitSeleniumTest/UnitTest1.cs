
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace NunitSeleniumTest
{
    public class Tests
    {
        private IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://automationexercise.com");
        }

        public IWebElement WaitAndFindElement(By locator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(condition: driver =>
            {
                IWebElement element = driver.FindElement(locator);
                if (element != null && element.Enabled && element.Displayed)
                    return element;
                return null;
            });
        }


        [Test]
        public void VerifyHomePage()
        {
            bool isSliderCarouselDisplayed = driver.FindElement(By.CssSelector("#slider-carousel")).Displayed;

            if (isSliderCarouselDisplayed)
            {
                Assert.Pass("Home page verification is successful.");
            }
            else
            {
                Assert.Fail();
            }
        }


        [Test]
        public void VerifyProductPage()
        {
            driver.Navigate().GoToUrl("https://www.automationexercise.com/products");

            bool isSearchProductDisplyayed = driver.FindElement(By.CssSelector("#search_product")).Displayed;

            if (isSearchProductDisplyayed)
            {
                IWebElement searchInput = driver.FindElement(By.CssSelector("#search_product"));
                searchInput.SendKeys("dress");

                IWebElement submitButton = driver.FindElement(By.CssSelector("#submit_search"));
                submitButton.Click();


                ClickAddToCartByProductId("3");
                ClickAddToCartByProductId("4");
                Thread.Sleep(9000);

                IWebElement hmLink = driver.FindElement(By.CssSelector(".brands-name a[href*='H&M']"));
                hmLink.Click();



            }
            else
            {
                Assert.Fail();
            }
        }
            

        public void ClickAddToCartByProductId(string productId)
        {
            // Find the element with the specified data-product-id and click it
            IWebElement addToCartButton = driver.FindElement(By.CssSelector($"a[data-product-id='{productId}'].add-to-cart"));

            Actions actions = new Actions(driver);
            
            actions.MoveToElement(addToCartButton).Perform();

            Thread.Sleep(9000);
            addToCartButton.Click();

            
            IWebElement continueShopButton = driver.FindElement(By.CssSelector(".close-modal"));

            Thread.Sleep(9000);
            continueShopButton.Click();
            
        }




        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
        }
    }
}