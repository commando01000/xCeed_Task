using AutoMapper;
using Common.Layer;
using Data.Layer.Contexts;
using Data.Layer.Entities;
using Repository.Layer.Interfaces;
using Repository.Layer.Specifications.Tasks;
using Service.Layer.ViewModels;
using Service.Layer.ViewModels.Tasks;

namespace Service.Layer.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public TaskService(IUnitOfWork<AppDbContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TaskVM>> GetAllTasks(TasksSpecifications tasksSpecifications)
        {
            var specs = new TasksWithSpecifications(tasksSpecifications);

            var tasks = await _unitOfWork.Repository<TaskItem, string>().GetAllWithSpecs(specs);

            var mappedTasks = _mapper.Map<List<TaskVM>>(tasks);

            return mappedTasks;
        }

        public async Task<PaginatedResultVM<TaskVM>> GetAllTasksPaginated(TasksSpecifications tasksSpecifications)
        {
            var specs = new TasksWithSpecifications(tasksSpecifications);

            var tasks = await _unitOfWork.Repository<TaskItem, Guid>().GetAllWithSpecs(specs);

            var countSpecs = new TasksWithCountSpecifications(tasksSpecifications);

            var TasksCount = await _unitOfWork.Repository<TaskItem, Guid>().GetCountAsync(countSpecs);

            var mappedTasks = _mapper.Map<List<TaskVM>>(tasks);

            var Pages = TasksCount % tasksSpecifications.PageSize == 0 ? TasksCount / tasksSpecifications.PageSize : (TasksCount / tasksSpecifications.PageSize) + 1;

            var totalPages = Pages;

            var paginatedResult = new PaginatedResultVM<TaskVM>(
                totalPages,
                tasksSpecifications.PageIndex,
                tasksSpecifications.PageSize,
                mappedTasks
            );

            return paginatedResult;
        }

        public async Task<TaskVM> GetTask(Guid id)
        {
            var tasksSpecifications = new TasksSpecifications();
            tasksSpecifications.Id = id.ToString();

            var specs = new TasksWithSpecifications(tasksSpecifications);
            var task = await _unitOfWork.Repository<TaskItem, Guid>().GetWithSpecs(specs);

            var mappedTask = _mapper.Map<TaskVM>(task);
            return mappedTask;
        }

        public async Task<Response<Nothing>> AddTask(TaskVM task)
        {
            var mappedTask = _mapper.Map<TaskItem>(task);
            await _unitOfWork.Repository<TaskItem, Guid>().Create(mappedTask);
            await _unitOfWork.CompleteAsync();

            if (mappedTask.Id != Guid.Empty)
            {
                return new Response<Nothing>()
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Task added successfully"
                };
            }
            else
            {
                return new Response<Nothing>()
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Error while adding task"
                };
            }
        }

        public async Task<Response<Nothing>> UpdateTask(TaskVM task)
        {
            var mappedTask = _mapper.Map<TaskItem>(task);
            await _unitOfWork.Repository<TaskItem, Guid>().Update(mappedTask);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {
                return new Response<Nothing>()
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Task updated successfully"
                };
            }
            else
            {
                return new Response<Nothing>()
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Error while updating task"
                };
            }
        }

        public async Task<Response<Nothing>> DeleteTask(Guid id)
        {
            var taskItem = await _unitOfWork.Repository<TaskItem, Guid>().Get(id);
            var result = await _unitOfWork.Repository<TaskItem, Guid>().Delete(taskItem);
            await _unitOfWork.CompleteAsync();

            if (result)
            {
                return new Response<Nothing>()
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Task deleted successfully"
                };
            }
            else
            {
                return new Response<Nothing>()
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Error while deleting task"
                };
            }
        }

    }
}
