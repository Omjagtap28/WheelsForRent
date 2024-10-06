function showPaymentForm(paymentOption) {
    console.log("showPaymentForm called with option:", paymentOption);


    // Hide all payment forms
    var forms = document.getElementsByClassName("payment-form");
    for (var i = 0; i < forms.length; i++) {
        forms[i].style.display = "none";
    }

    // Show the selected payment form
    var selectedForm = document.getElementById(paymentOption + "Form");
    if (selectedForm) {
        selectedForm.style.display = "block";
    }
}
