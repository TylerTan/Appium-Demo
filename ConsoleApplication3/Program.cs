using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            int EnableDemoN = 2;
            
            #region Native Android App

            if (EnableDemoN == 1)
            {
                //Set DesiredCapabilities
                DesiredCapabilities nativeAppCapabilities = new DesiredCapabilities();
                //Set device name if using emulator
                nativeAppCapabilities.SetCapability(MobileCapabilityType.DeviceName, "192.168.56.101:5555");
                nativeAppCapabilities.SetCapability(MobileCapabilityType.PlatformName, "Android");
                //if android version<=4.1, should use Selendroid
                nativeAppCapabilities.SetCapability(MobileCapabilityType.AutomationName, "Appium");
                nativeAppCapabilities.SetCapability(MobileCapabilityType.PlatformVersion, "5.1");
                //if don't want to launch the app in device directly, need set the package and activity
                //otherwise, need specifies the absolute app folder
                nativeAppCapabilities.SetCapability(MobileCapabilityType.AppPackage, "com.android.calculator2");
                nativeAppCapabilities.SetCapability(MobileCapabilityType.AppActivity, "com.android.calculator2.Calculator");

                //Start the driver
                AppiumDriver<IWebElement> nativeAppdriver = new AndroidDriver<IWebElement>(
                    new Uri("http://127.0.0.1:4723/wd/hub"), nativeAppCapabilities);
                //Specifies the amount of time the driver should wait when searching for an
                //     element if it is not immediately present
                nativeAppdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));

                //Get 0~9 button element
                AndroidElement button_1 = (AndroidElement)nativeAppdriver.FindElement(By.Id("com.android.calculator2:id/digit_1"));
                AndroidElement button_2 = (AndroidElement)nativeAppdriver.FindElement(By.Id("com.android.calculator2:id/digit_2"));
                AndroidElement button_3 = (AndroidElement)nativeAppdriver.FindElement(By.Id("com.android.calculator2:id/digit_3"));
                AndroidElement button_4 = (AndroidElement)nativeAppdriver.FindElement(By.Id("com.android.calculator2:id/digit_4"));
                AndroidElement button_5 = (AndroidElement)nativeAppdriver.FindElement(By.Id("com.android.calculator2:id/digit_5"));
                AndroidElement button_6 = (AndroidElement)nativeAppdriver.FindElement(By.Id("com.android.calculator2:id/digit_6"));
                AndroidElement button_7 = (AndroidElement)nativeAppdriver.FindElement(By.Id("com.android.calculator2:id/digit_7"));
                AndroidElement button_8 = (AndroidElement)nativeAppdriver.FindElement(By.Id("com.android.calculator2:id/digit_8"));
                AndroidElement button_9 = (AndroidElement)nativeAppdriver.FindElement(By.Id("com.android.calculator2:id/digit_9"));
                AndroidElement button_0 = (AndroidElement)nativeAppdriver.FindElement(By.Id("com.android.calculator2:id/digit_0"));

                //Get +-*/ element
                AndroidElement button_Add = (AndroidElement)nativeAppdriver.FindElementByName("+");
                AndroidElement button_Sub = (AndroidElement)nativeAppdriver.FindElementByName("−");
                AndroidElement button_Mul = (AndroidElement)nativeAppdriver.FindElementByName("×");
                AndroidElement button_Div = (AndroidElement)nativeAppdriver.FindElementByName("÷");

                //Get result element
                IList<IWebElement> editElements = (IList<IWebElement>)nativeAppdriver.FindElements(By.ClassName("android.widget.EditText"));
                AndroidElement formula = (AndroidElement)editElements[0];
                AndroidElement result = (AndroidElement)editElements[1];

                //Get equals element
                AndroidElement equals = (AndroidElement)nativeAppdriver.FindElementByAccessibilityId("equals");

                //#######Test Case1 verify 1+1=2
                button_1.Click(); //press 1
                button_Add.Click();//press +
                button_1.Click();//press 1
                equals.Click();//press =
                string result_1 = formula.Text;//get result value
                if (result_1 != "2")
                {
                    Console.WriteLine("Test Case1 failed");
                }
                else
                {
                    Console.WriteLine("Test Case1 pass");
                }

                //#######Test Case2 verify 1÷3=0.333333333333333333
                button_1.Click();
                button_Div.Click();
                button_3.Click();
                equals.Click();
                string result_2 = formula.Text;
                if (result_2 != "0.333333333333333333")
                {
                    Console.WriteLine("Test Case1 failed");
                }
                else
                {
                    Console.WriteLine("Test Case1 pass");
                }

                nativeAppdriver.Dispose();
            }                      
            
            #endregion

            #region Hybird App

            if (EnableDemoN == 2)
            {
                //Set DesiredCapabilities
                DesiredCapabilities hybirdCapabilities = new DesiredCapabilities();
                //Set device name if using emulator
                hybirdCapabilities.SetCapability(MobileCapabilityType.DeviceName, "192.168.56.101:5555");
                hybirdCapabilities.SetCapability(MobileCapabilityType.PlatformName, "Android");
                hybirdCapabilities.SetCapability(MobileCapabilityType.AutomationName, "Appium");
                hybirdCapabilities.SetCapability(MobileCapabilityType.PlatformVersion, "5.1");
                //if don't want to launch the app in device directly, need set the package and activity
                //otherwise, need specifies the absolute app folder
                hybirdCapabilities.SetCapability(MobileCapabilityType.App, "D:\\AppFolder\\App.apk");
                                
                AppiumDriver<IWebElement> hybirdDriver = new AndroidDriver<IWebElement>(
                    new Uri("http://127.0.0.1:4723/wd/hub"), hybirdCapabilities);

                //Specifies the amount of time the driver should wait when searching for an
                //     element if it is not immediately present
                hybirdDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

                //Get all contexts of the app, check whether include webview
                IList<string> driverContexts = hybirdDriver.Contexts;
                
                Console.WriteLine("Out put all contexts of the driver");
                foreach(string s in driverContexts)
                {
                    Console.WriteLine(s);
                }
                //Set the context as "WEBVIEW*", then can locate the element as in browser. (can use the API of the Selenium)
                hybirdDriver.Context = driverContexts.Last();

                //Set the timeout again for webview driver.
                hybirdDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

                //Get the Login elements
                IWebElement emailElement = hybirdDriver.FindElement(By.Id("email"));
                IWebElement passwordElement = hybirdDriver.FindElement(By.Id("password"));
                IWebElement loginElement = hybirdDriver.FindElementById("login-button");
                
                //Set the login information
                emailElement.SendKeys("test@example.com");                
                passwordElement.SendKeys("testing");
                IWebElement emailtElementXpath = hybirdDriver.FindElementByXPath("//input[@ng-model=\"login.user.email\"]");
                
                loginElement.Click();
                

                //#######Test Case3
                
                IWebElement friends = (IWebElement)hybirdDriver.FindElement(By.XPath("//div[@class=\"nav-bar-block\" and @nav-bar=\"active\" ]//a[@ui-sref=\"p.main.friends\"]"));
                friends.Click();

                IWebElement search = (IWebElement)hybirdDriver.FindElementById("search");
                search.SendKeys("jack1");

                //Wait for 1 second for the search result
                System.Threading.Thread.Sleep(1000);

                IList<IWebElement> searchResultList = (IList<IWebElement>)hybirdDriver.FindElementsByXPath("//div[@class=\"friend-information auto-truncate ng-binding\"]");
                string searchedPerson1 = searchResultList[0].Text;

                if (searchedPerson1 != "jack1 rose1")
                {
                    Console.WriteLine("Test Case3 failed");
                }
                else
                {
                    Console.WriteLine("Test Case3 pass");
                }

                //#######Test Case4

                //Check the search result of "rose"
                search.Clear();
                search.SendKeys("rose");

                //Wait for 1 second for the search result
                System.Threading.Thread.Sleep(1000);

                try
                {
                    IList<IWebElement> searchResultList2 = (IList<IWebElement>)hybirdDriver.FindElementsByXPath("//div[@class=\"friend-information auto-truncate ng-binding\"]");
                    //IWebElement searchResult_2 = (IWebElement)hybirdDriver.FindElementByXPath("//div[@class=\"search-results\"]/div[3]//div[@class=\"friend-information auto-truncate ng-binding\"]");
                    string searchedPerson2 = searchResultList2[1].Text;

                    if (searchedPerson2 != "jack1 rose1")
                    {
                        Console.WriteLine("Test Case4 failed");
                    }
                    else
                    {
                        Console.WriteLine("Test Case4 pass");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Element can not be located!, information is:");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Test Case4 failed");
                }                      
                

                hybirdDriver.Context = driverContexts.First();

                hybirdDriver.Dispose();
            } 
            #endregion
            
            Console.ReadKey();
            
        }
    }
}
