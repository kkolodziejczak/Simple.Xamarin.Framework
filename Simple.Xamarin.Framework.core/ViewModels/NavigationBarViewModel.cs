using System.Windows.Input;

namespace Simple.Xamarin.Framework.core
{
    public class NavigationBarViewModel : BaseViewModel
    {
        private static bool _isVisible;

        /// <summary>
        /// Indicates if NavigationBar is shown
        /// </summary>
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
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

        /// <summary>
        /// Shows NavigationBar
        /// </summary>
        public void Show()
        {
            IsVisible = true;
        }

        /// <summary>
        /// Hides NavigationBar
        /// </summary>
        public void Hide()
        {
            IsVisible = false;
        }
    }
}
