$('#allorders').on("click", function () {
    $('#orderlist-nav-pills-wizard').bootstrapWizard('show', 0);
});

$('#inprocess').on("click", function () {

    $('#orderlist-nav-pills-wizard').bootstrapWizard('show', 1);
});
$('#ready').on("click", function () {

    $('#orderlist-nav-pills-wizard').bootstrapWizard('show', 2);
});
$('#intransit').on("click", function () {

    $('#orderlist-nav-pills-wizard').bootstrapWizard('show', 3);
});
$('#cancelled').on("click", function () {

    $('#orderlist-nav-pills-wizard').bootstrapWizard('show', 4);
});
$('#completed').on("click", function () {

    $('#orderlist-nav-pills-wizard').bootstrapWizard('show', 5);
});


