using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Simple.Xamarin.Framework.core
{
    /// <summary>
    /// ViewModel that represents How ToolBar will be displayed
    /// </summary>
    public class ToolBarViewModel : BaseComponentViewModel
    {
        /// <summary>
        /// ToolBar's Items
        /// </summary>
        public ObservableCollection<ToolBarItemViewModel> Items { get; private set; }

        /// <summary>
        /// Adds new Item to the ToolBar
        /// </summary>
        /// <param name="title"></param>
        /// <param name="tapCommand"></param>
        /// <returns></returns>
        public ToolBarViewModel AddItem(string title, ICommand tapCommand, bool fontAwesomeFont = false)
        {
            if (Items == null)
                Items = new ObservableCollection<ToolBarItemViewModel>();
            Items.Add(new ToolBarItemViewModel
            {
                Title = title,
                TapCommand = tapCommand,
                FontAwesome = fontAwesomeFont,
            });

            return this;
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
        public ICommand TapCommand { get; set; }

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
