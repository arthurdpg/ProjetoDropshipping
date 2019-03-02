using Moq;
using Projeto.CrossCutting;
using Projeto.Data.Interfaces;
using Projeto.Domain.Entities;
using System;
using Xunit;

namespace Projeto.Domain.Test
{
    public class ClienteTest
    {
        private Mock<IClienteData> _clienteData = new Mock<IClienteData>();

        [Fact]
        public void ClienteCadastradoComSucesso()
        {
            var negocio = ConfigurarAlvoTeste();
            _clienteData.Setup(c => c.ObterPorCpf(It.IsAny<string>())).Returns((Cliente)null);
            _clienteData.Setup(c => c.ObterPorLogin(It.IsAny<string>())).Returns((Cliente)null);
            _clienteData.Setup(c => c.Salvar(It.IsAny<Cliente>()));

            var resultado = negocio.Salvar(RecuperarObjetoTeste());

            _clienteData.Verify(c => c.ObterPorCpf(It.IsAny<string>()), Times.Once);
            _clienteData.Verify(c => c.ObterPorLogin(It.IsAny<string>()), Times.Once);
            _clienteData.Verify(c => c.Salvar(It.IsAny<Cliente>()), Times.Once);

            Assert.True(resultado.Sucesso);
            Assert.Equal(resultado.Mensagem, Mensagens.MensagemOperacaoSucesso);
        }

        [Fact]
        public void CadastrarClienteErroObterCpf()
        {
            var negocio = ConfigurarAlvoTeste();
            _clienteData.Setup(c => c.ObterPorCpf(It.IsAny<string>())).Throws(new Exception(""));
            _clienteData.Setup(c => c.ObterPorLogin(It.IsAny<string>())).Returns((Cliente)null);

            var resultado = negocio.Salvar(RecuperarObjetoTeste());

            _clienteData.Verify(c => c.ObterPorCpf(It.IsAny<string>()), Times.Once);
            _clienteData.Verify(c => c.ObterPorLogin(It.IsAny<string>()), Times.Never);
            _clienteData.Verify(c => c.Salvar(It.IsAny<Cliente>()), Times.Never);

            Assert.False(resultado.Sucesso);
            Assert.Equal(resultado.Mensagem, Mensagens.MensagemOperacaoErro);
        }

        [Fact]
        public void CadastrarClienteErroObterLogin()
        {
            var negocio = ConfigurarAlvoTeste();
            _clienteData.Setup(c => c.ObterPorCpf(It.IsAny<string>())).Returns((Cliente)null);
            _clienteData.Setup(c => c.ObterPorLogin(It.IsAny<string>())).Throws(new Exception(""));

            var resultado = negocio.Salvar(RecuperarObjetoTeste());

            _clienteData.Verify(c => c.ObterPorCpf(It.IsAny<string>()), Times.Once);
            _clienteData.Verify(c => c.ObterPorLogin(It.IsAny<string>()), Times.Once);
            _clienteData.Verify(c => c.Salvar(It.IsAny<Cliente>()), Times.Never);

            Assert.False(resultado.Sucesso);
            Assert.Equal(resultado.Mensagem, Mensagens.MensagemOperacaoErro);
        }

        [Fact]
        public void CadastrarClienteErroSalvar()
        {
            var negocio = ConfigurarAlvoTeste();
            _clienteData.Setup(c => c.ObterPorCpf(It.IsAny<string>())).Returns((Cliente)null);
            _clienteData.Setup(c => c.ObterPorLogin(It.IsAny<string>())).Returns((Cliente)null);
            _clienteData.Setup(c => c.Salvar(It.IsAny<Cliente>())).Throws(new Exception(""));

            var resultado = negocio.Salvar(RecuperarObjetoTeste());

            _clienteData.Verify(c => c.ObterPorCpf(It.IsAny<string>()), Times.Once);
            _clienteData.Verify(c => c.ObterPorLogin(It.IsAny<string>()), Times.Once);
            _clienteData.Verify(c => c.Salvar(It.IsAny<Cliente>()), Times.Once);

            Assert.False(resultado.Sucesso);
            Assert.Equal(resultado.Mensagem, Mensagens.MensagemOperacaoErro);
        }

        [Fact]
        public void ClienteNaoPodeSerCadastradoCpfJaExiste()
        {
            var negocio = ConfigurarAlvoTeste();
            _clienteData.Setup(c => c.ObterPorCpf(It.IsAny<string>())).Returns(new Cliente());
            _clienteData.Setup(c => c.ObterPorLogin(It.IsAny<string>())).Returns((Cliente)null);

            var resultado = negocio.Salvar(RecuperarObjetoTeste());

            _clienteData.Verify(c => c.ObterPorCpf(It.IsAny<string>()), Times.Once);
            _clienteData.Verify(c => c.ObterPorLogin(It.IsAny<string>()), Times.Never);
            _clienteData.Verify(c => c.Salvar(It.IsAny<Cliente>()), Times.Never);

            Assert.False(resultado.Sucesso);
            Assert.Equal(resultado.Mensagem, Mensagens.ValidacaoClienteCpfDuplicado);
        }

        [Fact]
        public void ClienteNaoPodeSerCadastradoEmailJaExiste()
        {
            var negocio = ConfigurarAlvoTeste();
            _clienteData.Setup(c => c.ObterPorCpf(It.IsAny<string>())).Returns((Cliente)null);
            _clienteData.Setup(c => c.ObterPorLogin(It.IsAny<string>())).Returns(new Cliente());

            var resultado = negocio.Salvar(RecuperarObjetoTeste());

            _clienteData.Verify(c => c.ObterPorCpf(It.IsAny<string>()), Times.Once);
            _clienteData.Verify(c => c.ObterPorLogin(It.IsAny<string>()), Times.Once);
            _clienteData.Verify(c => c.Salvar(It.IsAny<Cliente>()), Times.Never);

            Assert.False(resultado.Sucesso);
            Assert.Equal(resultado.Mensagem, Mensagens.ValidacaoClienteEmailDuplicado);
        }

        [Fact]
        public void ClienteNaoPodeSerCadastradoNomeNaoPreenchido()
        {
            var cliente = RecuperarObjetoTeste();
            cliente.Nome = string.Empty;

            ValidarCenarioCampoObrigatorio(cliente);
        }

        [Fact]
        public void ClienteNaoPodeSerCadastradoCpfNaoPreenchido()
        {
            var cliente = RecuperarObjetoTeste();
            cliente.Cpf = string.Empty;

            ValidarCenarioCampoObrigatorio(cliente);
        }

        [Fact]
        public void ClienteNaoPodeSerCadastradoCpfMenor()
        {
            var cliente = RecuperarObjetoTeste();
            cliente.Cpf = "692.519.852-3";

            ValidarCenarioCampoObrigatorio(cliente);
        }

        [Fact]
        public void ClienteNaoPodeSerCadastradoCpfMaior()
        {
            var cliente = RecuperarObjetoTeste();
            cliente.Cpf = "692.519.852-355";

            ValidarCenarioCampoObrigatorio(cliente);
        }

        [Fact]
        public void ClienteNaoPodeSerCadastradoCpfTodosNumerosIguais()
        {
            var cliente = RecuperarObjetoTeste();
            cliente.Cpf = "999.999.999-99";

            ValidarCenarioCampoObrigatorio(cliente);
        }

        [Fact]
        public void ClienteNaoPodeSerCadastradoCpfDigitoInvalido()
        {
            var cliente = RecuperarObjetoTeste();
            cliente.Cpf = "692.519.852-33";

            ValidarCenarioCampoObrigatorio(cliente);
        }

        [Fact]
        public void ClienteNaoPodeSerCadastradoEmailNaoPreenchido()
        {
            var cliente = RecuperarObjetoTeste();
            cliente.Email = string.Empty;

            ValidarCenarioCampoObrigatorio(cliente);
        }

        [Fact]
        public void ClienteNaoPodeSerCadastradoLoginNaoPreenchido()
        {
            var cliente = RecuperarObjetoTeste();
            cliente.Login = string.Empty;

            ValidarCenarioCampoObrigatorio(cliente);
        }

        [Fact]
        public void ClienteNaoPodeSerCadastradoCelularNaoPreenchido()
        {
            var cliente = RecuperarObjetoTeste();
            cliente.Celular = string.Empty;

            ValidarCenarioCampoObrigatorio(cliente);
        }

        private void ValidarCenarioCampoObrigatorio(Cliente cliente)
        {
            var negocio = ConfigurarAlvoTeste();
            _clienteData.Setup(c => c.ObterPorCpf(It.IsAny<string>())).Returns((Cliente)null);
            _clienteData.Setup(c => c.ObterPorLogin(It.IsAny<string>())).Returns((Cliente)null);

            var resultado = negocio.Salvar(cliente);

            _clienteData.Verify(c => c.ObterPorCpf(It.IsAny<string>()), Times.Never);
            _clienteData.Verify(c => c.ObterPorLogin(It.IsAny<string>()), Times.Never);
            _clienteData.Verify(c => c.Salvar(It.IsAny<Cliente>()), Times.Never);

            Assert.False(resultado.Sucesso);
            Assert.Equal(resultado.Mensagem, Mensagens.ValidacaoClienteCamposInvalidos);
        }

        private ClienteBusiness ConfigurarAlvoTeste()
        {
            return new ClienteBusiness(_clienteData.Object);
        }

        private Cliente RecuperarObjetoTeste()
        {
            return new Cliente
            {
                Codigo = 1,
                Celular = "+55 (99) 99999-9999",
                Cpf = "692.519.852-35",
                Email = "email@projeto.com.br",
                Login = "email@projeto.com.br",
                Nome = "Cliente"
            };
        }
    }
}
