
$(document).ready(function () {
    $('#createTaskForm').on('submit', function (e) {
        e.preventDefault(); // prevent full page reload

        const form = $(this);
        const formData = form.serialize();

        $.ajax({
            url: '/Task/Create',
            type: 'POST',
            data: formData,
            success: function (response) {
                $('.text-danger').empty(); // Clear any existing errors

                debugger;
                if (response.status === false) {
                    if (response.errors && response.errors.length > 0) {
                        let errorList = '<ul>';
                        response.errors.forEach(function (err) {
                            errorList += `<li>${err}</li>`;
                        });
                        errorList += '</ul>';
                        $('div[asp-validation-summary="ModelOnly"]').html(errorList);
                    } else {
                        Notify(response.message, "Error");
                    }
                } else {
                    Notify("Task created successfully", "Success");
                    form[0].reset();
                    setTimeout(function () {
                        window.location.href = response.redirectURL;
                    }, 2500);
                }
            },
            error: function () {
                Notify("An error occurred while creating the task.", "Error");
            }
        });
    });
});