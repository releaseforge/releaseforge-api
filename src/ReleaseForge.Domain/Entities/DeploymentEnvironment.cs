namespace ReleaseForge.Domain.Entities;

public class DeploymentEnvironment
{
    public Guid Id { get; private set; }
    public Guid ProjectId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public int SortOrder { get; private set; }

    private DeploymentEnvironment() { }

    public DeploymentEnvironment(Guid projectId, string name)
    {
        Id = Guid.NewGuid();
        ProjectId = projectId;
        SetName(name);
    }

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name;
        Slug = name.ToLowerInvariant().Replace(' ', '-');
    }

    public void SetSortOrder(int sortOrder)
    {
        SortOrder = sortOrder;
    }
}
