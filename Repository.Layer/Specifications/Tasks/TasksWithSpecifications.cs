using Data.Layer.Entities;
using Repository.Layer.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Layer.Specifications.Tasks
{
    public class TasksWithSpecifications : BaseSpecifications<TaskItem>
    {
        public TasksWithSpecifications(TasksSpecifications spec) : base(task =>
            (string.IsNullOrEmpty(spec.Id) || task.Id.ToString() == spec.Id) &&
            (string.IsNullOrEmpty(spec.AssignedUserId) || task.AssignedUserId == spec.AssignedUserId) &&
            (string.IsNullOrEmpty(spec.Search) || task.AssignedUser != null && task.AssignedUser.DisplayName.ToLower().Contains(spec.Search.ToLower())) &&
            (string.IsNullOrEmpty(spec.TaskName) || task.TaskName.ToLower().Contains(spec.TaskName.ToLower())) &&
            (string.IsNullOrEmpty(spec.Description) || task.Description.ToLower().Contains(spec.Description.ToLower())) &&
            (!spec.FilterByPriority || task.Priority == spec.Priority) &&
            (!spec.DueDate.HasValue || task.DueDate.Date >= spec.DueDate.Value.Date)
        )
        {
            // include related user
            AddInclude(task => task.AssignedUser);
            if (spec.isPagingEnabled)
            {
                ApplyPaging(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);
            }
        }

        public TasksWithSpecifications(string Id) : base(task => task.Id.ToString() == Id)
        {
            // include related user
            AddInclude(task => task.AssignedUser);
        }
    }
}
