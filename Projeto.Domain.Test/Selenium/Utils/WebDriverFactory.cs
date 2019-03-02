using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.IO;
using System.Reflection;

namespace Projeto.Domain.Test.Selenium.Utils
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(Browser browser)
        {
            IWebDriver webDriver = null;
            var driversPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            switch (browser)
            {
                case Browser.Chrome:
                    webDriver = new ChromeDriver(driversPath);
                    break;
            }
            return webDriver;
        }
    }
}
