/*global $*/

/* Nice Scroll
===============================*/
$(document).ready(function () {
    
    "use strict";
    
	$("html").niceScroll({
        scrollspeed: 60,
        mousescrollstep: 35,
        cursorwidth: 5,
        cursorcolor: '#f1ee8b',
        cursorborder: 'none',
        background: 'rgba(255,255,255,0.3)',
        cursorborderradius: 3,
        autohidemode: false,
        cursoropacitymin: 0.1,
        cursoropacitymax: 1,
        zindex: "999",
        horizrailenabled: false
	});
    $(".profile-tabs-cont .personal-info-form .edit-btn").click(function(){
        $(".personal-info-form").addClass("editable");
        $(".personal-info-form input").removeAttr("disabled");
    });
   
});

$('.owl-carousel').owlCarousel({
    rtl: true,
    loop: false,
    margin: 10,
    nav: false,
    autoplay: false,
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 3
        },
        1000: {
            items: 4
        }
    }
});


/* Model 
========================== */
$('#myModal').on('shown.bs.modal', function () {
    
    "use strict";
    
    $('#myInput').trigger('focus');
});

$("#register-btn").click(function () {
    
    "use strict";
    
    $(".flip").css("transform", "rotateY(0deg)");
    $(".absolute-layer").css("width", "100%");
    $(".flip > div").css("width", "52%");
});

$("#login-btn").click(function () {
    
    "use strict";
    $(".flip").css("transform", "rotateY(180deg)");
    $(".absolute-layer").delay(1000).queue(function () {
        $(".absolute-layer").css("width", "54%");
        $(".flip > div").css("width", "100%");
    });
});


$(function () {
    $('.date input').click(function(event){
       $(this).parent('.date').data("DateTimePicker").show();
    });
var bindDatePicker = function() {
    $(".date").datetimepicker({
        locale: 'ar-sa',
        format:'YYYY-MM-DD',
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar",
            up: "fa fa-arrow-up",
            down: "fa fa-arrow-down",
            previous: 'fa fa-angle-right',
            next: 'fa fa-angle-left'
        }
    }).find('input:first').on("blur",function () {
        // check if the date is correct. We can accept dd-mm-yyyy and yyyy-mm-dd.
        // update the format if it's yyyy-mm-dd
        var date = parseDate($(this).val());

        if (! isValidDate(date)) {
            //create date based on momentjs (we have that)
            date = moment().format('DD-MM-YYYY');
        }

        $(this).val(date);
    });
}
var isValidDate = function(value, format) {
    format = format || false;
    // lets parse the date to the best of our knowledge
    if (format) {
        value = parseDate(value);
    }

    var timestamp = Date.parse(value);

    return isNaN(timestamp) == false;
}

var parseDate = function(value) {
    var m = value.match(/^(\d{1,2})(\/|-)?(\d{1,2})(\/|-)?(\d{4})$/);
    if (m)
        value = m[5] + '-' + ("00" + m[3]).slice(-2) + '-' + ("00" + m[1]).slice(-2);

    return value;
}


bindDatePicker();
});




/*Main Slider
===============================*/
$(document).ready(function () {
	'use strict';
    $('#revolution-slider').revolution({
        dottedOverlay: "none",
        delay: 9000,
        startwidth: 1150,
        startheight: 700,
        hideThumbs: 200,
        thumbWidth: 100,
        thumbHeight: 50,
        thumbAmount: 2,
        simplifyAll: "off",
        navigationType: false,
        hide_onleave: false,
        navigationStyle: "preview4",
        touchenabled: "on",
        onHoverStop: "on",
        nextSlideOnWindowFocus: "off",
        swipe_threshold: 75,
        swipe_min_touches: 1,
        drag_block_vertical: false,
        parallax: "mouse",
        parallaxBgFreeze: "off",
        parallaxLevels: [5, 10, 15, 20, 25, 30, 35, 40, 45, 50],
        keyboardNavigation: "off",
        navigationArrows: "off",
        shadow: 0,
        fullWidth: "on",
        fullScreen: "off",
        spinner: "spinner0",
        stopLoop: "off",
        stopAfterLoops: -1,
        stopAtSlide: -1,
        shuffle: "off",
        autoHeight: "off",
        forceFullWidth: "off",
        hideThumbsOnMobile: "off",
        hideNavDelayOnMobile: 1500,
        hideSliderAtLimit: 0,
        hideCaptionAtLimit: 0,
        hideAllCaptionAtLilmit: 0,
        startWithSlide: 0,
        isJoomla: false
        
    });
});

$("#range").ionRangeSlider({
    hide_min_max: true,
    keyboard: true,
    min: 0,
    max: 150,
    from: 30,
    to: 100,
    type: 'double',
    step: 1,
    prefix: "",
    grid: true
});

/* Check box modal */
$('input').iCheck({
    checkboxClass: 'icheckbox_square-grey',
    radioClass: 'iradio_square-grey'
});

$(document).ready(function ($) {
    $('#Img_carousel').sliderPro({
        width: 960,
        height: 500,
        fade: true,
        arrows: true,
        buttons: false,
        fullScreen: false,
        smallSize: 500,
        startSlide: 0,
        mediumSize: 1000,
        largeSize: 3000,
        thumbnailArrows: true,
        autoplay: false
    });
});

$('input.date-pick').datepicker();

/* Input incrementer*/
$(".numbers-row").append('<div class="inc button_inc">+</div><div class="dec button_inc">-</div>');
$(".button_inc").on("click", function () {

    var $button = $(this);
    var oldValue = $button.parent().find("input").val();

    if ($button.text() == "+") {
        var newVal = parseFloat(oldValue) + 1;
    } else {
        // Don't allow decrementing below zero
        if (oldValue > 1) {
            var newVal = parseFloat(oldValue) - 1;
        } else {
            newVal = 0;
        }
    }
    $button.parent().find("input").val(newVal);
});