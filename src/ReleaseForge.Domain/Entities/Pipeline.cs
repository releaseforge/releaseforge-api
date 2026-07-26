namespace ReleaseForge.Domain.Entities;

public class Pipeline
{
    public Guid Id { get; private set; }
    public Guid ProjectId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    private Pipeline() { }

    public Pipeline(Guid projectId, string name)
    {
        Id = Guid.NewGuid();
        ProjectId = projectId;
        SetName(name);
        CreatedAt = DateTime.UtcNow;
    }

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name;
    }
}
