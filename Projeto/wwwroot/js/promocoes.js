$(function () {
    promocao.carregarTabela();
});

var promocao = {

    tabela: null,

    seletores: {
        tabela: "#tabelaPromocoes",
        botaoNovo: "#btnCadastrarAmigo"
    },

    eventos: {
        onClickInativar: function (codigo) {
            bootbox.confirm({
                title: "Confirmação",
                message: "Deseja inativar a promoção selecionada?",
                buttons: {
                    cancel: {
                        label: '<i class="fa fa-times"></i> Cancelar'
                    },
                    confirm: {
                        label: '<i class="fa fa-check"></i> Confirmar'
                    }
                },
                callback: function (result) {
                    if (result) {
                        promocao.inativarPromocao(codigo);
                    }
                }
            });
        }
    },

    carregarTabela: function () {
        promocao.tabela = $(promocao.seletores.tabela).dataTable({
            language: {
                url: 'lib/datatables/localisation/Portuguese-Brasil.json'
            },
            "searching": false,
            "ajax":
            {
                "url": "/Api/Promocao",
                "type": "GET",
                "dataType": "JSON",
                "cache": false
            },
            "columns": [
                { "sTitle": "Produto", "data": "produto.nome" },
                //{ "sTitle": "", "data": "email" },
                //{ "sTitle": "Quantidade", "data": "email" },
                //{
                //    "sTitle": "Data início", "data": "celular", "mRender": function (dado) {
                //        return dado;
                //    }
                //},
                //{
                //    "sTitle": "Data fim", "data": "celular", "mRender": function (dado) {
                //        return dado;
                //    }
                //},
                {
                    "sTitle": "Ações", "data": "codigo", "mRender": function (codigo) {
                        return "<input type='button' value='Inativar' onclick='promocao.eventos.onClickInativar(" + codigo + ")' />";
                    }
                }
            ]
        });
    },

    redesenharTabela: function () {
        promocao.tabela.api().ajax.reload();
    },

    inativarPromocao: function (codigo) {
        $.ajax({
            url: '/Api/Promocao/Inativar',
            type: 'PUT',
            data: { codigo: codigo },
            success: function (data) {
                if (data.operacaoConcluidaComSucesso) {
                    bootbox.alert("Promoção inativada com sucesso!", function () { promocao.redesenharTabela(); });
                } else {
                    bootbox.alert(data.mensagem);
                }
            }
        });
    }
};