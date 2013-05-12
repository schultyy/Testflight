$(document).ready(function () {
    $("#report-list > a").click(function (e) {
        var that = this;
        $.ajax({
            type: "POST",
            url: '/Configuration/ReportDetails',
            dataType: 'json',
            data: { reportId: $(this).attr("id") },
            cache: false,
            success: function (data) {

                $("#result-details").empty();

                var heading = $("<p>").attr("id", "heading")
                        .text("Report");
                $("#result-details").append(heading);

                var list = $("<ul>");

                for (var key in data.LogEntries) {
                    var listEntry = $("<li>");
                    var currentLogEntry = data.LogEntries[key][0];
                    var message = currentLogEntry.Message;

                    listEntry.append($("<p>").attr("class", "logentry-heading")
                        .text(key));
                    listEntry.append($("<p>").attr("class", "logentry-content")
                        .text(message));
                    list.append(listEntry);
                }
                $("#result-details").append(list);
            }
        });
        e.preventDefault();
    });
});