using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Simple.Xamarin.Framework.core
{
    /// <summary>
    /// Base ViewModel to all Content Pages
    /// </summary>
    public class BasePageViewModel : BaseViewModel
    {
        private static bool _BackButtonBlocked;
        private static bool _UIBlocked;

        /// <summary>
        /// ViewModel that represents how NavigationBar will be displayed on this page
        /// </summary>
        public NavigationBarViewModel NavigationBar { get; set; }

        /// <summary>
        /// ViewModel that represents how BottomToolbar will be displayed on this page
        /// </summary>
        public ToolBarViewModel BottomToolBar { get; set; }

        /// <summary>
        /// ViewModel that represents how BottomToolbar will be displayed on this page
        /// </summary>
        public ToolBarViewModel UpperToolBar { get; set; }

        /// <summary>
        /// Base Constructor
        /// </summary>
        public BasePageViewModel()
        {
            NavigationBar = new NavigationBarViewModel
            {
                IsVisible = true,
                Title = "Simple Test1",
                LeftButtonTitle = "Left2",
                RightButtonTitle = "Right1",
                LeftButtonCommand = new SequentialCommand((Action)TestLeftButton),
                RightButtonCommand = new SequentialCommand(TestRightButton),
            };

            BottomToolBar = new ToolBarViewModel()
                .AddItem("1", new SequentialCommand(() => { Debug.WriteLine("Hello 1."); }))
                .AddItem("2", new SequentialCommand(() => { Debug.WriteLine("Hello 2."); }))
                .AddItem("3", new SequentialCommand(() => { Debug.WriteLine("Hello 3."); }))
                .AddItem("4", new SequentialCommand(() => { Debug.WriteLine("Hello 4."); }));

            UpperToolBar = new ToolBarViewModel()
                .AddItem("Upper", new SequentialCommand(() => 
                {
                    if (NavigationBar.IsVisible)
                        NavigationBar.Hide();
                    else
                        NavigationBar.Show();
                }))
                .AddItem("Upper2", new SequentialCommand(() => 
                {
                    if (BottomToolBar.IsVisible)
                        BottomToolBar.Hide();
                    else
                        BottomToolBar.Show();
                }));
        }

        public void TestLeftButton()
        {
            if(UpperToolBar.IsVisible)
                UpperToolBar.Hide();
            else
                UpperToolBar.Show();
        }

        public async Task TestRightButton()
        {
            ShowProgressBar("Downloading...\nPlease Wait!");
            await Task.Delay(7 * 1000);
            ShowActivityIndicator("Please Wait!");
            HideProgressBar();
            await Task.Delay(5 * 1000);
            HideActivityIndicator();
        }

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

        #region ActivityIndicator

        private static bool _displayActivityIndicator;
        public bool DisplayActivityIndicator
        {
            get => _displayActivityIndicator;
            set
            {
                _displayActivityIndicator = value;
                OnPropertyChanged();
            }
        }

        private static string _activityIndicatorText;
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

            // Notify about change
            DisplayActivityIndicator = true;
        }

        public void HideActivityIndicator()
        {
            // UnblockUI and hide ActivityIndicator
            DisplayActivityIndicator = false;
            UnblockUI();
        }

        #endregion

        #region ProgressBar

        private static bool _displayProgressBar;
        public bool DisplayProgressBar
        {
            get => _displayProgressBar;
            set
            {
                _displayProgressBar = value;
                OnPropertyChanged();
            }
        }

        private static string _progressBarText;
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

            // Notify about change
            DisplayProgressBar = true;
        }

        public void HideProgressBar()
        {
            // UnblockUI and hide ActivityIndicator
            DisplayProgressBar = false;
            UnblockUI();
        }

        #endregion

    }
}
