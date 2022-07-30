$('#province').change(function () {
    var param = this.value;
    var html = "<option disabled selected>--Select Canton--</option>";
    $('#city').html("<option disabled selected>--Select City--</option>");
    $.ajax({
        type: 'GET',
        url: '../Cart/Checkout?handler=Canton&provinceId=' + param,
        dataType: 'json',
        cache: false,
        success: function (data) {
            $.each(data, function (key, value) {
                console.log(value.Text);
                html = html + '<option value="' +
                    value.Value + '">' + value.Text + '</option>';
                $('#canton').html(html).removeAttr('disabled');;
            });
        },
    })
});

$('#canton').change(function () {
    var param = this.value;
    var html = "<option disabled selected>--Select City--</option>";
    $.ajax({
        type: 'GET',
        url: '../Cart/Checkout?handler=City&cantonId=' + param,
        dataType: 'json',
        cache: false,
        success: function (data) {
            $.each(data, function (key, value) {
                console.log(value.Text);
                html = html + '<option value="' +
                    value.Value + '">' + value.Text + '</option>';
                $('#city').html(html).removeAttr('disabled');
            });
        },
    })
});