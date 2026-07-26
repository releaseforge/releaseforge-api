using ReleaseForge.Domain.Enums;

namespace ReleaseForge.Domain.Entities;

public class Deployment
{
    public Guid Id { get; private set; }
    public Guid ReleaseId { get; private set; }
    public Guid EnvironmentId { get; private set; }
    public DeploymentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private Deployment() { }

    public Deployment(Guid releaseId, Guid environmentId)
    {
        Id = Guid.NewGuid();
        ReleaseId = releaseId;
        EnvironmentId = environmentId;
        Status = DeploymentStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void Start()
    {
        if (Status != DeploymentStatus.Pending)
            throw new InvalidOperationException("Only pending deployments can be started.");
        Status = DeploymentStatus.InProgress;
        StartedAt = DateTime.UtcNow;
    }

    public void Succeed()
    {
        if (Status != DeploymentStatus.InProgress)
            throw new InvalidOperationException("Only in-progress deployments can succeed.");
        Status = DeploymentStatus.Succeeded;
        CompletedAt = DateTime.UtcNow;
    }

    public void Fail()
    {
        if (Status != DeploymentStatus.InProgress)
            throw new InvalidOperationException("Only in-progress deployments can fail.");
        Status = DeploymentStatus.Failed;
        CompletedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status is DeploymentStatus.Succeeded or DeploymentStatus.Failed)
            throw new InvalidOperationException("Cannot cancel a completed deployment.");
        Status = DeploymentStatus.Cancelled;
        CompletedAt = DateTime.UtcNow;
    }

    public void Rollback()
    {
        if (Status != DeploymentStatus.Succeeded)
            throw new InvalidOperationException("Only succeeded deployments can be rolled back.");
        Status = DeploymentStatus.RolledBack;
        CompletedAt = DateTime.UtcNow;
    }
}
