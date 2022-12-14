using System;

namespace DependeciesDemo.Services
{
    public sealed class SingletonDisposable : IDisposable
    {
        public void Dispose() => Console.WriteLine($"{nameof(SingletonDisposable)}.Dispose()");

        public void Use() => Console.WriteLine($"{nameof(SingletonDisposable)}.Use()");
    }
}