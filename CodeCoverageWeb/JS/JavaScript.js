function ShowMessage(message)
{
    alert(message);
}

function displayLoading()
{
    $(".preload").css('visibility', 'visible');
    $(".preload").fadeOut(60000);
}

$(function () {
    $("#GetBtn").click(function () {
        $(".preload").css('visibility', 'visible');
        $(".preload").fadeOut(60000);
    });
});
    
