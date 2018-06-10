using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Simple.Xamarin.Framework.core
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _storage = new Dictionary<string, object>();
        private readonly Dictionary<string, List<string>> _dependencies = new Dictionary<string, List<string>>();

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Base constructor
        /// </summary>
        public BaseViewModel()
        {
            ResolvePropertyDependencies();
        }

        /// <summary>
        /// Rises <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        public void OnPropertyChanged([CallerMemberName]string name = default(string))
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Sets Value and calls <see cref="OnPropertyChanged()"/> for property
        /// <para>
        /// NOTE: This method will also call <see cref="OnPropertyChanged()"/> for all dependencies specified in <see cref="DependsOnAttribute"/>
        /// </para>
        /// </summary>
        /// <param name="value">Value to store.</param>
        /// <param name="name">Property name.</param>
        protected void SetValue(object value, [CallerMemberName]string name = null)
        {
            if (!_storage.ContainsKey(name))
            {
                _storage.Add(name, value);
            }
            else
            {
                _storage[name] = value;
            }

            // Rise PropertyChanged for property
            OnPropertyChanged(name);

            // and for all properties that depends on it
            foreach(var prop in _dependencies[name])
            {
                OnPropertyChanged(prop);
            }
        }

        /// <summary>
        /// Gets Value from storage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        protected T GetValue<T>([CallerMemberName]string name = null)
        {
            if (_storage.ContainsKey(name))
            {
                return (T)_storage[name];
            }
            return default(T);
        }

        /// <summary>
        /// Resolves all dependencies that property have
        /// </summary>
        private void ResolvePropertyDependencies()
        {
            foreach (var dependantPropertyInfo in this.GetType().GetRuntimeProperties())
            {
                // Check for DependsOnAttribute
                var dependsOnAttribute = dependantPropertyInfo.GetCustomAttribute<DependsOnAttribute>();
                if (dependsOnAttribute == null)
                {
                    continue;
                }

                foreach (var property in dependsOnAttribute.SourceProperties)
                {
                    if (_dependencies.NotContainsKey(property))
                    {
                        _dependencies.Add(property, new List<string>());
                    }
                   _dependencies[property].Add(dependantPropertyInfo.Name);
                }
            }
        }
    }
}
