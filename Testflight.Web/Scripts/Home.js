$(document).ready(function () {
    $(".query-build-link").click(function (e) {
        $.ajax({
            type: "POST",
            url: '/Home/QueryBuild',
            dataType: 'json',
            data: { configurationId: $(this).attr("id") },
            cache: false,
            success: function (data) {

            }
        });
        e.preventDefault();
    });
});