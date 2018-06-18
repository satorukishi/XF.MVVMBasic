using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.MVVMBasic.View;
using XF.MVVMBasic.ViewModel;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace XF.MVVMBasic
{
	public partial class App : Application
	{
        public static AlunoViewModel AlunoVM { get; set; }

        public App()
		{
			InitializeComponent();

            if (AlunoVM == null) AlunoVM = new AlunoViewModel();

            MainPage = new NavigationPage(new AlunoView() { BindingContext = AlunoVM});
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
