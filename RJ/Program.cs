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
        static DateTime time = new DateTime(2018, 7, 20, 7, 48, 00); //yyyy, month, day, hour, min, sec
            
        static void Main(string[] args)
        {

            EmailSender.SendNotification();
            IWebDriver driver = new ChromeDriver("/Users/ondra/Documents/Projects/Mine/RJ/RJ/bin/Debug/netcoreapp2.1/");
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            //driver.Scripts().ExecuteScript("some script");



            driver.Navigate().GoToUrl("https://jizdenky.regiojet.cz/m/?0");
            IWebElement fromStation = driver.FindElement(By.Name("fromStation"));
            //fromStation.SendKeys("Praha");
            var selectFromStation = new SelectElement(fromStation);
            selectFromStation.SelectByText(from);

            IWebElement toStation = driver.FindElement(By.Name("toStation"));
            //toStation.SendKeys("Ostrava");
            var selectToStation = new SelectElement(toStation);
            selectToStation.SelectByText(to);

            IWebElement departureTime = driver.FindElement(By.Id("departure"));
            departureTime.Click();

            IWebElement monthYear = driver.FindElement(By.ClassName("ds_head"));
            var preParse = monthYear.Text;
            //Console.WriteLine(preParse);


            var monthNavig = driver.FindElements(By.ClassName("ds_headnav")).ToList();
            //var nextMonthNav = driver.FindElement(By.ClassName("ds_headnav right"));
            //var prevMonthNav = driver.FindElement(By.ClassName("ds_headnav left"));


            var day = driver.FindElement(By.Id("ds_day_date_" + time.Day));
            day.Click();
            //wait
            Thread.Sleep(5000);
            fromStation.Submit();
            Console.WriteLine("Initialization Completed!");
            bool full = true;
            do
            {
                var myLine = driver.FindElements(By.ClassName("departure")).Where(x=>x.Text == time.Hour+":"+time.Minute).SingleOrDefault();
                var free = myLine.FindElement(By.XPath("../span[@class='free']")).Text;
                Int32.TryParse(free, out var freeSeats);
                if(freeSeats > 0){
                    Console.WriteLine("Line from "+from+" to "+to+" in "+time.Hour+":"+time.Minute+" "+time.Day+"."+time.Month+"."+time.Year+" has "+freeSeats+ " free seats");

                }
                Thread.Sleep(30000);
                driver.Navigate().Refresh();
                Console.Write(".");
            } while (full);
        }
    }
}
