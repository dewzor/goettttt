$(document).ready(function () {
    
    $('#downBtn').click(function () {

        $('html, body').animate({
            scrollTop: $("#scrollbox").offset().top
        }, 1000);

    })




});