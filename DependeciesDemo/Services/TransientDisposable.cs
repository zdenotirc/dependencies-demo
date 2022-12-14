using System;

namespace DependeciesDemo.Services
{
    public sealed class TransientDisposable : IDisposable
    {
        private readonly ScopedDisposable _scopedDisposable;

        public TransientDisposable(ScopedDisposable scopedDisposable)
        {
            _scopedDisposable = scopedDisposable;
        }
        
        public void Dispose()
        {
            _scopedDisposable.Dispose();
            
            Console.WriteLine($"{nameof(TransientDisposable)}.Dispose()");
        }

        public void Use() => Console.WriteLine($"{nameof(TransientDisposable)}.Use()");
    }
}