using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Simple.Xamarin.Framework.core;
using System.Windows.Input;
using System.Threading.Tasks;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace SXF.sample
{

    public class SampleVM : BasePageViewModel
    {
        [DependsOn(nameof(Command))]
        public string Test { get; set; }

        public ICommand Command { get; set; }

        public SampleVM()
        {
            Command = new SequentialCommand(async () =>
            {
                await Task.Delay(4000);
            });
        }

        public override void InitializeNavigationBar()
        {
        }

        public override void InitializeUpperToolBar()
        {
        }

        public override void InitializeBottomToolBar()
        {
        }
    }

    public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            var page = new MainPage
            {
                BindingContext = new SampleVM()
            };
            MainPage = page;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
