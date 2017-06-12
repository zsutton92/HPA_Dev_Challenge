using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

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
                    IWebElement stepOneElement = driver.FindElement(By.XPath("//*[text()='Step 1.']"));
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

                    IWebElement stepFourElement = driver.FindElement(By.XPath("//*[text() ='Step 4.']"));
                    if (stepFourElement != null)
                    {
                        Console.WriteLine(stepFourElement.Text);
                        IWebElement selectElementNeeded = driver.FindElement(By.XPath("//*[@id='selectionVal']"));
                        string strSelectText = selectElementNeeded.Text;

                        IWebElement dropDownElement = driver.FindElement(By.XPath("//*//*//select"));
                        SelectElement selectElement = new SelectElement(dropDownElement);
                        if (selectElement != null)
                        {
                            Console.WriteLine("Select " + strSelectText);
                            selectElement.SelectByText(strSelectText);
                            //selectElement.Click();
                            alert = driver.SwitchTo().Alert();
                            alert.Accept();
                        }
                    }

                    IWebElement stepFiveElement = driver.FindElement(By.XPath("//*[text() ='Step 5.']"));
                    string formResult = "" ;
                    if (stepFiveElement != null)
                    {
                        Console.WriteLine(stepFiveElement.Text);
                        IWebElement formElement;
                        string placeholder;

                        for (int i = 1; i < 10; i++)
                        {
                            formElement = driver.FindElement(By.XPath("//*[@id='FormTable']/tbody/tr[" + i + "]/td/*"));
                            placeholder = formElement.GetAttribute("placeholder");
                            Console.WriteLine("Sending " + placeholder + " to form.");
                            formElement.SendKeys(placeholder);
                        }

                        formElement = driver.FindElement(By.XPath("//*[@id='FormTable']/tbody/tr[10]/td/center/button"));
                        formElement.Click();

                        alert = driver.SwitchTo().Alert();
                        alert.Accept();

                        formElement = driver.FindElement(By.Id("formResult"));
                        formResult = formElement.Text;
                        Console.WriteLine("Result: " + formResult);
                    }

                    IWebElement stepSixElement = driver.FindElement(By.XPath("//*[text() = 'Step 6.']"));
                    if (stepSixElement != null)
                    {
                        Console.WriteLine(stepSixElement.Text);
                        string lineNumber = driver.FindElement(By.Id("lineNum")).Text;
                        Console.WriteLine("Line number to insert into: " + lineNumber);

                        IWebElement textboxElement = driver.FindElement(By.XPath("//*[@id='inputTable']/tbody/tr[" + lineNumber + "]/td[2]/input"));
                        textboxElement.Clear();
                        textboxElement.SendKeys(formResult);
                        textboxElement.SendKeys(Keys.Tab);
                       
                        alert = driver.SwitchTo().Alert();
                        alert.Accept();                        
                    }


                    for (int i = 7; i < 11; i++)
                    {
                        IWebElement stepElement = driver.FindElement(By.XPath("//*[text()= 'Step " + i + ".']"));
                        if (stepElement != null)
                        {
                            Console.WriteLine(stepElement.Text);
                            stepElement.Click();
                            bool dialogPresent = false;
                            do
                            {
                                dialogPresent = CheckForDialog(driver);
                            } while (!dialogPresent);



                            alert = driver.SwitchTo().Alert();
                            alert.Accept();

                        }
                    }

                }                                   
                Console.ReadLine();
            }
        }

        static bool ValidateURL(string strURL)
        {
            bool result = false;
            Uri uriResult;

            result = Uri.TryCreate(strURL, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;            
        }

        static bool CheckForDialog(IWebDriver driver)
        {
            bool result;
            try
            {
                driver.SwitchTo().Alert();
                result = true;                
            }
            catch (NoAlertPresentException)
            {
                result = false;
            }
            return result;
        }
    }
}
