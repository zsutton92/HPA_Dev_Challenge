using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.IE;

namespace HPA_Dev_Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string strUrl = args[0];
            bool validUrl = ValidateURL(strUrl);
            IAlert alert;
            if (validUrl)
            {
                IWebDriver driver = new InternetExplorerDriver();
                

                driver.Navigate().GoToUrl(strUrl);

                if (driver.Url == strUrl)
                {
                    Console.WriteLine("Successfully loaded " + strUrl);
                    //Find Step One
                    IWebElement stepOneElement =  driver.FindElement(By.XPath("//*[text()='Step 1.']"));
                    if (stepOneElement != null)
                    {                      
                        Console.WriteLine(stepOneElement.Text);
                        stepOneElement.Click();
                        alert = driver.SwitchTo().Alert();
                        alert.Accept();
                    }
                    else
                    {
                        Console.WriteLine("Unable to find Step 1.");
                    }
                    
                    IWebElement stepTwoElement = driver.FindElement(By.XPath("//*[text() ='Step 2.']"));
                    if (stepTwoElement != null)
                    {
                        Console.WriteLine(stepTwoElement.Text);
                        stepTwoElement.SendKeys(Keys.Tab);
                        stepTwoElement.SendKeys(Keys.Tab);
                        alert = driver.SwitchTo().Alert();
                        alert.Accept();
                    }

                    IWebElement stepThreeElement = driver.FindElement(By.XPath("//*[text() ='Step 3.']"));
                    if (stepThreeElement != null)
                    {
                        Console.WriteLine(stepThreeElement.Text);
                        IWebElement optionElement = driver.FindElement(By.XPath("//*[@id='optionVal']"));
                        Console.WriteLine("Select Option " + optionElement.Text);
                        IWebElement optionElementNeeded = driver.FindElement(By.XPath("//*//*//input[" + optionElement.Text + "]"));
                        optionElementNeeded.Click();

                        alert = driver.SwitchTo().Alert();
                        alert.Accept();
                    }

                }
            }
            Console.ReadLine();
        }

        static bool ValidateURL(string strURL)
        {
            bool result = false;
            Uri uriResult;

            result = Uri.TryCreate(strURL, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;            
        }

        static bool CheckForDialog(IWebDriver driver,string parent)
        {
            bool result = false;
            if (driver.WindowHandles.Count > 1)
            {
                foreach(string window in driver.WindowHandles)
                {
                    if(!window.Equals(parent))
                    {
                        driver.SwitchTo().Window(window);
                        Console.WriteLine("Modal Dialog Found!");
                    }
                }
            }


            return result;
        }
    }
}
