using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Projeto.Domain.Test.Selenium.Utils;
using System;
using System.IO;
using Xunit;

namespace Projeto.Domain.Test.ViewTests
{
    public class LoginViewTest
    {
        private IConfiguration _configuration;

        public LoginViewTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");
            _configuration = builder.Build();
        }

        [Theory]
        [InlineData(Browser.Chrome)]
        public void TestarValidacaoCamposObrigatorios(Browser browser)
        {
            LoginView view = new LoginView(_configuration, browser);
            view.CarregarPagina();
            view.Submit();

            var existeLabelErroEmail = view.LabelErroEmailExiste();
            var existeLabelErroSenha = view.LabelErroSenhaExiste();
            var existeSumarioValidacao = view.SumarioValidacaoExiste();
            var quantidadeErro = view.ContarQuantidadeErro();

            view.Fechar();

            Assert.True(existeLabelErroEmail);
            Assert.True(existeLabelErroSenha);
            Assert.True(existeSumarioValidacao);
            Assert.True(quantidadeErro == 2);
        }

        [Theory]
        [InlineData(Browser.Chrome, "email@projeto.com.br")]
        public void TestarPreenchimentoEmail(Browser browser, string email)
        {
            LoginView view = new LoginView(_configuration, browser);
            view.CarregarPagina();
            view.PreencherEmail(email);
            view.Submit();

            var existeLabelErroEmail = view.LabelErroEmailExiste();
            var existeLabelErroSenha = view.LabelErroSenhaExiste();
            var existeSumarioValidacao = view.SumarioValidacaoExiste();
            var quantidadeErro = view.ContarQuantidadeErro();

            view.Fechar();

            Assert.False(existeLabelErroEmail);
            Assert.True(existeLabelErroSenha);
            Assert.True(existeSumarioValidacao);
            Assert.True(quantidadeErro == 1);
        }

        [Theory]
        [InlineData(Browser.Chrome, "senha123")]
        public void TestarPreenchimentoSenha(Browser browser, string senha)
        {
            LoginView view = new LoginView(_configuration, browser);
            view.CarregarPagina();
            view.PreencherSenha(senha);
            view.Submit();

            var existeLabelErroEmail = view.LabelErroEmailExiste();
            var existeLabelErroSenha = view.LabelErroSenhaExiste();
            var existeSumarioValidacao = view.SumarioValidacaoExiste();
            var quantidadeErro = view.ContarQuantidadeErro();

            view.Fechar();

            Assert.True(existeLabelErroEmail);
            Assert.False(existeLabelErroSenha);
            Assert.True(existeSumarioValidacao);
            Assert.True(quantidadeErro == 1);
        }

        [Theory]
        [InlineData(Browser.Chrome, "email@projeto.com.br", "senha123")]
        public void TestarTentativaLoginInvalida(Browser browser, string email, string senha)
        {
            LoginView view = new LoginView(_configuration, browser);
            view.CarregarPagina();
            view.PreencherEmail(email);
            view.PreencherSenha(senha);
            view.Submit();

            var existeLabelErroEmail = view.LabelErroEmailExiste();
            var existeLabelErroSenha = view.LabelErroSenhaExiste();
            var existeSumarioValidacao = view.SumarioValidacaoExiste();
            var quantidadeErro = view.ContarQuantidadeErro();

            view.Fechar();

            Assert.False(existeLabelErroEmail);
            Assert.False(existeLabelErroSenha);
            Assert.True(existeSumarioValidacao);
            Assert.True(quantidadeErro == 1);
        }

        [Theory]
        [InlineData(Browser.Chrome, "email", "senha123")]
        public void TestarEmailFormatoInvalido(Browser browser, string email, string senha)
        {
            LoginView view = new LoginView(_configuration, browser);
            view.CarregarPagina();
            view.PreencherEmail(email);
            view.PreencherSenha(senha);
            view.Submit();

            var existeLabelErroEmail = view.LabelErroEmailExiste();
            var existeLabelErroSenha = view.LabelErroSenhaExiste();
            var existeSumarioValidacao = view.SumarioValidacaoExiste();
            var quantidadeErro = view.ContarQuantidadeErro();

            view.Fechar();

            Assert.True(existeLabelErroEmail);
            Assert.False(existeLabelErroSenha);
            Assert.True(existeSumarioValidacao);
            Assert.True(quantidadeErro == 1);
        }
    }
}
