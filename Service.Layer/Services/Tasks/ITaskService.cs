using Common.Layer;
using Repository.Layer.Specifications.Tasks;
using Service.Layer.ViewModels;
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
        public Task<PaginatedResultVM<TaskVM>> GetAllTasksPaginated(TasksSpecifications tasksSpecifications);
        public Task<TaskVM> GetTask(Guid id);
        public Task<Response<Nothing>> AddTask(TaskVM task);
        public Task<Response<Nothing>> UpdateTask(TaskVM task);
        public Task<Response<Nothing>> DeleteTask(Guid id);
    }
}
