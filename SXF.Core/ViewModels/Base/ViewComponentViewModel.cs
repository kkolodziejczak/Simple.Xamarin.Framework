using System;
using System.Collections.Generic;
using System.Text;

namespace SXF.Core.ViewModels
{
    public class BaseComponentViewModel : BaseViewModel
    {
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
            get => GetValue<bool>();
            private set => SetValue(value);
        }

        /// <summary>
        /// Shows view on the page
        /// </summary>
        public virtual void Show()
        {
            OnShow();
            IsVisible = true;
        }

        /// <summary>
        /// Hides view on the page
        /// </summary>
        public virtual void Hide()
        {
            OnHide();
            IsVisible = false;
        }
    }
}
