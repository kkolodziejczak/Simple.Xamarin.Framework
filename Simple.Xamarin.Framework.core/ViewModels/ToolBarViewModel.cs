using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Simple.Xamarin.Framework.core
{
    /// <summary>
    /// 
    /// </summary>
    public class ToolBarViewModel : BaseViewModel
    {

        private static bool _isVisible;

        /// <summary>
        /// Indicates if ToolBar is shown
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
        /// ToolBar's Items
        /// </summary>
        public ObservableCollection<ToolBarItemViewModel> Items { get; private set; }

        /// <summary>
        /// Adds new Item to the ToolBar
        /// </summary>
        /// <param name="title"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToolBarViewModel AddItem(string title, ICommand command, bool fontAwesomeFont = false)
        {
            if (Items == null)
                Items = new ObservableCollection<ToolBarItemViewModel>();
            Items.Add(new ToolBarItemViewModel
            {
                Title = title,
                Command = command,
                FontAwesome = fontAwesomeFont,
            });

            return this;
        }

        /// <summary>
        /// Shows ToolBar
        /// </summary>
        public void Show()
        {
            IsVisible = true;
        }

        /// <summary>
        /// Hides ToolBar
        /// </summary>
        public void Hide()
        {
            IsVisible = false;
        }

    }

    /// <summary>
    /// ViewModel for ToolBar that represents one control
    /// </summary>
    public class ToolBarItemViewModel
    {
        /// <summary>
        /// Title of the button
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Command to execute after button is pressed
        /// </summary>
        public ICommand Command { get; set; }

        /// <summary>
        /// Indicates if Button should have FontFamily set to FontAwesome
        /// </summary>
        public bool FontAwesome { get; set; }

        /// <summary>
        /// Returns FontFamily for the button
        /// </summary>
        public string FontFamily { get => FontAwesome ? "FontAwesome" : null; }
        
    }

}
