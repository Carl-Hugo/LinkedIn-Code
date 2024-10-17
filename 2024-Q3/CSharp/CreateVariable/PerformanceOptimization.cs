namespace CreateVariable;

public class PerformanceOptimization
{
    public void StackAllocation()
    {
        // Stack Allocation using Span<T>: Allocates memory on the stack for lightweight data structures. (C# 7.2)
        Span<int> numbers = stackalloc int[3] { 1, 2, 3 };
        Console.WriteLine($"Stack-allocated span: {string.Join(", ", numbers.ToArray())}");
        // Outputs: Stack-allocated span: 1, 2, 3
    }

    public void PointerVariable()
    {
        // Unsafe code and pointers: Allows direct memory access for performance optimization. (C# 1.0)
        // You must allow unsafe code to run. For example, add `<AllowUnsafeBlocks>true</AllowUnsafeBlocks>` in your .csproj file.
        unsafe
        {
            int number = 42;
            int* pointer = &number;
            Console.WriteLine($"Value at pointer: {*pointer}");
            // Outputs: Value at pointer: 42
        }
    }

    public void FixedBufferUsage()
    {
        // Fixed size buffer within an unsafe struct. (C# 1.0)
        // You must allow unsafe code to run. For example, add `<AllowUnsafeBlocks>true</AllowUnsafeBlocks>` in your .csproj file.
        unsafe
        {
            // Use the buffer
            FixedBufferExample buffer;
            for (int i = 0; i < 5; i++)
            {
                buffer.Numbers[i] = i * 10;
            }

            // Display the buffer
            Console.Write("Fixed size buffer within an unsafe struct: ");
            for (int i = 0; i < 5; i++)
            {
                if (i > 0) { Console.Write(", "); }
                Console.Write(buffer.Numbers[i]);
            }
            Console.WriteLine();
            // Outputs: Fixed size buffer within an unsafe struct: 0, 10, 20, 30, 40
        }
    }

    public unsafe struct FixedBufferExample
    {
        public fixed int Numbers[5];
    }

    public void ArrayPooling()
    {
        // Object pooling using ArrayPool<T> from System.Buffers (C# 4.0, .NET Core)
        var pool = System.Buffers.ArrayPool<int>.Shared;
        const int numberOfElementsToRent = 10;
        int[] pooledArray = pool.Rent(numberOfElementsToRent);  // Rent an array of at least 10 elements

        // Use the array
        for (int i = 0; i < numberOfElementsToRent; i++)
        {
            pooledArray[i] = i;
        }

        // Display the array
        Console.WriteLine($"Object pooling using ArrayPool: {string.Join(", ", pooledArray)}");
        // Outputs: Object pooling using ArrayPool: 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 0, 0, 0, 0, 0

        // Return the array to the pool
        pool.Return(pooledArray);
    }


    public void ArrayPooling2()
    {
        // Object pooling using ArrayPool<T> from System.Buffers (C# 4.0, .NET Core)
        var pool = System.Buffers.ArrayPool<int>.Shared;

        // Rent an array, use it, then release it
        const int numberOfElementsToRentA = 15;
        int[] pooledArrayA = pool.Rent(numberOfElementsToRentA);
        for (int i = 0; i < numberOfElementsToRentA; i++)
        {
            pooledArrayA[i] = i * 10;
        }
        pool.Return(pooledArrayA);

        // Rend a  array (will reuse the previous one)
        const int numberOfElementsToRentB = 10;
        int[] pooledArrayB = pool.Rent(numberOfElementsToRentB);  // Rent an array of at least 10 elements

        // Use the array
        for (int i = 0; i < numberOfElementsToRentB; i++)
        {
            pooledArrayB[i] = i;
        }

        // Display the array
        Console.WriteLine($"Object pooling using ArrayPool (preused array): {string.Join(", ", pooledArrayB)}");
        // Outputs: Object pooling using ArrayPool (preused array): 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 100, 110, 120, 130, 140, 0

        // Return the array to the pool
        pool.Return(pooledArrayB);
    }

    public void SpanMemoryOptimization()
    {
        // Span<T> and Memory<T>: Optimized for handling slices of memory efficiently. (C# 7.2 for Span<T>)
        int[] numbers = { 1, 2, 3, 4, 5 };
        Span<int> numbersSpan = numbers.AsSpan(1, 3);  // Create a slice of the array

        Console.Write("Span<T> is optimized for handling slices of memory: ");
        for (int i = 0; i < numbersSpan.Length; i++)
        {
            if (i > 0) { Console.Write(", "); }
            Console.Write(numbersSpan[i]);
        }
        Console.WriteLine();
        // Outputs: Span<T> is optimized for handling slices of memory: 2, 3, 4
    }

    public void MemoryOptimization()
    {
        int[] numbers = { 1, 2, 3, 4, 5 };
        Memory<int> memory = numbers.AsMemory();  // Convert array to Memory<T>
        Memory<int> memorySlice = memory.Slice(start: 1, length: 3);  // Create a slice

        Console.WriteLine($"Memory<T> slice: {string.Join(", ", memorySlice.ToArray())}");
        // Outputs: Memory<T> slice: 2, 3, 4
    }

    public void RefStructUsage()
    {
        // Ref struct: Avoids heap allocations by restricting struct to the stack. (C# 7.2)
        PerformanceStruct myStruct = new PerformanceStruct { X = 10, Y = 20 };
        Console.WriteLine($"RefStruct X: {myStruct.X}, Y: {myStruct.Y}");
        // Outputs: RefStruct X: 10, Y: 20
    }

    public ref struct PerformanceStruct
    {
        public int X;
        public int Y;
    }

    public void InParameterExample()
    {
        // In parameters for value types: Avoid copying large structs by passing by reference without modification. (C# 7.2)
        AnyStruct myStruct = new() { X = 10, Y = 20 };
        PrintStruct(in myStruct);
        // Outputs: InParameter X: 10, Y: 20

        // In parameter: The struct cannot be modified inside.
        static void PrintStruct(in AnyStruct myStruct)
            => Console.WriteLine($"InParameter X: {myStruct.X}, Y: {myStruct.Y}");

    }

    public struct AnyStruct
    {
        public int X;
        public int Y;
    }
}
