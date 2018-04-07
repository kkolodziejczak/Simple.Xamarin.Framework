using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Xamarin.Framework.core
{
    public class BaseComponentViewModel : BaseViewModel
    {
        private static bool _isVisible;

        /// <summary>
        /// Event that will be fired before <see cref="BaseComponentViewModel"/> is shown
        /// </summary>
        public event Action BeforeShow = () => { };

        /// <summary>
        /// Event that will be fired after <see cref="BaseComponentViewModel"/> is shown
        /// </summary>
        public event Action AfterShow = () => { };

        /// <summary>
        /// Event that will be fired before <see cref="BaseComponentViewModel"/> is hidden
        /// </summary>
        public event Action BeforeHide = () => { };

        /// <summary>
        /// Event that will be fired after <see cref="BaseComponentViewModel"/> is hidden
        /// </summary>
        public event Action AfterHide = () => { };

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
            BeforeShow();
            IsVisible = true;
            AfterShow();
        }

        /// <summary>
        /// Hides view on the page
        /// </summary>
        public void Hide()
        {
            BeforeHide();
            IsVisible = false;
            AfterHide();
        }
    }
}
