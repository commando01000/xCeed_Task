
$(document).ready(function () {
    // Initialize Tasks Table
    const tasksTable = $('#tasksTable').DataTable({
        scrollX: true,
        scrollCollapse: true,
        autoWidth: false,
        responsive: false,
        paging: true,
        pageLength: 5,
        ordering: true,
        searching: true,
        info: true,
        lengthChange: false,
        destroy: true,
        language: {
            emptyTable: "No data available in table",
        }
    });

    // Force recalculation of columns (important for scrollX alignment)
    tasksTable.columns.adjust().draw();
    // Window resize fix
    $(window).on('resize', function () {
        tasksTable.columns.adjust();
    });


    // Edit handler
    $('#tasksTable tbody').on('click', '.btn-edit', function () {
        const taskId = $(this).data('id');
        window.location.href = `/Task/Edit/${taskId}`;
    });

    // Delete handler using event delegation
    $('#tasksTable tbody').on('click', '.btn-delete', async function () {
        const taskId = $(this).data('id');
        const confirmed = confirm("Are you sure you want to delete this task?");
        if (!confirmed) return;

        const response = await fetch(`/Task/Delete/${taskId}`, {
            method: 'POST',
        });

        if (response.ok) {
            Notify("Task deleted successfully.", "Success");
            tasksTable.row($(this).closest('tr')).remove().draw();
        } else {
            Notify("Failed to delete task.", "Error");
        }
    });
});
