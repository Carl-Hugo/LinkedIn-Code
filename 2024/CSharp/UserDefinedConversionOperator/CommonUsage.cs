public class MyController : Controller
{
    public ActionResult<string> GetString()
    {
        string result = "Hello, World!";
        return result; // Implicitly converts to ActionResult<string>
    }
}