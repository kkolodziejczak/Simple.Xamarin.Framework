using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Simple.Xamarin.Framework.core
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public void OnPropertyChanged([CallerMemberName]string name = default(string))
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
