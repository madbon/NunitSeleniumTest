Test Case 1 & 14

Source: 
- https://www.automationexercise.com/
- https://www.automationexercise.com/test_cases

Application
1. Visual Studio

Framework Used
- .NET 6.0 (Long Term Support)

Project Type
- NUnit Test Project

Packages Used
- Selenium.WebDriver
- NUnit

How to use
1.  Open Visual Studio and Click Clone a repository
2. Insert https://github.com/madbon/NunitSeleniumTest.git to repository location
3. Click Clone button
4. Click Test Menu > Run All Tests

Classes
- TestCase1: To Automate Registration of User
- TestCase14: To Automate Place Order, Registration while checking out.

Available Tests (Test Case 1)
- VerifyHomePage
- NavigateToRegisterPage
- RegisterNewUser

Available Tests (Test Case 14)
- VerifyHomePage
- AddToCart
- VerifyCartDisplayed
- UpdateProductQuantity
- ValidateUpdatedAmountInCart
- ProceedToCheckout
- RegisterNewUser
- ProceedToCheckoutAfterRegister
- VerifyAddressCheckout
- Payment


Other Methods (to avoid repetition of code)
- ClickAddToCartByProductId
- NavigateToSelectedProduct
- CheckUpdatedAmountInCart
- GetProductPrice
- GetProductQuantity
- GetProductTotal


