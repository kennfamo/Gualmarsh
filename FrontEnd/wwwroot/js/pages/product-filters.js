
$(document).ready(function () {
    var maxLimit = $("#maxPrice").val();
    var filteredMaxLimit = $("#filteredMaxPrice").val();
    var filteredMinLimit = $("#filteredMinPrice").val();
    $("#pricerange").ionRangeSlider({
        skin: "round",
        type: "double",
        grid: !0,
        step: 500,
        min: 0,
        max: maxLimit,
        from: filteredMinLimit,
        to: filteredMaxLimit,
        prefix: "₡",
        onFinish: function (data) {
            if (this.from != data.from) {
                var searchParams = new URLSearchParams(window.location.search);
                searchParams.set("min_price", data.from);
                window.location.search = searchParams.toString();
            } else if (this.to != data.to) {
                var searchParams = new URLSearchParams(window.location.search);
                searchParams.set("max_price", data.to);
                window.location.search = searchParams.toString();
            } 
        }
    });
});
