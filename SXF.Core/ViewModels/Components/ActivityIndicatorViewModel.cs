using System;

namespace SXF.Core.ViewModels
{
    public class ActivityIndicatorViewModel : BaseComponentViewModel
    {
        /// <summary>
        /// Text that is displayed
        /// </summary>
        public string Text
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        /// <summary>
        /// Displays ActivityIndicator on the screen
        /// </summary>
        /// <param name="text"></param>
        public void Show(string text)
        {
            Text = text;
            base.Show();
        }
    }
}