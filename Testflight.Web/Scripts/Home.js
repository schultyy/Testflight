$(document).ready(function () {
    $(".query-build-link").click(function (e) {
        var that = this;
        $.ajax({
            type: "POST",
            url: '/Home/QueryBuild',
            dataType: 'json',
            data: { configurationId: $(this).attr("id") },
            cache: false,
            success: function (data) {
                if (data == "OK") {
                    var td = $(that).parent().parent().children(".build-status");
                    td.empty();
                    td.append($("<p>").text("running"));
                }
            }
        });
        e.preventDefault();
    });
});