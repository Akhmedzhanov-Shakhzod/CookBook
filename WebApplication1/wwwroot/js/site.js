// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('.multi-select').selectpicker();
    $('#btnPrint1').mousedown(function () {
        var restorePage = document.body.innerHTML;
        var printcontent = document.getElementById('ChangeOrderContent').innerHTML;
        document.body.innerHTML = printcontent;
        window.print();
        document.body.innerHTML = restorePage;
    });
    $('#btnPrint2').mousedown(function () {
        var restorePage = document.body.innerHTML;
        var printcontent = document.getElementById('CountdownContent').innerHTML;
        document.body.innerHTML = printcontent;
        window.print();
        document.body.innerHTML = restorePage;
    });
    $('#btnPrint3').mousedown(function () {
        var restorePage = document.body.innerHTML;
        var printcontent = document.getElementById('PrintDish').innerHTML;
        document.body.innerHTML = printcontent;
        window.print();
        document.body.innerHTML = restorePage;
    });
})





