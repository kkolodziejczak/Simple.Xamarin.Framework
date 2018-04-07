using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Xamarin.Framework.core
{
    public class BaseComponentViewModel : BaseViewModel
    {
        private bool _isVisible;

        /// <summary>
        /// Event that will be fired before <see cref="BaseComponentViewModel"/> is shown
        /// </summary>
        public event Action OnShow = () => { };

        /// <summary>
        /// Event that will be fired before <see cref="BaseComponentViewModel"/> is hidden
        /// </summary>
        public event Action OnHide = () => { };

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
            OnShow();
            IsVisible = true;
        }

        /// <summary>
        /// Hides view on the page
        /// </summary>
        public void Hide()
        {
            OnHide();
            IsVisible = false;
        }
    }
}
