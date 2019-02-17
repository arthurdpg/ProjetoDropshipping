$(function () {
    user.inicializar(); 
});

var user = {

    elementos: {
        txtCelular: $('#txtCellphone'),
        txtCPF: $('#txtCPF')
    },

    inicializar: function () {
        user.elementos.txtCelular.mask("+55 (99) 99999-9999");
        user.elementos.txtCPF.mask("999.999.999-99");
    }
};