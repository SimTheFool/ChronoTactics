using System;

public struct Watched<T>
{
    private Action<T, T> onPropertyChanged;

    private T property;
    public T Value
    {
        get => this.property;
        set
        {
            if(!value.Equals(this.property))
                this.onPropertyChanged?.Invoke(value, this.property);
            this.property = value;
        }
    }

    public Watched(Action<T, T> action)
    {
        this.onPropertyChanged = action;
        this.property = default(T);
    }

    public Watched(T defaultValue, Action<T, T> action)
    {
        this.onPropertyChanged = action;
        this.property = defaultValue;
    }
}