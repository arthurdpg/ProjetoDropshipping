﻿using Projeto.CrossCutting;
using Projeto.Data.Interfaces;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;
using System;

namespace Projeto.Domain
{
    public class ClienteBusiness : IClienteBusiness
    {
        private readonly IClienteData _clienteData;

        public ClienteBusiness(IClienteData clienteData)
        {
            _clienteData = clienteData;
        }

        public Cliente ObterPorLogin(string login)
        {
            return _clienteData.ObterPorLogin(login);
        }

        public Cliente ObterPorCpf(string cpf)
        {
            return _clienteData.ObterPorCpf(cpf);
        }

        public ResultadoDto Salvar(Cliente cliente)
        {
            try
            {
                var resultado = ValidarCadastroCliente(cliente);

                if (resultado.Sucesso)
                {
                    _clienteData.Salvar(cliente);
                    return new ResultadoDto(true, Mensagens.MensagemOperacaoSucesso);
                }
                return resultado;
            }
            catch (Exception)
            {
                return new ResultadoDto(false, Mensagens.MensagemOperacaoErro);
            }
        }

        public ResultadoDto ValidarCadastroCliente(Cliente novoCliente)
        {
            var valido = novoCliente.Validar();

            if (!valido)
                return new ResultadoDto(false, Mensagens.ValidacaoClienteCamposInvalidos);

            if (ObterPorCpf(novoCliente.Cpf) != null)
                return new ResultadoDto(false, Mensagens.ValidacaoClienteCpfDuplicado);

            if (ObterPorLogin(novoCliente.Login) != null)
                return new ResultadoDto(false, Mensagens.ValidacaoClienteEmailDuplicado);

            return new ResultadoDto(true);
        }
    }
}
