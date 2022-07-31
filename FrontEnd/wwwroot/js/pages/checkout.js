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

$('#shipping').on("click", function () {

    $('#checkout-nav-pills-wizard').bootstrapWizard('show', 0);
});
$('#payment').on("click", function () {

    $('#checkout-nav-pills-wizard').bootstrapWizard('show', 1);
});

function pickUpMethod() {
    var x = document.getElementById("pickup-method-toggle");
    var y = document.getElementById("deliver-method-toggle");
    if (x.style.display === "none") {
        x.style.display = "block";
        y.style.display = "none";
    }
    else
    {
        x.style.display = "none";
        y.style.display = "block";
    }
};

function deliverAddressMethod() {
    var x = document.getElementById("pickup-method-toggle");
    var y = document.getElementById("deliver-method-toggle");
    if (y.style.display === "none") {
        y.style.display = "block";
        x.style.display = "none";
    }
    else {
        y.style.display = "none";
        x.style.display = "block";
    }
};

function newShippingAddress() {
    var x = document.getElementById("add-shipping-toggle");
    if (x.style.display === "none") {
        x.style.display = "block";
    }
    else {
        x.style.display = "none";
    }
};


