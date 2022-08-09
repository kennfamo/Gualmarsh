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
   
    x.style.display = "block";
    y.style.display = "none";
    
    document.getElementById("shipping-cost-id").innerHTML = "Free";
    document.getElementById("shipping-cost").value = 0;
    var total = parseInt(document.getElementById("shipping-cost").value) + parseInt(document.getElementById("subtotal").value);
    document.getElementById("total").innerHTML = "₡" + total;
};

function deliverAddressMethod() {
    var x = document.getElementById("pickup-method-toggle");
    var y = document.getElementById("deliver-method-toggle");
    y.style.display = "block";
    x.style.display = "none";
    document.getElementById("shipping-cost-id").innerHTML = "₡1500";
    document.getElementById("shipping-cost").value = 1500;
    var total = parseInt(document.getElementById("shipping-cost").value) + parseInt(document.getElementById("subtotal").value);
    document.getElementById("total").innerHTML = "₡" + total;
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

function sinpe() {
    var x = document.getElementById("sinpe-toggle");
    if (x.style.display === "none") {
        x.style.display = "block";
    }
    else {
        x.style.display = "none";
    }
}

function cc_debit() {
    var x = document.getElementById("sinpe-toggle");
    if (x.style.display === "block") {
        x.style.display = "none";
    }
}

function cash() {
    var x = document.getElementById("sinpe-toggle");
    if (x.style.display === "block") {
        x.style.display = "none";
    }
}


