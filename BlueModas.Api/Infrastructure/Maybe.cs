using System;

namespace BlueModas.Api.Infrastructure
{
    public struct Maybe<T>
    {
        private T _value;

        private Maybe(T value)
        {
            _value = value;
        }

        public T Value => _value ?? throw new InvalidOperationException();

        public bool HasValue => _value != null;

        public static implicit operator Maybe<T>(T value)
        {
            return new Maybe<T>(value);
        }
    }
}
