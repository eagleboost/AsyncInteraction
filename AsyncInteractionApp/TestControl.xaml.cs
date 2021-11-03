namespace AsyncInteractionApp
{
  using System.Windows.Threading;
  using AsyncInteraction;
  using AsyncInteraction.Extensions;
  using AsyncInteraction.MessageBox;
  using AsyncInteraction.Tasks;
  using AsyncInteractionApp.Login;

  /// <summary>
  /// Interaction logic for TestControl.xaml
  /// </summary>
  public partial class TestControl
  {
    public TestControl()
    {
      InitializeComponent();

      var dispatcher = Dispatcher.CurrentDispatcher;
      var threadName = dispatcher.GetThreadName();
      DataContext = new ViewModel(threadName)
      {
        DispatcherWaiter = new DispatcherWaiter(dispatcher),
        MessageBox = new AsyncMessageBox(),
        LoginInteraction = new AsyncInteractionWindow<LoginWindow, ILoginViewModel>(),
      };
    }
  }
}