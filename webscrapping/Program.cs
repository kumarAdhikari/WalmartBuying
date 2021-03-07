using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace webscrapping
{
    class Program
    {
        static void Main(string[] args)
        {
            getHtml();

            
        }

        private static void getHtml()
        {
            Console.WriteLine("Enter Product URL from Walmart: ");
            var Url = Console.ReadLine();
            Console.WriteLine("Your Email for Walmart: ");
            string email = Console.ReadLine();
            Console.WriteLine("Your Password for Walmart: ");
            string userpassword = Console.ReadLine();
            ChromeOptions options = new ChromeOptions();
            using (IWebDriver driver = new ChromeDriver(options))
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Url = Url;
                driver.Navigate();
                bool isItAvailable = false;
                while (isItAvailable == false)
                {
                    try
                    {
                        var element = driver.FindElement(By.XPath("//button[@class ='button spin-button prod-ProductCTA--primary button--primary']"));
                        string elements = driver.FindElement(By.XPath("//button[@class ='button spin-button prod-ProductCTA--primary button--primary']")).Text;
                        
                        if (elements == "Add to cart")
                        {
                            element.Click();
                            isItAvailable = true;
                        }
                        else
                        {
                            driver.Navigate().Refresh();
                            isItAvailable = false;
                        }
                        
                    }
                    catch
                    {
                        TimeSpan.FromSeconds(30);
                        driver.Navigate().Refresh();
                        isItAvailable = false;
                    }

                }
                wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[@class ='button ios-primary-btn-touch-fix hide-content-max-m checkoutBtn button--primary']")));

                var element1 = driver.FindElement(By.XPath("//button[@class ='button ios-primary-btn-touch-fix hide-content-max-m checkoutBtn button--primary']"));
                element1.Click();

                IWebElement username = driver.FindElement(By.XPath("//input[contains(@id, 'sign-in-email')]"));
                IWebElement password = driver.FindElement(By.XPath("//input[contains(@class, 'form-control field-input field-input--primary show-hide')]"));

                username.Clear();
                username.SendKeys(email);

                password.Clear();
                password.SendKeys(userpassword);

                var element2= driver.FindElement(By.XPath("//button[@class='button width-full button--primary']"));
                element2.Click();

                var element3 = driver.FindElement(By.XPath("//button[@class='button auto-submit-place-order no-margin set-full-width-button place-order-btn btn-block-s button--primary']"));
                element3.Click();

                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                ss.SaveAsFile("C://Image.png",
                ScreenshotImageFormat.Png);

            }

        }
    }
}

