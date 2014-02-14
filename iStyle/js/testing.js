
$(document).ready(function () {
    $('.dropdownactive li a').each(function (e) {
        $(this).click(function () {
            var item = $(this);
            item.parent().parent().prev().find('span').html(item.html());

        })
    });
    (function ($) {
        $('.tipclass').tooltip();
    })(window.jQuery);

    (function ($) {
        $('.floatDiv').popover('show');
    })(window.jQuery);

    $("#loaddemo-hide").click(function () {
        (function ($) {
            $("#loaddemo").loading('hide');
        })(window.jQuery);
    });

});

function DoSomething()
{
    alert("dd");
}

