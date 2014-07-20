$(document).ready(function () {
    $('.delete-button').click(function (e) {
        if (!confirm($(this).attr('data-confirm'))) {
            e.preventDefault();
        }
    });
});