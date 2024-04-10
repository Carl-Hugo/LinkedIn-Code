namespace Shared;

public static class Constants
{
#if INITIAL_VALUE
    public const string MY_CONST = "InitialValue";
#else
    public const string MY_CONST = "UpdatedValue";
#endif
}
