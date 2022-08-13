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
    var total = parseInt(document.getElementById("shipping-cost").value) + parseInt(document.getElementById("subtotal").value) - parseInt(document.getElementById("discount-amount").value);
    document.getElementById("total").innerHTML = "₡" + total;
};

function deliverAddressMethod() {
    var x = document.getElementById("pickup-method-toggle");
    var y = document.getElementById("deliver-method-toggle");
    y.style.display = "block";
    x.style.display = "none";
    document.getElementById("shipping-cost-id").innerHTML = "₡1500";
    document.getElementById("shipping-cost").value = 1500;
    var total = parseInt(document.getElementById("shipping-cost").value) + parseInt(document.getElementById("subtotal").value) - parseInt(document.getElementById("discount-amount").value);
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
    var sinpe = document.getElementById("sinpe-toggle");
    var cc_debit = document.getElementById("cc-toggle");
    var cash = document.getElementById("cash-toggle");
    var paypal = document.getElementById("paypal-toggle");
    if (sinpe.style.display === "none") {
        sinpe.style.display = "block";
        cc_debit.style.display = "none";
        cash.style.display = "none";
        paypal.style.display = "none";
    }
};

function cc_debit() {
    var sinpe = document.getElementById("sinpe-toggle");
    var cc_debit = document.getElementById("cc-toggle");
    var cash = document.getElementById("cash-toggle");
    var paypal = document.getElementById("paypal-toggle");
    if (cc_debit.style.display === "none") {
        cc_debit.style.display = "block";
        sinpe.style.display = "none";
        cash.style.display = "none";
        paypal.style.display = "none";
    }
};

function cash() {
    var sinpe = document.getElementById("sinpe-toggle");
    var cc_debit = document.getElementById("cc-toggle");
    var cash = document.getElementById("cash-toggle");
    var paypal = document.getElementById("paypal-toggle");
    if (cash.style.display === "none") {
        cc_debit.style.display = "none";
        sinpe.style.display = "none";
        cash.style.display = "block";
        paypal.style.display = "none";
    }
};

function paypal() {
    var sinpe = document.getElementById("sinpe-toggle");
    var cc_debit = document.getElementById("cc-toggle");
    var cash = document.getElementById("cash-toggle");
    var paypal = document.getElementById("paypal-toggle");
    if (paypal.style.display === "none") {
        cc_debit.style.display = "none";
        sinpe.style.display = "none";
        cash.style.display = "none";
        paypal.style.display = "block";
    }
};

