// 1️⃣ "throw ex;" resets the stack trace
try
{
    // Some code that might throw
}
catch (Exception ex)
{
    // Optional logging or cleanup
    throw ex;
}

// 2️⃣ Preserve the stack trace with "throw"
try
{
    // Some code that might throw
}
catch (Exception ex)
{
    // Optional logging or cleanup
    throw;
}

// 3️⃣ Creating a new exception without the inner exception loses context
try
{
    // Some code that might throw
}
catch (Exception ex)
{
    // Optional logging or cleanup
    throw new Exception("A new error occurred.");
}

// 4️⃣ Preserve context by adding an inner exception
try
{
    // Some code that might throw
}
catch (Exception ex)
{
    // Optional logging or cleanup
    throw new Exception("Additional context provided.", ex);
}