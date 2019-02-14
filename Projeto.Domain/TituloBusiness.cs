using System.Collections.Generic;
using Projeto.CrossCutting;
using Projeto.Data.Interfaces;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;

namespace Projeto.Domain
{
    public class TituloBusiness : ITituloBusiness
    {
        private readonly ITituloData _tituloData;
        private readonly IConsoleBusiness _consoleBusiness;
        private readonly IEmprestimoAmigoTituloBusiness _emprestimoBusiness;

        public TituloBusiness(ITituloData tituloData, IConsoleBusiness consoleBusiness, IEmprestimoAmigoTituloBusiness emprestimoBusiness)
        {
            _tituloData = tituloData;
            _consoleBusiness = consoleBusiness;
            _emprestimoBusiness = emprestimoBusiness;
        }

        public Titulo Obter(int codigo)
        {
            return _tituloData.Obter(codigo);
        }

        public List<Titulo> Consultar(string usuario)
        {
            return _tituloData.Consultar(usuario);
        }

        public void Excluir(int codigo)
        {
            if (!_emprestimoBusiness.VerificarTituloPossuiEmprestimo(codigo))
            {
                _tituloData.Excluir(codigo);
            }
            else
            {
                throw new ProjetoException("Não é possível excluir o jogo, pois ele possui um empréstimo associado.");
            }
        }

        public void Salvar(Titulo titulo)
        {
            if (titulo != null && titulo.Validar())
            {
                titulo.Console = _consoleBusiness.Obter(titulo.Console.Codigo);
                _tituloData.Salvar(titulo);
            }
            else
            {
                throw new ProjetoException("Dados inválidos ao salvar o jogo.");
            }
        }
    }
}
