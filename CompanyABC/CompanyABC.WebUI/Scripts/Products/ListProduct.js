$(document).ready(function () {
    $('.delete-button').click(function (e) {
        if (!confirm($(this).attr('data-confirm'))) {
            e.preventDefault();
        }
    });

    function getParameterByName(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
        return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }

    if (getParameterByName("search") !== "") {
        $(".search-query").addClass("x");
    }
});