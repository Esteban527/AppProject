using AppProject.Web.Core;
using AppProject.Web.Data;
using AppProject.Web.Data.Entities;
using AppProject.Web.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;

namespace AppProject.Web.Services
{
    public interface ITaskListsService
    {
        public Task<Response<List<TaskList>>> GetListAsyc();
    }

    public class TaskListsService : ITaskListsService
    {
        private readonly DataContext _context;

        public TaskListsService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<List<TaskList>>> GetListAsyc()
        {
            try
            {
                List<TaskList> list = await _context.TaskLists.ToListAsync();
                return new Response<List<TaskList>>
                {
                    IsSuccess = true,
                    Message = "Lista obtenida con exito",
                    Result = list
                };

                

            }
            catch (Exception ex)
            {
                return new Response<List<TaskList>>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    
                };
            }
        }
    }
    
}