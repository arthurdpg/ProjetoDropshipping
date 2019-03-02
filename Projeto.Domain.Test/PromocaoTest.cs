using Moq;
using Xunit;
using Projeto.Domain.Entities;
using Projeto.Data.Interfaces;
using Projeto.CrossCutting;
using System;

namespace Projeto.Domain.Test
{
    public class PromocaoTest
    {
        private Mock<IPromocaoData> _promocaoData = new Mock<IPromocaoData>();

        [Fact]
        public void ValidarFinalizacaoPromocaoSucesso()
        {
            var negocio = ConfigurarAlvoTeste();
            _promocaoData.Setup(p => p.Obter(It.IsAny<int>())).Returns(new Promocao());
            _promocaoData.Setup(p => p.Salvar(It.IsAny<Promocao>()));

            var resultado = negocio.Finalizar(1);

            _promocaoData.Verify(p => p.Obter(It.IsAny<int>()), Times.Once);
            _promocaoData.Verify(p => p.Salvar(It.IsAny<Promocao>()), Times.Once);


            Assert.True(resultado.Sucesso);
            Assert.Equal(resultado.Mensagem, Mensagens.MensagemOperacaoSucesso);
        }

        [Fact]
        public void ValidarFinalizacaoPromocaoNaoEncontrado()
        {
            var negocio = ConfigurarAlvoTeste();
            _promocaoData.Setup(p => p.Obter(It.IsAny<int>())).Returns((Promocao)null);
            _promocaoData.Setup(p => p.Salvar(It.IsAny<Promocao>()));

            var resultado = negocio.Finalizar(1);

            _promocaoData.Verify(p => p.Obter(It.IsAny<int>()), Times.Once);
            _promocaoData.Verify(p => p.Salvar(It.IsAny<Promocao>()), Times.Never);

            Assert.False(resultado.Sucesso);
            Assert.Equal(resultado.Mensagem, Mensagens.MensagemOperacaoRegistroNaoEncontrado);
        }

        [Fact]
        public void ValidarFinalizacaoPromocaoErroObter()
        {
            var negocio = ConfigurarAlvoTeste();
            _promocaoData.Setup(p => p.Obter(It.IsAny<int>())).Throws(new Exception(""));
            _promocaoData.Setup(p => p.Salvar(It.IsAny<Promocao>()));

            var resultado = negocio.Finalizar(1);

            _promocaoData.Verify(p => p.Obter(It.IsAny<int>()), Times.Once);
            _promocaoData.Verify(p => p.Salvar(It.IsAny<Promocao>()), Times.Never);

            Assert.False(resultado.Sucesso);
            Assert.Equal(resultado.Mensagem, Mensagens.MensagemOperacaoErro);
        }

        [Fact]
        public void ValidarFinalizacaoPromocaoErroSalvar()
        {
            var negocio = ConfigurarAlvoTeste();
            _promocaoData.Setup(p => p.Obter(It.IsAny<int>())).Returns(new Promocao());
            _promocaoData.Setup(p => p.Salvar(It.IsAny<Promocao>())).Throws(new Exception(""));

            var resultado = negocio.Finalizar(1);

            _promocaoData.Verify(p => p.Obter(It.IsAny<int>()), Times.Once);
            _promocaoData.Verify(p => p.Salvar(It.IsAny<Promocao>()), Times.Once);

            Assert.False(resultado.Sucesso);
            Assert.Equal(resultado.Mensagem, Mensagens.MensagemOperacaoErro);
        }

        private PromocaoBusiness ConfigurarAlvoTeste()
        {
            return new PromocaoBusiness(_promocaoData.Object);
        }
    }
}
