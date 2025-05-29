using Service.Layer.ViewModels.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Layer.ViewModels.Home
{
    public class HomeVM
    {
        public List<TaskVM> TasksList { get; set; } = new List<TaskVM>();
        public PaginatedResultVM<TaskVM> PaginatedTasks { get; set; }
    }
}
