
$(document).ready(function () {
    
    "use strict";

    $(".choose3").on("click", function () {
        $(".content-test").show();
    });

    $("#qty3").on("keydown", function () {
        $(".choose3 p #adu").text($("#qty3").val());
    });

    $("#roomIn").on("change", function () {
        $(".choose3 p #room").text($("#roomIn").val());
    });

    $("#chilIn").on("change", function () {
        $(".choose3 p #chil").text($("#chilIn").val());
    });

    

    // $('#toptitle').text(function(i, oldText) {
    //     return oldText === 'Profil' ? 'New word' : oldText;
    // });

    
    
});