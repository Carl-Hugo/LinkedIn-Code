public class ServiceWithoutGuardClause
{
    private readonly IDependency _dependency;
    public ServiceWithoutGuardClause(IDependency dependency)
    {
        // Without a guard clause, the dependency could be null.
        _dependency = dependency;
    }

    public void Operation()
    {
        // This might throw a NullReferenceException if _dependency is null.
        _dependency.Method();
    }
}

public class ServiceWithGuardClause
{
    private readonly IDependency _dependency;
    public ServiceWithoutGuardClause(IDependency dependency)
    {
        // Guard clause to prevent the dependency from being null
        _dependency = dependency ?? throw new ArgumentNullException(nameof(dependency));
    }

    public void Operation()
    {
        // Safe to call Method as _dependency is guaranteed not to be null
        _dependency.Method();
    }
}

public interface IDependency
{
    void Method();
}