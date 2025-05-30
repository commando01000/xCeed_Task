
$(document).ready(function () {

    // Tasks Table (normal users)
    var tasksTable = document.getElementById('tasksTable');
    // Admin Table
    var adminTable = document.getElementById('admin-tasks-table');

    // Tasks Table (for regular users)
    if (tasksTable) {
        userTasksDT = InitializeTasksDatatable();
        bindPriorityFilter(userTasksDT);
    }

    // Admin Table
    if (adminTable) {
        adminTasksDT = InitializeAdminTasksDatatable();
        bindPriorityFilter(adminTasksDT);
    }

    function bindPriorityFilter(dataTableInstance) {
        if (!dataTableInstance) return;

        $('#priorityFilter').off('change').on('change', function () {
            const selected = $(this).val();
            if (adminTable) {
                // Force reload of page 1 with selected filter
                $.ajax({
                    url: '/Home/GetPaginatedTasks',
                    type: 'POST',
                    data: {
                        page: 1,
                        priority: selected
                    },
                    success: function (response) {
                        adminTable.innerHTML = response;
                        const dt = InitializeAdminTasksDatatable();
                        bindPriorityFilter(dt);
                    },
                    error: function () {
                        Notify('Failed to apply filter.', 'Error');
                    }
                });
            } else {
                // Apply client-side filter (for normal user)
                dataTableInstance.column(2).search(selected).draw();
            }
        });
    }



    function InitializeTasksDatatable() {
        const dt = $('#tasksTable').DataTable({
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

        dt.columns.adjust().draw();

        $(window).on('resize', function () {
            dt.columns.adjust();
        });

        return dt;
    }

    function InitializeAdminTasksDatatable() {
        // Initialize Tasks Table
        const adminTasksTable = $('#tasksAdminTable').DataTable({
            scrollX: true,
            scrollCollapse: true,
            autoWidth: false,
            responsive: false,
            paging: false,
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
        adminTasksTable.columns.adjust().draw();
        // Window resize fix
        $(window).on('resize', function () {
            adminTasksTable.columns.adjust();
        });

        return adminTasksTable;
    }

    // Edit handler using event delegation for tasks table
    $('#tasksTable tbody').on('click', '.btn-edit', function () {
        const taskId = $(this).data('id');
        window.location.href = `/Task/Edit/${taskId}`;
    });

    // Delete handler using event delegation for tasks table
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
    // Admin Table
    if (adminTable != null) {
        InitializeAdminTasksDatatable();
        // Handle pagination click
        $(adminTable).on('click', '.page-link', function (e) {
            e.preventDefault();
            var page = $(this).data('page');
            if (!page) return;

            var selectedPriority = $('#priorityFilter').val(); // get current filter

            $.ajax({
                url: '/Home/GetPaginatedTasks',
                type: 'POST',
                data: {
                    page: page,
                    priority: selectedPriority // send it to server
                },
                success: function (response) {
                    adminTable.innerHTML = response;
                    const dt = InitializeAdminTasksDatatable();
                    bindPriorityFilter(dt);
                },
                error: function () {
                    Notify('Failed to load data.', 'Error');
                }
            });
        });

        // Edit Handler using event delegation
        $(adminTable).on('click', '.btn-edit', function () {
            const taskId = $(this).data('id');
            window.location.href = `/Task/Edit/${taskId}`;
        });

        // Delete handler using event delegation
        $(adminTable).on('click', '.btn-delete', async function () {
            const taskId = $(this).data('id');
            const confirmed = confirm("Are you sure you want to delete this task?");
            if (!confirmed) return;

            // Call an ajax request to delete the task
            const response = await fetch(`/Task/Delete/${taskId}`, {
                method: 'POST',
            });

            if (response.ok) {
                Notify("Task deleted successfully.", "Success");
                $(this).closest('tr').remove();
            } else {
                Notify("Failed to delete task.", "Error");
            }
        });
    }
    // Admin Table


    // Custom Filtering
    function filterTasksTable(tasksTable) {
        $('#priorityFilter').on('change', function () {
            const selectedPriority = $(this).val();
            tasksTable.column(2).search(selectedPriority).draw();
        });
    }
});
