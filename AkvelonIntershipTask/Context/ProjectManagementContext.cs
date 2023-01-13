using AkvelonIntershipTask.Models;
using Microsoft.EntityFrameworkCore;
using Task = AkvelonIntershipTask.Models.Task;

namespace AkvelonIntershipTask.Context;

public class ProjectManagementContext: DbContext
{
    public DbSet<Project> Project { get; set; }
    public DbSet<Task> Task { get; set; }
    protected ProjectManagementContext()
    {
    }

    public ProjectManagementContext(DbContextOptions options) : base(options)
    {
    }
}