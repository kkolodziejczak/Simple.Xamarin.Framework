using System;

namespace Simple.Xamarin.Framework.core
{
    public class ActivityIndicatorViewModel : BaseComponentViewModel
    {

        public bool IsRunning { get; set; }


        public string Text { get; set; }

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