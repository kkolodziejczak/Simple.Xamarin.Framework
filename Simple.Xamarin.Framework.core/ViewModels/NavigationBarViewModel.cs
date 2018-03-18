using System.Windows.Input;

namespace Simple.Xamarin.Framework.core
{
    public class NavigationBarViewModel : BaseViewModel
    {
        private static bool _show;

        /// <summary>
        /// Indicates if ToolBar is shown
        /// </summary>
        public bool Show
        {
            get => _show;
            set
            {
                _show = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Title of the NavigationBar
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Title of the left Button
        /// </summary>
        public string LeftButtonTitle { get; set; }

        /// <summary>
        /// Command that will be executed when left button is pressed
        /// </summary>
        public ICommand LeftButtonCommand { get; set; }

        /// <summary>
        /// Title of the right Button
        /// </summary>
        public string RightButtonTitle { get; set; }

        /// <summary>
        /// Command that will be executed when right button is pressed
        /// </summary>
        public ICommand RightButtonCommand { get; set; }
    }
}
