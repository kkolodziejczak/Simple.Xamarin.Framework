using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Xamarin.Framework.core
{
    public class BaseComponentViewModel : BaseViewModel
    {
        private static bool _isVisible;

        /// <summary>
        /// Indicates if view is shown
        /// </summary>
        public bool IsVisible
        {
            get => _isVisible;
            private set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Shows view on the page
        /// </summary>
        public void Show()
        {
            IsVisible = true;
        }

        /// <summary>
        /// Hides view on the page
        /// </summary>
        public void Hide()
        {
            IsVisible = false;
        }
    }
}
