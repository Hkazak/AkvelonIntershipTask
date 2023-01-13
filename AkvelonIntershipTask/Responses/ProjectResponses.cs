namespace AkvelonIntershipTask.Responses;

public class ProjectResponses
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Priority { get; set; }
    public string Status { get; set; } = null!;
}