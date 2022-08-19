
$(document).ready(function () {
    var maxLimit = $("#maxPrice").val();
    var filteredMaxLimit = $("#filteredMaxPrice").val();
    var filteredMinLimit = $("#filteredMinPrice").val();
    var filteredRating = $("#filteredRating").val();
    if (filteredRating == 4) {
        $("#productRatingRadio1").prop("checked", true);
        $("#clearLink").prop("hidden", false);
    } else if (filteredRating == 3) {
        $("#productRatingRadio2").prop("checked", true);
        $("#clearLink").prop("hidden", false);
    } else if (filteredRating == 2) {
        $("#productRatingRadio3").prop("checked", true);
        $("#clearLink").prop("hidden", false);
    } else if (filteredRating == 1) {
        $("#productRatingRadio4").prop("checked", true);
        $("#clearLink").prop("hidden", false);
    }
    var searchParams = new URLSearchParams(window.location.search);
    $("#pricerange").ionRangeSlider({
        skin: "round",
        type: "double",
        grid: true,
        step: 500,
        min: 0,
        max: maxLimit,
        from: filteredMinLimit,
        to: filteredMaxLimit,
        prefix: "₡",
        onFinish: function (data) {
            if (this.from != data.from) {
                searchParams.set("min_price", data.from);
                window.location.search = searchParams.toString();
            } else if (this.to != data.to) {
                searchParams.set("max_price", data.to);
                window.location.search = searchParams.toString();
            } 
            if (data.to == 0) {
                searchParams.delete("max_price");
                window.location.search = searchParams.toString();
            }
            if (data.from == 0) {
                searchParams.delete("min_price");
                window.location.search = searchParams.toString();
            }
        }
    });
    $(document).on("click", "#productRatingRadio1", function () {
        searchParams.set("rating", 4);
        window.location.search = searchParams.toString();        
    });
    $(document).on("click", "#productRatingRadio2", function () {
        searchParams.set("rating", 3);
        window.location.search = searchParams.toString(); 
        
    });
    $(document).on("click", "#productRatingRadio3", function () {
        searchParams.set("rating", 2);
        window.location.search = searchParams.toString(); 
        
    });
    $(document).on("click", "#productRatingRadio4", function () {
        searchParams.set("rating", 1);
        window.location.search = searchParams.toString(); 
        
    });
    
    $("#clearLink").click(function () {
        searchParams.delete("rating");
        window.location.search = searchParams.toString(); 
    });
    $("#recentLink").click(function () {
        searchParams.set("recent", 1);
        window.location.search = searchParams.toString();
    });
    $("#popularityLink").click(function () {
        searchParams.delete("recent");
        window.location.search = searchParams.toString();
    });
});

