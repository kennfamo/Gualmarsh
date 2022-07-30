function GetCanton() {
    var param = $('#province').val();
    $.ajax({
        type: 'GET',
        url: '../Cart/Checkout?handler=Canton?param=' + param,
        dataType: 'json',
        data: { 'ProvinceId': $('#province').val() },
        cache: false,
        success: function (data) {
            alert(data);
        },               
    })
};
