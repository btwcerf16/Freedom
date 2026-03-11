using System;
using UnityEngine;

public class Subscriber<T, K>: IDisposable
{
    private Action<T, K> _action;
    private Action<Subscriber<T, K>> _onDispose;

    public Subscriber(Action<T, K> action, Action<Subscriber<T, K>> onDispose)
    {
        _action = action;
        _onDispose = onDispose;
    }
    public void Invoke(T arg0, K arg1) => _action?.Invoke(arg0, arg1);
    public void Dispose() => _onDispose?.Invoke(this);
}
