namespace Shared;

public static class Constants
{
#if INITIAL_VALUE
    public const string MY_CONST = "InitialValue";
    public static readonly string MY_READONLY = "InitialValue";
#else
    public const string MY_CONST = "UpdatedValue";
    public static readonly string MY_READONLY = "UpdatedValue";
#endif
}
