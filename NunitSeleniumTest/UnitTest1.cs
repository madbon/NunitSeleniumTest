
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System;

namespace NunitSeleniumTest
{
    public class Tests
    {
        private IWebDriver driver;
        WebDriverWait wait;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://automationexercise.com");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }


        [Test, Order(1)]
        public void VerifyHomePage()
        {
            bool isSliderCarouselDisplayed = driver.FindElement(By.CssSelector("#slider-carousel")).Displayed;

            // Assert that the slider carousel is displayed.
            Assert.IsTrue(isSliderCarouselDisplayed, "Homepage does not load properly");
        }


        [Test, Order(2)]
        public void AddToCart()
        {
            Thread.Sleep(3000); // to have time to manually to close ads
            IWebElement productMenu = wait.Until(e => e.FindElement(By.CssSelector(".shop-menu ul.nav li:nth-child(2) a")));
            productMenu.Click();

            Thread.Sleep(3000); //  to have time to manually to close ads
            IWebElement searchProduct = wait.Until(e => e.FindElement(By.CssSelector("#search_product")));
            searchProduct.SendKeys("dress");

            Thread.Sleep(3000); //  to have time to manually to close ads
            IWebElement submitSearch = wait.Until(e => e.FindElement(By.CssSelector("#submit_search")));
            submitSearch.Click();

            ClickAddToCartByProductId("3");

            ClickAddToCartByProductId("4");

            ClickAddToCartByProductId("16");

            ClickAddToCartByProductId("19");

        }


        public void ClickAddToCartByProductId(string productId)
        {

            Thread.Sleep(3000);
            IWebElement addToCartItem = wait.Until(e => e.FindElement(By.CssSelector($"a[data-product-id='{productId}'].add-to-cart")));
            addToCartItem.Click();
            Thread.Sleep(3000);
            IWebElement closeButton = wait.Until(e => e.FindElement(By.CssSelector("#cartModal .modal-dialog.modal-confirm .modal-content .modal-footer button.close-modal")));
            Thread.Sleep(3000);
            closeButton.Click();

        }





        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //driver.Quit();
        }
    }
}