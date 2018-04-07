using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Simple.Xamarin.Framework.core
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        readonly Dictionary<string, object> _storage = new Dictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public void OnPropertyChanged([CallerMemberName]string name = default(string))
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Sets Value and calls <see cref="OnPropertyChanged(string)"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
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
            OnPropertyChanged(name);
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
    }
}
