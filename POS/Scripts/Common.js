var ajaxCount = 0;
$(document).ready(function () {
    $(document).ajaxStart(function () {
        $(".loader_bg").css('display', 'block');
    });
    
    $(document).ajaxSuccess(function () {
        if (ajaxCount == 0)
        {
            $(".loader_bg").css('display', 'none');
        }
    });
    $(document).ajaxError(function () {
        $(".loader_bg").css('display', 'none');
    });
    $(window).load(function () {
        
        $('input[type=text]').each(function () {
            if ($(this).val().length>0) {
                $(this).removeClass("NotFilled");
                $(this).addClass("Filled");
            }
            else {
                $(this).removeClass("Filled");
                $(this).addClass("NotFilled");
            }

        });

        $('input[type=password]').each(function () {
            if ($(this).val().length>0) {
                $(this).removeClass("NotFilled");
                $(this).addClass("Filled");
            }
            else {
                $(this).removeClass("Filled");
                $(this).addClass("NotFilled");
            }
        }); 
        
    });
    $('input[type=text], input[type=password], input[type=datetime]').keypress(function () {
        if ($(this).val().length > 0) {
            $(this).removeClass("NotFilled");
            $(this).addClass("Filled");
        }
        else {
            $(this).removeClass("Filled");
            $(this).addClass("NotFilled");
        }

    });
    $('input[type=text], input[type=password],input[type=datetime]').focusout(function () {
        if ($(this).val().length > 0) {
            $(this).removeClass("NotFilled");
            $(this).addClass("Filled");
        }
        else {
            $(this).removeClass("Filled");
            $(this).addClass("NotFilled");
        }

    });
    $("select").on('change', function () {
        $(this).parent().parent().parent().find(".error-msg").empty();
    });

    $("input,textarea,select,.selectcon").hover(function () {
        var html = $(this).parent().find("span.error-msg").text();
        if (html.length > 0) {
            $(this).parent().find("span.error-msg").css("visibility", "visible");
        }
    },
    function () {
        $(this).parent().find("span.error-msg").css("visibility", "hidden");
    }
    );

    $(".TelDropdown  Select").change(function () {
        $($(this).parent().parent().find('input'))[1].focus()
    });

    $('form').submit(function ()
    {
        debugger;
        $(this).find('.field-validation-error').each(function ()
        {
            ShowMessage("warning", $(this).find('span').html());
        });
        if (!$(this).valid()) {
            return false;
        }
    });

    $(".number").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });
    
});
function PrintDoc(){
    var toPrint = document.getElementById('quotation');
    var popupWin = window.open('', '_blank', 'fullscreen=yes,location=no,left=200px');
    popupWin.document.open();
    popupWin.document.write('<html><title>::Print Preview::</title><link rel="stylesheet" type="text/css" href="/Content/bootstrap.css" /><link rel="stylesheet" type="text/css" href="/Content/admin-style.css"/></head><body onload="window.print()">')
    popupWin.document.write(toPrint.innerHTML);
    popupWin.document.write('</html>');
    popupWin.document.close();
}
/*--This JavaScript method for Print Preview command--*/
function PrintPreview() {
    var toPrint = document.getElementById('quotation');
    var popupWin = window.open('', '_blank', 'width=350,height=150,location=no,left=200px');
    popupWin.document.open();
    popupWin.document.write('<html><title>::Print Preview::</title><link rel="stylesheet" type="text/css" href="/Content/bootstrap.css"/><link rel="stylesheet" type="text/css" href="/Content/admin-style.css" /></head><body>')
    popupWin.document.write(toPrint.innerHTML);
    popupWin.document.write('</html>');
    popupWin.document.close();
}

function ExecuteAjax(url, data, fnSuccess, fnError, target) {
    /// <summary>To make ajax call to get/set data.</summary>
    /// <param name="url" type="String">Url to make ajax call.</param>
    /// <param name="data" type="Json">data to send with ajax call.</param>
    /// <param name="fnSuccess" type="Function">Success function of ajax call.</param>
    /// <param name="fnError" type="Function">Error function of ajax call.</param>
    try {
        $(".loaderDiv").fadeIn();
        $(target).prop("disabled", "disabled");

        setTimeout(function () {
            $.ajax({
                type: 'POST',
                url: url,
                async: false,
                data: (data ? JSON.stringify(data) : "{}"),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: (typeof (fnSuccess) == "function" ? function (response) {
                    fnSuccess(response);
                    $(".loaderDiv").fadeOut();
                } : function (response) { if (!IsHideLoading) $.loader('close'); }),
                error: (typeof (fnError) == "function" ? function (xhr, ajaxOptions, thrownError) {
                    window.location.href = "/Errors/Index";
                    if (!IsHideLoading) $.loader('close'); fnError(xhr, ajaxOptions, thrownError);
                } : function (xhr, ajaxOptions, thrownError) { if (!IsHideLoading) $.loader('close'); }),
                complete: function () { $(target).prop("disabled", ""); }
            });
        }, 300);
    } catch (e) {
       
        window.location.href = "/Errors/Index";

        //    throw e;
    }

}

//var $toastlast;


function ShowMessage(MsgType, Msg,btn) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        //"positionClass": "toast-top-center",
        "positionClass": "toast-top-full-width",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "10000",
        "hideDuration": "10000",
        "timeOut": "10000",
        "extendedTimeOut": "100000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut",
        "tapToDismiss": true
    }

    if (btn != undefined)
    {
        Msg = getMessageWithClearButton(Msg);
        toastr.options.tapToDismiss = true;
    }
    

    if (MsgType.trim().length > 0) {
        switch (MsgType.toLowerCase()) {
            case "success":
                toastr.success(Msg);
                break;
            case "info":
                toastr.info(Msg);
                break;
            case "warning":
                toastr.warning(Msg);
                break;
            case "error":
                toastr.error(Msg);
                break;
            default:
                toastr.info(Msg);
                break;
        }
    }
    else {
        Msg = Msg.trim() == "" ? "No Message" : Msg;
        toastr.info(Msg);
    }
    $toastlast = toastr;

    $('.toast, .toast-close-button, .tosterResendCode button').click(function () {
        toastr.clear();
    });
}

var getMessageWithClearButton = function (Msg) {
    Msg = Msg ? Msg : 'Clear itself?';
    Msg += '<button type="button" class="btn tosterResendCode">Send New Code</button>';
    return Msg;
};

function ConvertDecimalToCurrency(Amount) {

    if (Amount) {
        Amount = ConvertCurrencyToDecimal(Amount, true);
        return "$" + Amount.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
    }
    else
        return "$0";

}

function ConvertCurrencyToDecimal(Amount, IsFromCurrencyConvert) {
    if (Amount) {
        var returnValue = parseFloat(Amount.toString().replace(/,/g, '').replace('$', ''))

        if (isNaN(returnValue) && !IsFromCurrencyConvert)
            returnValue = 0;
        else if (isNaN(returnValue) && IsFromCurrencyConvert)
            returnValue = "";

        return returnValue;
    }
    else
        return 0;
}





function ConvertNumberToUnite(numbers) {
    if (numbers) {
        numbers = ConvertUniteToNumber(numbers, true);
        return numbers.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
    }
    else
        return "";

}

function ConvertUniteToNumber(numbers, IsFromNumberConvert) {
    if (numbers) {
        var returnValue = parseFloat(numbers.toString().replace(/,/g, '').replace('$', ''))

        if (isNaN(returnValue) && !IsFromNumberConvert)
            returnValue = 0;
        else if (isNaN(returnValue) && IsFromNumberConvert)
            returnValue = "";

        return returnValue;
    }
    else
        return 0;

}


function openLinkinPopUp(url) {
    //$("#basicPopupModalUrl").attr("src", 'http://twitter.github.io/bootstrap/');
    //$('#basicPopupModal').modal({ show: true });
    window.open(url, "", 'width=900,height=500,top=150,left=150');
}

Number.prototype.formatMoney = function (c, d, t) {
    var n = this,
        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "." : d,
        t = t == undefined ? "," : t,
        s = n < 0 ? "-" : "",
        i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

function ShowParsleyError(element)
{
    $(element).find('.parsley-errors-list>li').each(function () {
        ShowMessage("warning", $(this).html());
    });
}