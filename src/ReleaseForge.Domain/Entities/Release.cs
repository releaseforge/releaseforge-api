using ReleaseForge.Domain.Enums;

namespace ReleaseForge.Domain.Entities;

public class Release
{
    public Guid Id { get; private set; }
    public Guid ProjectId { get; private set; }
    public string Version { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public ReleaseStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ReleasedAt { get; private set; }

    private readonly List<Deployment> _deployments = [];
    public IReadOnlyCollection<Deployment> Deployments => _deployments.AsReadOnly();

    private Release() { }

    public Release(Guid projectId, string version, string title, string? description)
    {
        Id = Guid.NewGuid();
        ProjectId = projectId;
        SetVersion(version);
        SetTitle(title);
        Description = description;
        Status = ReleaseStatus.Draft;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetVersion(string version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(version);
        Version = version;
    }

    public void SetTitle(string title)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        Title = title;
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public void Plan()
    {
        if (Status != ReleaseStatus.Draft)
            throw new InvalidOperationException("Only draft releases can be planned.");
        Status = ReleaseStatus.Planned;
    }

    public void Start()
    {
        if (Status != ReleaseStatus.Planned)
            throw new InvalidOperationException("Only planned releases can be started.");
        Status = ReleaseStatus.InProgress;
    }

    public void Deploy()
    {
        if (Status != ReleaseStatus.InProgress)
            throw new InvalidOperationException("Only in-progress releases can be deployed.");
        Status = ReleaseStatus.Deployed;
        ReleasedAt = DateTime.UtcNow;
    }

    public void Rollback()
    {
        if (Status is ReleaseStatus.Draft or ReleaseStatus.Cancelled)
            throw new InvalidOperationException("Cannot rollback a draft or cancelled release.");
        Status = ReleaseStatus.RolledBack;
    }

    public void Cancel()
    {
        if (Status is ReleaseStatus.Deployed or ReleaseStatus.RolledBack)
            throw new InvalidOperationException("Cannot cancel a deployed or rolled back release.");
        Status = ReleaseStatus.Cancelled;
    }

    public Deployment CreateDeployment(Guid environmentId)
    {
        var deployment = new Deployment(Id, environmentId);
        _deployments.Add(deployment);
        return deployment;
    }
}
