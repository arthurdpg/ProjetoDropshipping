﻿$(function () {
    promocao.carregarTabela();
});

var promocao = {

    tabela: null,

    seletores: {
        tabela: "#tabelaPromocoes",
        botaoNovo: "#btnCadastrarAmigo"
    },

    eventos: {
        onClickFinalizar: function (codigo) {
            bootbox.confirm({
                title: "Confirmação",
                message: "Deseja finalizar a promoção selecionada?",
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
                        promocao.finalizarPromocao(codigo);
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
                "url": "/Api/Promocao/Promocoes",
                "type": "GET",
                "dataType": "JSON",
                "cache": false
            },
            "columns": [
                { "sTitle": "Produto", "data": "nomeProduto", "sWidth": "35%" },
                {
                    "sTitle": "Imagem", "bSortable": false, "sWidth": "15%", "mRender": function (valor, tipo, objeto) {
                        return "<img src='" + objeto.caminhoImagemProduto + "' alt='" + objeto.nomeProduto + "' height='150' width='125'>";
                    }
                },
                { "sTitle": "Data início", "data": "dataInicio", "sWidth": "10%" },
                { "sTitle": "Data fim", "data": "dataFim", "sWidth": "10%" },
                { "sTitle": "Desconto", "data": "desconto", "sWidth": "5%" },
                {
                    "sTitle": "Ações", "bSortable": false, "sWidth": "5%", "mRender": function (valor, tipo, objeto) {
                        return !objeto.finalizada ? "<input type='button' value='Finalizar' onclick='promocao.eventos.onClickFinalizar(" + objeto.codigo + ")' />" : "";
                    }
                }
            ]
        });
    },

    redesenharTabela: function () {
        promocao.tabela.api().ajax.reload();
    },

    finalizarPromocao: function (codigo) {
        $.ajax({
            url: '/Api/Promocao/Finalizar',
            type: 'PUT',
            data: { codigo: codigo },
            success: function (data) {
                if (data.operacaoConcluidaComSucesso) {
                    bootbox.alert(data.mensagem, function () { promocao.redesenharTabela(); });
                } else {
                    bootbox.alert(data.mensagem);
                }
            }
        });
    }
};