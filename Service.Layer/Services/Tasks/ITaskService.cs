using Repository.Layer.Specifications.Tasks;
using Service.Layer.ViewModels.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Layer.Services.Tasks
{
    public interface ITaskService
    {
        public Task<List<TaskVM>> GetAllTasks(TasksSpecifications tasksSpecifications);
        public Task<TaskVM> AddTask(TaskVM task);
        public Task<TaskVM> UpdateTask(TaskVM task);
        public Task<bool> DeleteTask(string id);
    }
}
