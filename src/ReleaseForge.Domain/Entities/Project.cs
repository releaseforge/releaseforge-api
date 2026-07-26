namespace ReleaseForge.Domain.Entities;

public class Project
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<Release> _releases = [];
    public IReadOnlyCollection<Release> Releases => _releases.AsReadOnly();

    private readonly List<DeploymentEnvironment> _environments = [];
    public IReadOnlyCollection<DeploymentEnvironment> Environments => _environments.AsReadOnly();

    private readonly List<Pipeline> _pipelines = [];
    public IReadOnlyCollection<Pipeline> Pipelines => _pipelines.AsReadOnly();

    private Project() { }

    public Project(string name, string? description)
    {
        Id = Guid.NewGuid();
        SetName(name);
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name;
        Slug = name.ToLowerInvariant()
            .Replace(' ', '-')
            .Replace('.', '-')
            .Replace("_", "-");
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetDescription(string? description)
    {
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public Release CreateRelease(string version, string title, string? description = null)
    {
        var release = new Release(Id, version, title, description);
        _releases.Add(release);
        UpdatedAt = DateTime.UtcNow;
        return release;
    }

    public DeploymentEnvironment CreateEnvironment(string name)
    {
        var env = new DeploymentEnvironment(Id, name);
        _environments.Add(env);
        UpdatedAt = DateTime.UtcNow;
        return env;
    }

    public Pipeline CreatePipeline(string name)
    {
        var pipeline = new Pipeline(Id, name);
        _pipelines.Add(pipeline);
        UpdatedAt = DateTime.UtcNow;
        return pipeline;
    }
}
