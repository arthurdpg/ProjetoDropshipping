$(function () {
    home.carregarTabela();
});

var home = {

    quantidadePromocao: 8,

    tamanhoPaginaPromocao: 4,

    seletores: {
        carrosel: "#carrosel",
        paginacao: ".carousel-indicators",
        tabela: "#tabelaPromocoes",
        botaoNovo: "#btnCadastrarAmigo",
    },

    eventos: {
        onClickAdicionar: function (codigo) {
            home.adicionarProduto(codigo);
        }
    },

    carregarTabela: function () {

        $.ajax({
            url: '/Api/Promocao/PromocoesRecentes',
            type: 'GET',
            data: { quantidade: home.quantidadePromocao },
            success: function (data) {
                if (data && data.data) {
                    home.montarTabela(data.data);
                } else {
                    bootbox.alert("Falha ao carregar promoções");
                }
            }
        });
    },

    adicionarProduto: function (codigo) {
        bootbox.alert("Item adicionado ao carrinho");
    },

    montarTabela: function (promocoes) {

        var pagina = "";
        var cssPagina = "item carousel-item active";

        for (var i = 1; i <= promocoes.length; i++) {
            if (i % home.tamanhoPaginaPromocao === 1) {
                pagina += "<div class='" + cssPagina + "'>";

                if (i === 1)
                    cssPagina = "item carousel-item";

                pagina += "<div class='row'>";
                pagina += home.montarItem(promocoes[i - 1]);
            }
            else if (i % home.tamanhoPaginaPromocao === 0) {
                pagina += home.montarItem(promocoes[i - 1]);
                pagina += "</div></div>";
                $(home.seletores.tabela).html(pagina);
            }
            else {
                pagina += home.montarItem(promocoes[i - 1]);
            }
        }

        $(home.seletores.paginacao).html(home.montarPaginacao(promocoes));
    },

    montarItem: function (promocao) {
        var item = "";

        item += "<div class='col-sm-3'>";
        item += "<div class='thumb-wrapper'>";
        item += "<div class='img-box'>";
        item += "<img src='" + promocao.caminhoImagemProduto + "' class='img-responsive img-fluid' alt='" + promocao.nomeProduto + "'>";
        item += "</div>";
        item += "<div class='thumb-content'>";
        item += "<h4>" + promocao.nomeProduto + "</h4>";
        item += "<p class='item-price'><strike>" + promocao.preco + "</strike> <span>" + promocao.precoComDesconto + "</span></p>";
        item += "<div class='star-rating'>";
        item += "<ul class='list-inline'>";
        item += home.montarAvaliacao(promocao);
        item += "</ul>";
        item += "</div>";
        item += "<a href='#' class='btn btn-primary' onclick='home.adicionarProduto(" + promocao.codigo + ");'>Comprar</a>";
        item += "</div>";
        item += "</div>";
        item += "</div>";

        return item;
    },

    montarPaginacao: function (promocoes) {

        var quantidadeReal = promocoes.length;
        var quantidadePaginas = Math.ceil(quantidadeReal / home.tamanhoPaginaPromocao);

        var paginacao = "<li data-target='" + home.seletores.carrosel + "' data-slide-to='0' class='active'></li>";

        for (var i = 1; i < quantidadePaginas; i++) {
            paginacao += "<li data-target='" + home.seletores.carrosel + "' data-slide-to='" + i + "'></li>";
        }

        return paginacao;
    },

    montarAvaliacao: function (promocao) {
        var avaliacoes = "";

        for (var i = 1; i <= 5; i++) {
            avaliacoes += home.recuperarEstrela(i, promocao.avaliacao);
        }

        return avaliacoes;
    },

    recuperarEstrela: function (posicao, avaliacao) {

        var cssClass = "fa fa-star";

        if (posicao > avaliacao)
            cssClass = "fa fa-star-o";

        if ((posicao - 0.5) === avaliacao)
            cssClass = "fa fa-star-half-o";

        return "<li class='list-inline-item'><i class='" + cssClass + "'></i></li>";
    }
};