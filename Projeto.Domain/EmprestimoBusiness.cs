using System;
using System.Collections.Generic;
using Projeto.CrossCutting;
using Projeto.Data.Interfaces;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;

namespace Projeto.Domain
{
    public class EmprestimoBusiness : IEmprestimoBusiness
    {
        private readonly IEmprestimoData _emprestimoData;
        private readonly IAmigoBusiness _amigoBusiness;
        private readonly ITituloBusiness _tituloBusiness;

        public EmprestimoBusiness(IEmprestimoData emprestimoData, IAmigoBusiness amigoBusiness, ITituloBusiness tituloBusiness)
        {
            _emprestimoData = emprestimoData;
            _amigoBusiness = amigoBusiness;
            _tituloBusiness = tituloBusiness;
        }

        public Emprestimo Obter(int codigo)
        {
            return _emprestimoData.Obter(codigo);
        }

        public List<Emprestimo> ConsultarEmAndamento(string usuario)
        {
            return _emprestimoData.ConsultarEmAndamento(usuario);
        }

        public List<Emprestimo> ConsultarFinalizados(string usuario)
        {
            return _emprestimoData.ConsultarFinalizados(usuario);
        }

        public void Excluir(int codigo)
        {
            _emprestimoData.Excluir(codigo);
        }

        public void Finalizar(int codigo)
        {
            var emprestimo = Obter(codigo);

            if (emprestimo != null)
            {
                emprestimo.DataDevolucao = DateTime.Now;
                Salvar(emprestimo);
            }
        }

        public void Salvar(Emprestimo emprestimo)
        {
            if (emprestimo != null && emprestimo.Validar())
            {
                emprestimo.DataEmprestimo = emprestimo.DataEmprestimo == DateTime.MinValue
                    ? DateTime.Now
                    : emprestimo.DataEmprestimo;

                emprestimo.Amigo = _amigoBusiness.Obter(emprestimo.Amigo.Codigo);
                emprestimo.Titulo = _tituloBusiness.Obter(emprestimo.Titulo.Codigo);
                _emprestimoData.Salvar(emprestimo);
            }
            else
            {
                throw new ProjetoException("Dados inválidos ao salvar o empréstimo.");
            }
        }
    }
}
