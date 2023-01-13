using AkvelonIntershipTask.Context;
using AkvelonIntershipTask.DTOs;
using AkvelonIntershipTask.Models;
using AkvelonIntershipTask.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = AkvelonIntershipTask.Models.Task;
using TaskStatus = AkvelonIntershipTask.Models.TaskStatus;

namespace AkvelonIntershipTask.Controllers;

[ApiController, Route("[Controller]")]

public class TasksController : ControllerBase
{
    private readonly ProjectManagementContext _context;

    public TasksController(ProjectManagementContext context)
    {
        _context = context;
    }

    [HttpPost]
    public ActionResult CreateTask([FromBody] TaskDTO dto)
    {
        var project = _context.Project.FirstOrDefault(x => x.ProjectId == dto.ProjectId);
        if (project is null)
        {
            throw new Exception($"Project with ID {dto.ProjectId} not found");
        }

        var task = new Task
        {
            Description = dto.Description,
            Priority = dto.Priority,
            TaskName = dto.TaskName,
            Status = Enum.Parse<TaskStatus>(dto.Status),
            Project = project
        };
        _context.Task.Add(task);
        _context.SaveChanges();
        var response = new TaskResponses
        {
            TaskId = task.TaskId,
            TaskName = task.TaskName,
            Priority = task.Priority,
            Description = task.Description,
            Status = task.Status.ToString(),
            ProjectId = task.Project.ProjectId
        };
        return Ok(response);
    }

    [HttpDelete]
    [Route("id/{id}")]
    public ActionResult DeleteTask([FromRoute] int id)
    {
        var task = _context.Task.FirstOrDefault(x => x.TaskId == id);
        if (task is null)
        {
            throw new Exception($"Task with ID {id} not found");
        }

        _context.Task.Remove(task);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPut] [Route("id/{id}/status/{status}")]
    public ActionResult EditTask ([FromRoute] string status,[FromRoute]int id)
    {
        var task = _context.Task.FirstOrDefault(x => x.TaskId == id);
        if (task is null)
        {
            throw new Exception($"Task with ID {id} not found");
        }

        task.Status = Enum.Parse<TaskStatus>(status);
        _context.Task.Update(task);
        _context.SaveChanges();
        return Ok(task);
    }

    [HttpGet] public ActionResult<List<Task>> GetAllTasks()
    {
        var result = _context.Task.Include(x => x.Project).ToList();
        var response = new List<TaskResponses>();
        foreach (var task in result)
        {
            var taskResponse = new TaskResponses
            {
                TaskId = task.TaskId,
                TaskName = task.TaskName,
                Priority = task.Priority,
                Description = task.Description,
                Status = task.Status.ToString(),
                ProjectId = task.Project.ProjectId
            };
            
            response.Add(taskResponse);
        }
        return Ok(response);
    }
}