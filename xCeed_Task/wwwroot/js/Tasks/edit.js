
$(document).ready(function () {

    $('#save-task').on('click', function (e) {
        e.preventDefault();

        const form = $('#edit-task-form');
        const formData = form.serialize();

        $.ajax({
            url: '/Task/Edit',
            type: 'POST',
            data: formData,
            success: function (response) {
                debugger;
                if (response.status) {
                    Notify(response.message, 'Success');
                    setTimeout(function () {
                        window.location.href = '/Home/Index';
                    }, 2500);
                }
                else {
                    Notify(response.message, 'Error');
                }
            },
            error: function (xhr) {
                console.error(xhr);
                Notify(xhr.responseJSON.Message, 'Error');
            }
        });
    });
});