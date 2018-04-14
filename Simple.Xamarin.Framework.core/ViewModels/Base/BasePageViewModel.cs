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

        /// <summary>
        /// Indicates if UI is blocked
        /// </summary>
        public bool UIBlocked
        {
            get => GetValue<bool>();
            private set => SetValue(value);
        }

        /// <summary>
        /// Indicates if Hardware back button is blocked
        /// </summary>
        public bool BackButtonBlocked
        {
            get => GetValue<bool>();
            private set => SetValue(value);
        }

        /// <summary>
        /// ViewModel that represents how NavigationBar will be displayed on this page
        /// </summary>
        public NavigationBarViewModel NavigationBar { get; protected set; }

        /// <summary>
        /// ViewModel that represents how BottomToolbar will be displayed on this page
        /// </summary>
        public BaseComponentViewModel BottomToolBar { get; protected set; }

        /// <summary>
        /// ViewModel that represents how UpperToolbar will be displayed on this page
        /// </summary>
        public BaseComponentViewModel UpperToolBar { get; protected set; }

        /// <summary>
        /// ViewModel that represents how ActivityIndicator will be displayed on this page
        /// </summary>
        public ActivityIndicatorViewModel ActivityIndicator { get; protected set; }

        /// <summary>
        /// ViewModel that represents how ProgressBar will be displayed on this page
        /// </summary>
        public ProgressBarViewModel ProgressBar { get; protected set; }

        /// <summary>
        /// Base Constructor
        /// </summary>
        public BasePageViewModel()
        {
            UpperToolBar = new BaseComponentViewModel();
            BottomToolBar = new BaseComponentViewModel();
            ActivityIndicator = new ActivityIndicatorViewModel();
            ActivityIndicator.OnShow += () => BlockUI();
            ActivityIndicator.OnHide += () => UnblockUI();
            ProgressBar = new ProgressBarViewModel();

            Initialize();
        }

        /// <summary>
        /// Initializes Page
        /// </summary>
        private void Initialize()
        {
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
            {
                BackButtonBlocked = true;
            }
            UIBlocked = true;
        }

        /// <summary>
        /// Unblocks UI clicks
        /// </summary>
        /// <param name="withHardwareBackButton"></param>
        public void UnblockUI(bool withHardwareBackButton = true)
        {
            if (withHardwareBackButton)
            {
                BackButtonBlocked = false;
            }
            UIBlocked = false;
        }

        /// <summary>
        /// Override for base OnBackButtonPressed to block it if <see cref="BlockUI(bool)"/> was called before.
        /// </summary>
        /// <returns><see cref="true"/> if backButton is blocked!</returns>
        public bool OnBackButtonPressed()
        {
            if (BackButtonBlocked)
            {
                Debug.WriteLine("[SXF] BackButton is Blocked!");
                return true;
            }
            return false;
        }

    }
}
