using AutoMapper;
using Data.Layer.Contexts;
using Data.Layer.Entities;
using Repository.Layer.Interfaces;
using Repository.Layer.Specifications.Tasks;
using Service.Layer.ViewModels.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<TaskVM> AddTask(TaskVM task)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTask(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TaskVM> UpdateTask(TaskVM task)
        {
            throw new NotImplementedException();
        }
    }
}
