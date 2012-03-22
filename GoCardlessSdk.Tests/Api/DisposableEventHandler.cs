using System;

namespace GoCardlessSdk.Tests.Api
{
    public class DisposableEventHandler<T> : IDisposable
    {
        private readonly Action<T> _remove;
        private readonly T _t;

        public DisposableEventHandler(Action<T> add, Action<T> remove, T t)
        {
            _remove = remove;
            _t = t;
            add(t);
        }

        public void Dispose()
        {
            _remove(_t);
        }
    }
}