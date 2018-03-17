using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Simple.Xamarin.Framework.Base
{
    /// <summary>
    /// Base ViewModel to all Content Pages
    /// </summary>
    public class BasePageViewModel : INotifyPropertyChanged
    {
        private bool _BackButtonBlocked;
        private bool _UIBlocked;
        
        /// <summary>
        /// Base Constructor
        /// </summary>
        public BasePageViewModel()
        {
            
            LeftButtonCommand = new Command(() =>
            {
                ShowActivityIndicator("Please Wait!");

                Task.Run(async () =>
                {
                    await Task.Delay(4*1000);
                    HideActivityIndicator();
                });
            });

            RightButtonCommand = new Command(() =>
            {
                ShowProgressBar("Downloading...\nPlease Wait!");

                Task.Run(async () =>
                {
                    await Task.Delay(4 * 1000);
                    HideProgressBar();
                });
            });
        }

        #region Navigation Bar

        /// <summary>
        /// Determines if BottomToolBar will be shown
        /// </summary>
        public bool ShowToolBar { get; set; }
        /// <summary>
        /// Indicates if ToolBar was shown before ActivityIndicator was shown
        /// </summary>
        private bool _wasToolBarShown;

        /// <summary>
        /// Determines if NavigationBar will be shown
        /// </summary>
        public bool ShowNavigationBar { get; set; } = true;
        /// <summary>
        /// Indicates if NavigationBar was shown before ActivityIndicator was shown
        /// </summary>
        private bool _wasNavigationBarShown;

        /// <summary>
        /// Title of the page
        /// </summary>
        public string PageTitle { get; set; }

        /// <summary>
        /// Title of the left button
        /// </summary>
        public string LeftButtonTitle { get; set; }

        /// <summary>
        /// Command that will fire after left button was pressed
        /// </summary>
        public ICommand LeftButtonCommand { get; set; }

        /// <summary>
        /// Title of the right button
        /// </summary>
        public string RightButtonTitle { get; set; }

        /// <summary>
        /// Command that will fire after right button was pressed
        /// </summary>
        public ICommand RightButtonCommand { get; set; }

        #endregion

        #region Restriction stuff

        /// <summary>
        /// Blocks UI clicks
        /// </summary>
        public void BlockUI(bool withHardwareBackButton = true)
        {
            if (withHardwareBackButton)
                _BackButtonBlocked = true;
            _UIBlocked = true;
        }

        /// <summary>
        /// Unblocks UI clicks
        /// </summary>
        /// <param name="withHardwareBackButton"></param>
        public void UnblockUI(bool withHardwareBackButton = true)
        {
            if (withHardwareBackButton)
                _BackButtonBlocked = false;
            _UIBlocked = false;
        }

        /// <summary>
        /// Override for base OnBackButtonPressed to block it if <see cref="BlockUI(bool)"/> was called before.
        /// </summary>
        /// <returns><see cref="true"/> if backButton is blocked!</returns>
        public bool OnBackButtonPressed()
        {
            if (_BackButtonBlocked)
            {
                Debug.WriteLine("BackButton is Blocked!");
                return true;
            }
            return false;
        }

        #endregion

        #region ActivityIndicator

        private bool _displayActivityIndicator;
        public bool DisplayActivityIndicator
        {
            get => _displayActivityIndicator;
            set
            {
                _displayActivityIndicator = value;
                OnPropertyChanged();
            }
        }

        private string _activityIndicatorText;
        public string ActivityIndicatorText
        {
            get => _activityIndicatorText;
            set
            {
                _activityIndicatorText = value;
                OnPropertyChanged();
            }
        }

        public void ShowActivityIndicator(string text)
        {
            // Block UI
            BlockUI();

            // Setup 
            ActivityIndicatorText = text;

            // Save current layout to revert later
            SaveCurrentView();

            // Notify about change
            DisplayActivityIndicator = true;
        }

        public void HideActivityIndicator()
        {
            // Restore status from past
            RestoreLastView();

            // UnblockUI and hide ActivityIndicator
            DisplayActivityIndicator = false;
            UnblockUI();
        }

        #endregion

        #region ProgressBar

        private bool _displayProgressBar;
        public bool DisplayProgressBar
        {
            get => _displayProgressBar;
            set
            {
                _displayProgressBar = value;
                OnPropertyChanged();
            }
        }

        private string _progressBarText;
        public string ProgressBarText
        {
            get => _progressBarText;
            set
            {
                _progressBarText = value;
                OnPropertyChanged();
            }
        }


        public void ShowProgressBar(string text)
        {
            // Block UI
            BlockUI();

            // Set text
            ProgressBarText = text;

            // Save current layout to revert later
            SaveCurrentView();

            // Notify about change
            DisplayProgressBar = true;
        }

        public void HideProgressBar()
        {
            // Restore status from past
            RestoreLastView();

            // UnblockUI and hide ActivityIndicator
            DisplayProgressBar = false;
            UnblockUI();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Saves current status of the view. If NavigationBar was shown and/or ToolBar
        /// </summary>
        private void SaveCurrentView()
        {
            _wasNavigationBarShown = ShowNavigationBar;
            ShowNavigationBar = false;

            _wasToolBarShown = ShowToolBar;
            ShowToolBar = false;
        }

        /// <summary>
        /// Restores status of last view.
        /// </summary>
        private void RestoreLastView()
        {
            ShowNavigationBar = _wasNavigationBarShown;
            ShowToolBar = _wasToolBarShown;
        }

        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public void OnPropertyChanged([CallerMemberName]string name = default(string))
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        } 

        #endregion

    }
}
