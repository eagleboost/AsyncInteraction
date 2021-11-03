namespace AsyncInteractionApp
{
  using System.Threading;
  using System.Windows.Threading;

  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow
  {
    public MainWindow()
    {
      InitializeComponent();

      Title = $"Main Window [Main GUI Thread]";
      ShowSecondaryWindow();
    }

    private void ShowSecondaryWindow()
    {
      var wait = new ManualResetEventSlim();

      void ThreadStart()
      {
        var dispatcher = Dispatcher.CurrentDispatcher;
        dispatcher.BeginInvoke(() =>
        {
          var w = new SecondaryWindow { Title = $"Secondary Window [{dispatcher.Thread.Name}]" };
          w.Show();
          wait.Set();
        });

        Dispatcher.Run();
      }

      var thread = new Thread((ThreadStart)ThreadStart) { Name = "Secondary GUI Thread", IsBackground = true };
      thread.SetApartmentState(ApartmentState.STA);
      thread.Start();

      wait.Wait();
    }
  }
}