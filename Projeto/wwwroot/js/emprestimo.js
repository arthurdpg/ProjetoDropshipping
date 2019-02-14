$(function () {
    emprestimo.carregarTabelas();
    emprestimo.inicializar();
});

var emprestimo = {

    tabelaEmAndamento: null,

    tabelaFinalizados: null,

    seletores: {
        tabelaEmAndamento: "#tabelaEmprestimosEmAndamento",
        tabelaFinalizados: "#tabelaEmprestimosFinalizados",
        botaoNovo: "#btnCadastrarEmprestimo"
    },

    inicializar: function () {
        $(emprestimo.seletores.botaoNovo).on("click", emprestimo.eventos.onClickNovo);
    },

    eventos: {
        onClickExcluir: function (codigo) {
            bootbox.confirm({
                title: "Confirmação",
                message: "Deseja excluir o empréstimo selecionado? Essa ação não poderá ser desfeita.",
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
                        emprestimo.excluirEmprestimo(codigo);
                    }
                }
            });
        },
        onClickFinalizar: function (codigo) {
            bootbox.confirm({
                title: "Confirmação",
                message: "Deseja finalizar o empréstimo selecionado? Essa ação não poderá ser desfeita.",
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
                        emprestimo.finalizarEmprestimo(codigo);
                    }
                }
            });
        },
        onClickNovo: function () {
            emprestimo.novoEmprestimo();
        },
        onClickEditar: function (codigo) {
            emprestimo.editarEmprestimo(codigo);
        }
    },

    carregarTabelas: function () {
        emprestimo.tabelaEmAndamento = $(emprestimo.seletores.tabelaEmAndamento).dataTable({
            language: {
                url: 'lib/datatables/localisation/Portuguese-Brasil.json'
            },
            "searching": false,
            "ajax":
            {
                "url": "/Api/Emprestimo/GetEmprestimosEmAndamento",
                "type": "GET",
                "dataType": "JSON",
                "cache": false
            },
            "columns": [
                {
                    "sTitle": "Jogo", "data": "titulo", "mRender": function (objeto) {
                        return objeto.nome;
                    }
                },
                {
                    "sTitle": "Amigo", "data": "amigo", "mRender": function (objeto) {
                        return objeto.nome;
                    }
                },
                {
                    "sTitle": "Data do empréstimo", "data": "dataEmprestimo", "mRender": function (objeto) {
                        return emprestimo.formatarData(objeto);
                    }
                },
                {
                    "sTitle": "Ações", "data": "codigo", "mRender": function (codigo) {
                        return "<input type='button' value='Excluir' onclick='emprestimo.eventos.onClickExcluir(" + codigo + ")' /> &nbsp;" +
                            "<input type='button' value='Editar' onclick='emprestimo.eventos.onClickEditar(" + codigo + ")' /> &nbsp;" +
                            "<input type='button' value='Finalizar' onclick='emprestimo.eventos.onClickFinalizar(" + codigo + ")' />";
                    }
                }
            ]
        });

        emprestimo.tabelaFinalizados = $(emprestimo.seletores.tabelaFinalizados).dataTable({
            language: {
                url: 'lib/datatables/localisation/Portuguese-Brasil.json'
            },
            "searching": false,
            "ajax":
            {
                "url": "/Api/Emprestimo/GetEmprestimosFinalizados",
                "type": "GET",
                "dataType": "JSON",
                "cache": false
            },
            "columns": [
                {
                    "sTitle": "Jogo", "data": "titulo", "mRender": function (objeto) {
                        return objeto.nome;
                    }
                },
                {
                    "sTitle": "Amigo", "data": "amigo", "mRender": function (objeto) {
                        return objeto.nome;
                    }
                },
                {
                    "sTitle": "Data do empréstimo", "data": "dataEmprestimo", "mRender": function (objeto) {
                        return emprestimo.formatarData(objeto);
                    }
                },
                {
                    "sTitle": "Data da devolução", "data": "dataDevolucao", "mRender": function (objeto) {
                        return emprestimo.formatarData(objeto);
                    }
                }
            ]
        });
    },

    formatarData: function (valor) {
        var data = new Date(valor);
        return data.getDate() + "/" + (data.getMonth() + 1) + "/" + data.getFullYear();
    },

    redesenharTabela: function () {
        emprestimo.tabelaEmAndamento.api().ajax.reload();
        emprestimo.tabelaFinalizados.api().ajax.reload();
    },

    excluirEmprestimo: function (codigo) {
        $.ajax({
            url: '/Api/Emprestimo/Delete',
            type: 'DELETE',
            data: { codigo: codigo },
            success: function () {
                bootbox.alert("Empréstimo excluído com sucesso!", function () { emprestimo.redesenharTabela(); });
            }
        });
    },

    finalizarEmprestimo: function (codigo) {
        $.ajax({
            url: '/Api/Emprestimo/FinalizarEmprestimo',
            type: 'POST',
            data: { codigo: codigo },
            success: function (data) {
                if (data.operacaoConcluidaComSucesso) {
                    bootbox.alert("Empréstimo finalizado com sucesso!", function () { emprestimo.redesenharTabela(); });
                } else {
                    bootbox.alert(data.mensagem);
                }
            }
        });
    },

    novoEmprestimo: function () {
        modalEmprestimo.exibirModal();
    },

    editarEmprestimo: function (codigo) {
        $.ajax({
            url: '/Api/Emprestimo/' + codigo,
            type: 'GET',
            success: function (data) {
                modalEmprestimo.exibirModal(data);
            }
        });
    }
}

var modalEmprestimo =
    {
        seletores: {
            modal: "#modalEmprestimo",
            hddCodigo: "#hddCodigo",
            ddlAmigosEmprestimo: "#ddlAmigosEmprestimo",
            ddlTitulosEmprestimo: "#ddlTitulosEmprestimo",
            btnSalvar: "#btnSalvarEmprestimo",
            form: "#formModalEmprestimo"
        },

        inicializar: function () {
            //$(modalAmigo.seletores.form).validate({
            //    rules: {
            //        Nome: {
            //            required: true
            //        },
            //        Celular: {
            //            required: true,
            //            minlength: 15
            //        },
            //        Email: {
            //            email: true
            //        }
            //    }
            //});

            modalEmprestimo.limparCampos();

            var btnSalvar = $(modalEmprestimo.seletores.btnSalvar);
            btnSalvar.off("click");

            btnSalvar.on("click", function () {
                if (modalEmprestimo.validarForm()) {
                    modalEmprestimo.eventos.onClickEditarSalvar();
                }
            });
        },

        eventos: {
            onClickEditarSalvar: function () {
                $.ajax({
                    url: '/Api/Emprestimo/SalvarEmprestimo',
                    type: 'POST',
                    dataType: "JSON",
                    data: modalEmprestimo.recuperarObjetoPreenchido(),
                    success: function (data) {
                        modalEmprestimo.fecharModal();

                        if (data.operacaoConcluidaComSucesso) {
                            bootbox.alert("Empréstimo salvo com sucesso!", function () { emprestimo.redesenharTabela(); });
                        } else {
                            bootbox.alert(data.mensagem);
                        }
                    }
                });
            }
        },

        exibirModal: function (dados) {
            modalEmprestimo.inicializar();

            if (dados)
                modalEmprestimo.preencherCampos(dados);

            $(modalEmprestimo.seletores.modal).modal('show');
        },

        fecharModal: function () {
            $(modalEmprestimo.seletores.modal).modal('hide');
        },

        limparCampos: function () {
            $(modalEmprestimo.seletores.ddlAmigosEmprestimo + ' option:first').prop('selected', true);
            $(modalEmprestimo.seletores.ddlTitulosEmprestimo + ' option:first').prop('selected', true);
        },

        validarForm: function () {
            return true;
        },

        preencherCampos: function (emprestimo) {
            $(modalEmprestimo.seletores.hddCodigo).val(emprestimo.codigo);
            $(modalEmprestimo.seletores.ddlAmigosEmprestimo).val(emprestimo.amigo.codigo);
            $(modalEmprestimo.seletores.ddlTitulosEmprestimo).val(emprestimo.titulo.codigo);
        },

        recuperarObjetoPreenchido: function () {
            return {
                Codigo: $(modalEmprestimo.seletores.hddCodigo).val(),
                Amigo: {
                    Codigo: $(modalEmprestimo.seletores.ddlAmigosEmprestimo).val()
                },
                Titulo: {
                    Codigo: $(modalEmprestimo.seletores.ddlTitulosEmprestimo).val()
                }
            }
        }
    }