$(document).ready(function () {
    $('#customerForm').ajaxForm(function () {
        alert("Thank you for your comment!");
        var customerForm = $('form[name="customerForm"]');
        console.log(customerForm);
        $.ajax({
            type: 'POST',
            url: '/Customer/SentProposal',
            data: { id: mlaId },
            cache: false,
            success: function (result) {

            }
        });
    });
}); 