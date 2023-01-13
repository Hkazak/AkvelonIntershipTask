namespace AkvelonIntershipTask.DTOs;

public class ProjectDTO
{
    public string ProjectName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Priority { get; set; }
    public string Status { get; set; }
}