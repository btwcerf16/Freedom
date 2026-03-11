using System;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveVariable<T>
{
    private readonly List<Subscriber<T, T>> _subscribers = new();

    private T _value;
    private IEqualityComparer<T> _comparer;
    private T t;

    public ReactiveVariable() : this(default(T))
    {
    }
    public ReactiveVariable(T value) : this(value, EqualityComparer<T>.Default)
    {
    }
    public ReactiveVariable(T value, IEqualityComparer<T> comparer)
    {
        _value = value;
        _comparer = comparer;
    }
    public T Value
    {
        get => _value;
        set
        {
            T oldValue = _value;
            _value = value;
            if (_comparer.Equals(oldValue, value) == false)
            {
                foreach (var subscriber in _subscribers)
                {
                    subscriber.Invoke(oldValue, value);
                }
            }
        }
    }
    public IDisposable Subscribe(Action<T, T> action)
    {
        Subscriber<T, T> subscriber = new Subscriber<T, T>(action, Remove);
        _subscribers.Add(subscriber);
        return subscriber;
    }
    public void Remove(Subscriber<T, T> subscriber) => _subscribers.Remove(subscriber);

}
