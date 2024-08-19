

public class ConsumerBeforeCSharp11
{
    [BeforeCSharp11(typeof(string))]
    public void Method() => throw new NotImplementedException();
}

public class BeforeCSharp11Attribute : Attribute
{
    public BeforeCSharp11Attribute(Type t) => ParamType = t;
    public Type ParamType { get; }
}


public class ConsumerSinceCSharp11
{
    [SinceCSharp11Generic<string>]
    public void Method() => throw new NotImplementedException();
}

public class SinceCSharp11GenericAttribute<T> : Attribute
{
}