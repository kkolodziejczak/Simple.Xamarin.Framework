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
    public abstract class BasePageViewModel : BaseViewModel
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
        public BaseComponentViewModel BottomToolBar { get; set; }

        /// <summary>
        /// ViewModel that represents how UpperToolbar will be displayed on this page
        /// </summary>
        public BaseComponentViewModel UpperToolBar { get; set; }

        /// <summary>
        /// ViewModel that represents how ActivityIndicator will be displayed on this page
        /// </summary>
        public ActivityIndicatorViewModel ActivityIndicator { get; set; }

        /// <summary>
        /// Base Constructor
        /// </summary>
        public BasePageViewModel()
        {
            UpperToolBar = new BaseComponentViewModel();
            BottomToolBar = new BaseComponentViewModel();
            ActivityIndicator = new ActivityIndicatorViewModel();
            ActivityIndicator.BeforeShow += () => BlockUI();
            ActivityIndicator.BeforeHide += () => UnblockUI();
            InitializeNavigationBar();
            InitializeUpperToolBar();
            InitializeBottomToolBar();
        }

        /// <summary>
        /// Method that initializes NavigationBar on this Page
        /// <para>
        /// NOTE: This method is always called from <see cref="BasePageViewModel"/> Constructor
        /// </para>
        /// </summary>
        public abstract void InitializeNavigationBar();

        /// <summary>
        /// Method that initializes UpperToolBar on this Page
        /// <para>
        /// NOTE: This method is always called from <see cref="BasePageViewModel"/> Constructor
        /// </para>
        /// </summary>
        public abstract void InitializeUpperToolBar();

        /// <summary>
        /// Method that initializes BottomToolBar on this Page
        /// <para>
        /// NOTE: This method is always called from <see cref="BasePageViewModel"/> Constructor
        /// </para>
        /// </summary>
        public abstract void InitializeBottomToolBar();

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
                Debug.WriteLine("[SXF] BackButton is Blocked!");
                return true;
            }
            return false;
        }

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
