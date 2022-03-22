using System;

namespace FrameworkDesign
{
    public class BindableProperty<T> where T : IEquatable<T>
    {
        private T value = default(T);
        public Action<T> OnValueChanged;

        public T Value
        {
            get => value;
            set
            {
                if(!value.Equals(this.value))
                {
                    this.value = value;
                    OnValueChanged?.Invoke(value);
                }
            }
        }
    }
}