using System;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace HPA_Dev_Challenge
{
    class Program
    {
        static string formResult = "";
        static void Main(string[] args)
        {
            string strUrl = @"http://hpa.services/automation-challenge/";
            bool validUrl = ValidateURL(strUrl);

            if (validUrl)
            {
                IWebDriver driver = new InternetExplorerDriver();


                driver.Navigate().GoToUrl(strUrl);

                if (driver.Url == strUrl)
                {
                    bool stepSuccess;
                    Console.WriteLine("Successfully loaded " + strUrl);



                    stepSuccess = StepOne(driver);
                    if (stepSuccess)
                    {
                        Console.WriteLine("Step one complete.");
                    }
                    else
                    {
                        Console.WriteLine("Unable to execute step one. Exiting the program...");
                        Environment.Exit(0);
                    }


                    stepSuccess = StepTwo(driver);
                    if (stepSuccess)
                    {
                        Console.WriteLine("Step two complete.");
                    }
                    else
                    {
                        Console.WriteLine("Unable to execute step two. Exiting the program...");
                        Environment.Exit(0);
                    }


                    stepSuccess = StepThree(driver);
                    if (stepSuccess)
                    {
                        Console.WriteLine("Step three complete.");
                    }
                    else
                    {
                        Console.WriteLine("Unable to execute step three. Exiting the program...");
                        Environment.Exit(0);
                    }

                    stepSuccess = StepFour(driver);
                    if (stepSuccess)
                    {
                        Console.WriteLine("Step four complete.");
                    }
                    else
                    {
                        Console.WriteLine("Unable to execute step four. Exiting the program...");
                        Environment.Exit(0);
                    }

                    stepSuccess = StepFive(driver);
                    if (stepSuccess)
                    {
                        Console.WriteLine("Step five complete.");
                    }
                    else
                    {
                        Console.WriteLine("Unable to execute step five. Exiting the program...");
                        Environment.Exit(0);
                    }

                    stepSuccess = StepSix(driver);
                    if (stepSuccess)
                    {
                        Console.WriteLine("Step Six complete.");
                    }
                    else
                    {
                        Console.WriteLine("Unable to execute step Six. Exiting the program...");
                        Environment.Exit(0);
                    }

                    stepSuccess = LastSteps(driver);
                    if (stepSuccess)
                    {
                        Console.WriteLine("Steps 7 - 10 complete.");
                        Console.WriteLine("All steps have been completed. Press any key to exit...");
                    }
                    else
                    {
                        Console.WriteLine("Unable to execute steps 7 - 10. Exiting the program...");
                        Environment.Exit(0);
                    }
                }                                   
                Console.ReadLine();
                driver.Close();
            }
            else
            {
                Console.WriteLine("The URL is not valid. Please exit and try again!");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Creates an URI based on string input. Returns tru if it matches either HTTP or HTTPS scheme.
        /// </summary>
        /// <param name="strURL"></param>
        /// <returns></returns>
        static bool ValidateURL(string strURL)
        {
            bool result = false;
            Uri uriResult;

            result = Uri.TryCreate(strURL, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;            
        }
        /// <summary>
        /// Accepts the alert dialog whenever it presents itself.
        /// </summary>
        /// <param name="driver"></param>
        static void AcceptDialog(IWebDriver driver)
        {
            bool dialogPresent = false;
            do
            {
                dialogPresent = CheckForDialog(driver);
            } while (!dialogPresent);


            IAlert alert;
            alert = driver.SwitchTo().Alert();
            alert.Accept();
        }
        /// <summary>
        /// Checks for the presence of an alert dialog.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Performs step one.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        static bool StepOne(IWebDriver driver)
        {
            bool result;
            IWebElement stepOneElement = driver.FindElement(By.XPath("//*[text()='Step 1.']"));
            if (stepOneElement != null)
            {
                Console.WriteLine("Executing Step One.");
                try
                {
                    stepOneElement.Click();
                    AcceptDialog(driver);
                    result = true;
                }
                catch
                {
                    result = false;
                }                
            }
            else
            {
                result = false;
            }        
            return result;
        }
        /// <summary>
        /// Performs step two.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        static bool StepTwo(IWebDriver driver)
        {
            bool result;
            IWebElement stepTwoElement = driver.FindElement(By.XPath("//*[text() ='Step 2.']"));
            if (stepTwoElement != null)
            {
                Console.WriteLine("Executing Step Two.");
                try
                {
                    stepTwoElement.SendKeys(Keys.Tab);
                    stepTwoElement.SendKeys(Keys.Tab);
                    AcceptDialog(driver);
                    result = true;
                }
                catch
                {
                    result = false;
                }                
            }
            else
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Performs step three.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        static bool StepThree(IWebDriver driver)
        {
            bool result;

            IWebElement stepThreeElement = driver.FindElement(By.XPath("//*[text() ='Step 3.']"));
            if (stepThreeElement != null)
            {
                Console.WriteLine("Executing Step Three.");
                try
                {
                    IWebElement optionElement = driver.FindElement(By.XPath("//*[@id='optionVal']"));
                    Console.WriteLine("Select Option " + optionElement.Text);
                    IWebElement optionElementNeeded = driver.FindElement(By.XPath("//*//*//input[" + optionElement.Text + "]"));
                    optionElementNeeded.Click();
                    AcceptDialog(driver);
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Performs step four.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        static bool StepFour(IWebDriver driver)
        {
            bool result;
            IWebElement stepFourElement = driver.FindElement(By.XPath("//*[text() ='Step 4.']"));
            if (stepFourElement != null)
            {
                Console.WriteLine("Executing Step Four.");
                try
                {
                    IWebElement selectElementNeeded = driver.FindElement(By.XPath("//*[@id='selectionVal']"));
                    string strSelectText = selectElementNeeded.Text;

                    IWebElement dropDownElement = driver.FindElement(By.XPath("//*//*//select"));
                    SelectElement selectElement = new SelectElement(dropDownElement);
                    if (selectElement != null)
                    {
                        Console.WriteLine("Select " + strSelectText);
                        selectElement.SelectByText(strSelectText);
                        AcceptDialog(driver);
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                catch
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }     
            
            return result;
        }
        /// <summary>
        /// Performs step five.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        static bool StepFive(IWebDriver driver)
        {
            bool result;
            IWebElement stepFiveElement = driver.FindElement(By.XPath("//*[text() ='Step 5.']"));
            if (stepFiveElement != null)
            {
                Console.WriteLine("Executing Step Five.");
                try
                {
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

                    AcceptDialog(driver);

                    formElement = driver.FindElement(By.Id("formResult"));
                    formResult = formElement.Text;
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Performs step six.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        static bool StepSix(IWebDriver driver)
        {
            bool result;
            IWebElement stepSixElement = driver.FindElement(By.XPath("//*[text() = 'Step 6.']"));
            if (stepSixElement != null)
            {
                Console.WriteLine("Executing Step Six.");
                try
                {
                    string lineNumber = driver.FindElement(By.Id("lineNum")).Text;
                    Console.WriteLine("Line number to insert into: " + lineNumber);

                    IWebElement textboxElement = driver.FindElement(By.XPath("//*[@id='inputTable']/tbody/tr[" + lineNumber + "]/td[2]/input"));
                    textboxElement.Clear();
                    textboxElement.SendKeys(formResult);
                    textboxElement.SendKeys(Keys.Tab);

                    AcceptDialog(driver);
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Performs step 7 - 10.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        static bool LastSteps(IWebDriver driver)
        {
            bool result = false;
            for (int i = 7; i < 11; i++)
            {
                try
                {
                    IWebElement stepElement = driver.FindElement(By.XPath("//*[text()= 'Step " + i + ".']"));
                    if (stepElement != null)
                    {
                        Console.WriteLine("Executing " + stepElement.Text);
                        stepElement.Click();
                        AcceptDialog(driver);
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                catch
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
