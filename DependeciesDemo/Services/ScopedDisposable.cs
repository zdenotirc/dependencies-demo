using System;

namespace DependeciesDemo.Services
{
    public sealed class ScopedDisposable : IDisposable
    {
        public void Dispose() => Console.WriteLine($"{nameof(ScopedDisposable)}.Dispose()");
        
        public void Use() => Console.WriteLine($"{nameof(ScopedDisposable)}.Use()");
    }
}