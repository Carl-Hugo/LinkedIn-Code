using Global_Using_Directives.MyNamespace;

namespace Global_Using_Directives;

public class Service1
{
    public void Operation()
    {
        var myClass = new MyClass();
        Console.WriteLine("Service1");
        myClass.Operation();
    }
}
