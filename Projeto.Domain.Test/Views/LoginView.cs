using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Projeto.Domain.Test.Selenium.Utils;
using System;

namespace Projeto.Domain.Test.ViewTests
{
    public class LoginView
    {
        private IConfiguration _configuration;
        private Browser _browser;
        private IWebDriver _driver;

        public LoginView(IConfiguration configuration, Browser browser)
        {
            _configuration = configuration;
            _browser = browser;

            _driver = WebDriverFactory.CreateWebDriver(browser);
        }
        public void CarregarPagina()
        {
            _driver.LoadPage(TimeSpan.FromSeconds(20), _configuration.GetSection("Selenium:UrlLogin").Value);
        }

        public void PreencherEmail(string valor)
        {
            _driver.SetText(By.Name("Email"), valor);
        }

        public void PreencherSenha(string valor)
        {
            _driver.SetText(By.Name("Password"), valor);
        }

        public bool LabelErroEmailExiste()
        {
            return ElementoPorIdExiste("Email-error");
        }

        public bool LabelErroSenhaExiste()
        {
            return ElementoPorIdExiste("Password-error");
        }

        public bool SumarioValidacaoExiste()
        {
            return ElementoPorClasseExiste("validation-summary-errors");
        }

        public int ContarQuantidadeErro()
        {
            try
            {
                return _driver.FindElement(By.ClassName("validation-summary-errors"))
                .FindElement(By.TagName("ul"))
                .FindElements(By.TagName("li")).Count;
            }
            catch
            {
                return 0;
            }
            
        }

        public void Submit()
        {
            _driver.Submit(By.Id("btnLogin"));
        }

        public void Fechar()
        {
            _driver.Quit();
            _driver = null;
        }

        private bool ElementoPorIdExiste(string id)
        {
            try
            {
                return _driver.FindElement(By.Id(id)) != null;
            }
            catch
            {
                return false;
            }
        }

        private bool ElementoPorClasseExiste(string classe)
        {
            try
            {
                return _driver.FindElement(By.ClassName(classe)) != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
