﻿namespace Projeto.CrossCutting
{
    public static class Mensagens
    {
        public const string CampoObrigatorio = "O campo {0} é obrigatório";
        public const string CampoFormato = "O campo {0} está com o formato inválido";
        public const string CampoComprimento = "O campo {0} deve ter no mínimo {2} e no máximo {1} caracteres.";
        public const string CampoSenhaEConfirmacao = "A senha e a confirmação da senha não estão idênticas.";

        public const string ValidacaoClienteCamposInvalidos = "Campos informados inválidos.";
        public const string ValidacaoClienteEmailDuplicado = "E-mail informado já cadastrado.";
        public const string ValidacaoClienteCpfDuplicado = "CPF informado já cadastrado.";
    }
}
