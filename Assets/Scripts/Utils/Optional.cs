using System;

namespace Utils
{
    public class Optional<T>
    {
        private readonly T _value;
        private readonly bool _valueSet;

        public Optional(T value)
        {
            _value = value;
            _valueSet = true;
        }

        public Optional()
        {
            _valueSet = false;
        }

        public Optional<R> Map<R>(Func<T, R> transform)
        {
            return _valueSet ? new Optional<R>(transform(_value)) : new Optional<R>();
        }

        public void Fold(Action<T> ifPresent, Action ifAbsent)
        {
            if (_valueSet)
                ifPresent(_value);
            else
                ifAbsent();
        }

        public T GetOrElse(Func<T> result)
        {
            return _valueSet ? _value : result();
        }

        public T GetOrDefault(T defaultValue)
        {
            return _valueSet ? _value : defaultValue;
        }
    }
}
