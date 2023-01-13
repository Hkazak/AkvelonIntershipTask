using AkvelonIntershipTask.Context;
using AkvelonIntershipTask.DTOs;
using AkvelonIntershipTask.Models;
using AkvelonIntershipTask.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AkvelonIntershipTask.Controllers;

[ApiController,Route("[Controller]")]

public class ProjectController: ControllerBase
{
    private readonly ProjectManagementContext _context;

    public ProjectController(ProjectManagementContext context)
    {
        _context = context;
    }

    [HttpPost]
    public ActionResult CreateProject([FromBody] ProjectDTO dto)
    {
        var project = new Project
        {
            ProjectName = dto.ProjectName,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Priority = dto.Priority,
            Status = Enum.Parse<ProjectStatus>(dto.Status)
        };
        _context.Project.Add(project);
        _context.SaveChanges();
        var response = new ProjectResponses
        {
            ProjectId = project.ProjectId,
            ProjectName = project.ProjectName,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Priority = project.Priority,
            Status = project.Status.ToString()
        };
        return Ok(response);
    }
    [HttpDelete]
    [Route("id/{id}")]
    public ActionResult DeleteProject([FromRoute] int id)
    {
        var project = _context.Project.FirstOrDefault(x => x.ProjectId == id);
        if (project is null)
        {
            throw new Exception($"Project with ID {id} not found");
        }

        _context.Project.Remove(project);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPut]
    [Route("id/{id}/status/{status}")]
    public ActionResult EditProject([FromRoute] string status,[FromRoute]int id)
    {
        var project = _context.Project.FirstOrDefault(x => x.ProjectId == id);
        if (project is null)
        {
            throw new Exception($"Project with ID {id} not found");
        }

        project.Status = Enum.Parse<ProjectStatus>(status);
        _context.Project.Update(project);
        _context.SaveChanges();
        return Ok(project);
    }

    [HttpGet]
    public ActionResult<List<Project>> GetAllProjects()
    {
        var result = _context.Project.ToList();
        var response = new List<ProjectResponses>();
        foreach (var project in result)
        {
            var projectResponse = new ProjectResponses
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority,
                Status = project.Status.ToString()
            };
            
            response.Add(projectResponse);
        }
        return Ok(response);
    }
}