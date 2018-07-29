using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Threading;          


namespace RJ
{
    class Program
    {
        const string from = "Praha";
        const string to = "Ostrava";
        static DateTime time = new DateTime(2018, 8, 22, 11, 48, 00); //yyyy, month, day, hour, min, sec
            
        static void Main(string[] args)
        {

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            IWebDriver driver = new ChromeDriver("/Users/ondra/Documents/Projects/Mine/RJ/RJ/bin/Debug/netcoreapp2.1/",chromeOptions);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            //driver.Scripts().ExecuteScript("some script");


            Bot regiobot = new Bot(driver, js);
            bool result = regiobot.Run(from,to,time);
        }
    }
}
