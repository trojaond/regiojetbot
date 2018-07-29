using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Collections.Generic;
using System.Threading;          


namespace RJ
{
    public class Bot
    {
        IWebDriver driver;
        IJavaScriptExecutor js;

        IWebElement fromStation;
        IWebElement toStation;
        IWebElement departureTime;
        IWebElement monthYear;


        IWebElement monthPrev;
        IWebElement monthNext;

        public Bot(IWebDriver driver, IJavaScriptExecutor js  )
        {
            this.driver = driver;
            this.js = js;
        }

        public bool Run(string from, string destination, DateTime date){

            driver.Navigate().GoToUrl("https://jizdenky.regiojet.cz/m/?0");
            fromStation = driver.FindElement(By.Name("fromStation"));
            var selectFromStation = new SelectElement(fromStation);
            selectFromStation.SelectByText(from);

            toStation = driver.FindElement(By.Name("toStation"));
            var selectToStation = new SelectElement(toStation);
            selectToStation.SelectByText(destination);

            departureTime = driver.FindElement(By.Id("departure"));
            departureTime.Click();




            var monthNavig = driver.FindElements(By.ClassName("ds_headnav"));
            monthPrev = monthNavig.First();
            monthNext = monthNavig.Last();

            DateDiscovery(date);

            Thread.Sleep(2000);

            fromStation.Submit();
            Console.WriteLine("Initialization Completed!");
            bool full = true;
            do
            {
                var myLine = driver.FindElements(By.ClassName("departure")).Where(x => x.Text == date.Hour + ":" + date.Minute).SingleOrDefault();
                var free = myLine.FindElement(By.XPath("../span[@class='free']")).Text;
                Int32.TryParse(free, out var freeSeats);
                if (freeSeats > 0)
                {
                    Console.WriteLine("Line from " + from + " to " + destination + " in " + date.Hour + ":" + date.Minute + " " + date.Day + "." + date.Month + "." + date.Year + " has " + freeSeats + " free seats");
                    EmailSender.SendNotification("<b>Uvolnena jizdenka</b>", "trojaon@gmail.com");
                    full = false;
                    break;
                }
                Thread.Sleep(30000);
                driver.Navigate().Refresh();
                Console.Write(".");
            } while (full);
            Console.WriteLine("Sucessfull");

            return !full;
        }

        private bool DateDiscovery(DateTime date){
            List<string> months = new List<string>{"Leden", "Únor", "Březen", "Duben", "Květen","Červen", "Červenec", "Srpen", "Září", "Říjen", "Listopad", "Prosinec"}; 
            monthYear = driver.FindElement(By.ClassName("ds_head"));
            //var parsed = monthYear.Text.Split(null);
            bool searching = true;
            do {
                var monthNavig = driver.FindElements(By.ClassName("ds_headnav"));
                monthPrev = monthNavig.First();
                monthNext = monthNavig.Last(); 
                monthYear = driver.FindElement(By.ClassName("ds_head"));
                var parsed = monthYear.Text.Split(null);
                if(date.Year.ToString() != parsed[1]){
                    monthNext.Click();
                }else if(parsed[0] != months[date.Month - 1]){
                    monthNext.Click();
                }else{
                    searching = false;
                }

            }
            while (searching);

                Console.WriteLine("Date matched");
                var day = driver.FindElement(By.Id("ds_day_date_" + date.Day));
                day.Click();
            return !searching;

        }

    }
}
