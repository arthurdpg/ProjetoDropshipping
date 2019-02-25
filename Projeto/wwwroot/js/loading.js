$(function () {
    ajaxLoading.change();
});

var ajaxLoading = {
    change: function () {
        $(document).ajaxStart(function () {
            ajaxLoading.show();
        }).ajaxStop(function () {
            ajaxLoading.disable();
        });
    },
    show: function () {
        ajaxLoading.createDiv();
        ajaxLoading.createCss();
    },
    createDiv: function () {
        var div = '<div id="load"><div id="mask"></div><div id="ajax">';
        $('body').prepend(div + '<img src="../images/loading.gif"/></div></div>');
    },
    createCss: function () {
        $('#ajax').css({
            width: '128px',
            height: '128px',
            position: 'fixed',
            top: '50%',
            left: '50%',
            marginTop: '-64px',
            marginRight: '0px',
            marginBottom: '0px',
            marginLeft: '-64px',
            zIndex: 9999
        });

        $('#mask').css({
            backgroundColor: '#fff',
            opacity: '0.6',
            top: '0',
            left: '0',
            width: '100%',
            height: $(document).height(),
            position: 'fixed',
            zIndex: 9998
        });
    },
    disable: function () {
        $('#load').remove();
    }
}