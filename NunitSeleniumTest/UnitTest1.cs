
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

        [Test, Order(3)]
        public void VerifyCartDisplayed()
        {
            Thread.Sleep(3000); 
            IWebElement cartMenu = wait.Until(e => e.FindElement(By.CssSelector(".shop-menu ul.nav li:nth-child(3) a")));
            cartMenu.Click();

            bool cartInfoTable = driver.FindElement(By.CssSelector("#cart_info_table")).Displayed;

            Assert.IsTrue(cartInfoTable, "Cart does not load properly");
        }

        [Test, Order(4)]
        public void UpdateProductQuantity()
        {
            NavigateToSelectedProduct("3");

            NavigateToSelectedProduct("4");

            NavigateToSelectedProduct("16");

            NavigateToSelectedProduct("19");
        }

        public void NavigateToSelectedProduct(string productId)
        {
            Thread.Sleep(3000);
            IWebElement viewProduct = wait.Until(e => e.FindElement(By.CssSelector("#cart_info_table tbody tr#product-"+productId+" td.cart_description h4 a")));
            viewProduct.Click();

            Thread.Sleep(3000);
            IWebElement quantityInput = wait.Until(e => e.FindElement(By.CssSelector("#quantity")));
            quantityInput.SendKeys("");
            Thread.Sleep(1000);
            quantityInput.SendKeys("2");

            Thread.Sleep(3000);
            IWebElement addToCartButton = wait.Until(e => e.FindElement(By.CssSelector(".cart")));
            addToCartButton.Click();


            IWebElement closeButton = wait.Until(e => e.FindElement(By.CssSelector("#cartModal .modal-dialog.modal-confirm .modal-content .modal-footer button.close-modal")));
            Thread.Sleep(3000);
            closeButton.Click();

            Thread.Sleep(3000);
            IWebElement cartMenu = wait.Until(e => e.FindElement(By.CssSelector(".shop-menu ul.nav li:nth-child(3) a")));
            cartMenu.Click();
        }

        [Test, Order(5)]
        public void ValidateUpdatedAmountInCart()
        {
            CheckUpdatedAmountInCart("3");

            CheckUpdatedAmountInCart("4");

            CheckUpdatedAmountInCart("16");

            CheckUpdatedAmountInCart("19");

            Thread.Sleep(3000);
        }

        public void CheckUpdatedAmountInCart(string productId)
        {
            
            double price = GetProductPrice(productId);

            int quantity = GetProductQuantity(productId);

            double totalDisplayed = GetProductTotal(productId);

            double expectedTotal = quantity * price;

            Assert.That(totalDisplayed, Is.EqualTo(expectedTotal), "The product price is incorrect.");

        }


        public double GetProductPrice(string productId)
        {
            IWebElement viewProduct = wait.Until(e => e.FindElement(By.CssSelector("#cart_info_table tbody tr#product-" + productId + " td.cart_price p")));

            // Get the text of the element and remove "Rs. "
            string text = viewProduct.Text.Replace("Rs. ", "");

            // Convert the string to a double
            double price = Double.Parse(text);

            return price;
        }

        public int GetProductQuantity(string productId)
        {
            IWebElement viewProduct = wait.Until(e => e.FindElement(By.CssSelector("#cart_info_table tbody tr#product-" + productId + " td.cart_quantity button.disabled")));

            string text = viewProduct.Text;

            // Convert the string to a integer
            int quantity = int.Parse(text);

            return quantity;
        }

        public double GetProductTotal(string productId)
        {
            IWebElement viewProduct = wait.Until(e => e.FindElement(By.CssSelector("#cart_info_table tbody tr#product-" + productId + " td.cart_total p.cart_total_price")));

            // Get the text of the element and remove "Rs. "
            string text = viewProduct.Text.Replace("Rs. ", "");

            // Convert the string to a double
            double productTotal = Double.Parse(text);

            return productTotal;
        }

        [Test, Order(6)]
        public void ProceedToCheckout()
        {
            IWebElement proceedCheckout = wait.Until(e => e.FindElement(By.CssSelector("section#do_action .container .col-sm-6 a.check_out")));
            Thread.Sleep(3000);
            proceedCheckout.Click();

            Thread.Sleep(3000);
            IWebElement registerLoginButton = wait.Until(e => e.FindElement(By.CssSelector("#checkoutModal .modal-dialog .modal-body p:nth-child(2) a")));
            registerLoginButton.Click();
        }

        [Test, Order(7)]
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
            bool accountCreated = driver.PageSource.Contains("ACCOUNT CREATED!");
            Assert.IsTrue(accountCreated, "Expected 'ACCOUNT CREATED!' but was not found on the page");

            Thread.Sleep(3000);
            IWebElement continueButton = wait.Until(e => e.FindElement(By.CssSelector("[data-qa='continue-button']")));
            continueButton.Click();
        }

        [Test, Order(8)]
        public void ProceedToCheckoutAfterRegister()
        {
            Thread.Sleep(3000);
            IWebElement cartMenu = wait.Until(e => e.FindElement(By.CssSelector(".shop-menu ul.nav li:nth-child(3) a")));
            cartMenu.Click();

            Thread.Sleep(3000);
            bool cartInfoTable = driver.FindElement(By.CssSelector("#cart_info_table")).Displayed;

            Assert.IsTrue(cartInfoTable, "Cart does not load properly");

            Thread.Sleep(3000);
            IWebElement proceedCheckout = wait.Until(e => e.FindElement(By.CssSelector("section#do_action .container .col-sm-6 a.check_out")));
            Thread.Sleep(3000);
            proceedCheckout.Click();
        }

        [Test, Order(9)]
        public void VerifyAddressCheckout()
        {
            Thread.Sleep(3000);
            IWebElement firstLastName = wait.Until(e => e.FindElement(By.CssSelector("ul#address_delivery li.address_firstname.address_lastname")));
            string textFirstLastName = firstLastName.Text;

            Assert.That(textFirstLastName, Is.EqualTo("Mr. John Wick"), "First & Last name are not the same");

            IWebElement company = wait.Until(e => e.FindElement(By.CssSelector("ul#address_delivery li:nth-child(2)")));
            string textCompany = company.Text;
            Assert.That(textCompany, Is.EqualTo("Continental Inc."), "Company is not the same");

            IWebElement address1 = wait.Until(e => e.FindElement(By.CssSelector("ul#address_delivery li:nth-child(3)")));
            string textAddress1 = address1.Text;
            Assert.That(textAddress1, Is.EqualTo("033B Somewhere St."), "Address details is not the same");

            IWebElement addressPhone = wait.Until(e => e.FindElement(By.CssSelector("ul#address_delivery li.address_phone")));
            string textAddressPhone = addressPhone.Text;
            Assert.That(textAddressPhone, Is.EqualTo("551-123-456-789"), "Phone number is not the same");

            Thread.Sleep(3000);
            IWebElement message = wait.Until(e => e.FindElement(By.CssSelector("#ordermsg textarea.form-control")));
            message.SendKeys("Sample comment about my order");

            Thread.Sleep(3000);
            IWebElement placeOrder = wait.Until(e => e.FindElement(By.CssSelector("a.check_out")));

        }

        [Test, Order(9)]
        public void Payment()
        {
            IWebElement nameOnCard = wait.Until(e => e.FindElement(By.Name("name_on_card")));
            nameOnCard.SendKeys("John Wick");

            IWebElement cardNumber = wait.Until(e => e.FindElement(By.Name("card_number")));
            cardNumber.SendKeys("1234456789001234");

            IWebElement cvc = wait.Until(e => e.FindElement(By.Name("cvc")));
            cvc.SendKeys("123");

            IWebElement expiryMonth = wait.Until(e => e.FindElement(By.Name("expiry_month")));
            expiryMonth.SendKeys("10");

            IWebElement expiryYear = wait.Until(e => e.FindElement(By.Name("expiry_year")));
            expiryYear.SendKeys("2025");

            Thread.Sleep(2000);
            IWebElement payConfirm = wait.Until(e => e.FindElement(By.Id("submit")));
            payConfirm.Click();

            Thread.Sleep(3000);
            string expectedConfirmMessage = "Congratulations! Your order has been confirmed!";
            bool isConfirmMessagePresent = driver.PageSource.Contains(expectedConfirmMessage);

            Assert.IsTrue(isConfirmMessagePresent, $"Expected text '{expectedConfirmMessage}' not found on the page");

            Thread.Sleep(3000);
            IWebElement deleteAccountMenue = wait.Until(e => e.FindElement(By.CssSelector(".shop-menu ul.nav li:nth-child(5) a")));
            deleteAccountMenue.Click();

            Thread.Sleep(3000);
            string expectedDeleteMessage = "Account Deleted!";
            bool isDeleteMessagePresent = driver.PageSource.Contains(expectedDeleteMessage);
            Assert.IsTrue(isDeleteMessagePresent, $"Expected text '{expectedDeleteMessage}' not found on the page");

            Thread.Sleep(3000);
            IWebElement continueButton = wait.Until(e => e.FindElement(By.CssSelector("[data-qa='continue-button']")));
            continueButton.Click();
        }


        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }
    }
}